using System;
using System.Collections.Generic;
using System.Linq;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.GetCalendar {
    public class GetCalendarItems {
        private readonly ICalendarItemProvider _calendarItemProvider;
        private readonly IDateProvider _dateProvider;
        private readonly IFutureDateProvider _futureDateProvider;
        public GetCalendarItems (
            ICalendarItemProvider calendarItemProvider,
            IDateProvider dateProvider,
            IFutureDateProvider futureDateProvider) {
            _calendarItemProvider = calendarItemProvider;
            _dateProvider = dateProvider;
            _futureDateProvider = futureDateProvider;
        }

        public List<CalendarSummaryItem> GetItems () {
            var today = _dateProvider.GetToday ();
            var items = _calendarItemProvider.GetItems ();
            var newItems = GetNewStartDatesIfNecessary (items);
            var afterToday = newItems.Where (p => p.RepeatRules.StartOn > today).ToList ();
            if (!afterToday.Any ()) {
                return new List<CalendarSummaryItem> ();
            }
            var remindersToReturn = new List<CalendarSummaryItem> ();
            var nthDayOfMonthRules = afterToday.Where (p => p.When.NthDayOfMonthRules != null).ToList ();
            var nthWeekdayOfMonthRules = afterToday.Where (p => p.When.NthWeekdayOfMonthRules != null).ToList ();

            remindersToReturn.AddRange (AddNthDayOfMonthRules (nthDayOfMonthRules));
            remindersToReturn.AddRange (AddNthWeekdayOfMonthRules (nthWeekdayOfMonthRules));
            return remindersToReturn;
        }

        private IEnumerable<CalendarSummaryItem> AddNthWeekdayOfMonthRules (List<CalendarItem> nthWeekdayOfMonthRules) {
            var remindersToReturn = new List<CalendarSummaryItem> ();

            nthWeekdayOfMonthRules.ForEach (entry => {
                entry.Reminders.Split (',').ToList ().ForEach (reminder => {
                    CheckReminderLength (reminder);
                    AddCalendarItemsToList (entry, reminder, remindersToReturn);
                });
            });
            return remindersToReturn;
        }

        private List<CalendarSummaryItem> AddNthDayOfMonthRules (List<CalendarItem> nthDayOfMonthRules) {
            var remindersToReturn = new List<CalendarSummaryItem> ();

            nthDayOfMonthRules.ForEach (entry => {
                entry.Reminders.Split (',').ToList ().ForEach (reminder => {
                    CheckReminderLength (reminder);
                    AddCalendarItemsToList (entry, reminder, remindersToReturn);
                });
            });
            return remindersToReturn;
        }

        private void AddCalendarItemsToList (CalendarItem entry, string reminder, List<CalendarSummaryItem> remindersToReturn) {
            int.TryParse (reminder[0].ToString (), out int amount);
            var unit = reminder[1];
            switch (unit) {
                case 'm':
                    var daysInMonth = 30;
                    if (IsWithinReminderThreshold (amount, daysInMonth, entry)) {
                        if (CanAddToReminders (remindersToReturn, entry)) {
                            remindersToReturn.Add (entry.ToSummaryItem (Threshold.Month));
                        }
                    }
                    break;
                case 'w':
                    var daysInWeek = 7;
                    if (IsWithinReminderThreshold (amount, daysInWeek, entry)) {
                        if (CanAddToReminders (remindersToReturn, entry)) {
                            switch (amount) {
                                case 1:
                                    remindersToReturn.Add (entry.ToSummaryItem (Threshold.OneWeek));
                                    break;
                                case 2:
                                    remindersToReturn.Add (entry.ToSummaryItem (Threshold.TwoWeeks));
                                    break;
                                case 3:
                                    remindersToReturn.Add (entry.ToSummaryItem (Threshold.ThreeWeeks));
                                    break;
                                case 4:
                                    remindersToReturn.Add (entry.ToSummaryItem (Threshold.FourWeeks));
                                    break;
                                default:
                                    throw new ArgumentException ($"Week amount {amount} is not supported.  Use Month if you want more.");
                            }
                        }
                    }
                    break;
                case 'd':
                    var day = 1;
                    if (IsWithinReminderThreshold (amount, day, entry)) {
                        if (CanAddToReminders (remindersToReturn, entry)) {
                            remindersToReturn.Add (entry.ToSummaryItem (Threshold.OneDay));
                        }
                    }
                    break;
                default:
                    throw new ArgumentException ("Unknown unit.  Only m,w,d are allowed.");
            }
        }

        private void CheckReminderLength (string reminder) {
            if (reminder.Length != 2) {
                throw new ArgumentException ("Reminder length is not length of 2.  Configuration is wrong.");
            }
        }

        private List<CalendarItem> GetNewStartDatesIfNecessary (List<CalendarItem> calendarItems) {

            return calendarItems.Select (item => {
                if (item.When.NthWeekdayOfMonthRules != null) {
                    return _futureDateProvider.CalcCalendarItemForThisYear (item);
                } else {
                    var previousYear = item.RepeatRules.StartOn.Year < _dateProvider.GetToday ().Year;
                    if (previousYear) {
                        return _futureDateProvider.CalcFutureDate (item);
                    } else {
                        return item;
                    }
                }
            }).ToList ();
        }

        private CalendarItem CalendarItemWithThisYear (CalendarItem item) {
            var repeatRules = item.RepeatRules;
            var startOn = repeatRules.StartOn;
            var date = new DateTime (_dateProvider.GetToday ().Year, startOn.Month, startOn.Day);
            var newRepeatRules = new RepeatRules (date, repeatRules.EndOn);
            return new CalendarItem (item.Id, item.Name, item.Reminder, item.RepeatsYearly, item.When, newRepeatRules, item.Reminders);
        }

        private bool IsWithinReminderThreshold (int amount, int unit, CalendarItem entry) {
            var daysToAdd = amount * unit;
            var startOn = entry.RepeatRules.StartOn;
            var dateMinusReminderLength = startOn - TimeSpan.FromDays (daysToAdd);
            var today = _dateProvider.GetToday ();
            if (dateMinusReminderLength == today) {
                return true;
            }
            return false;
        }

        private bool CanAddToReminders (List<CalendarSummaryItem> list, CalendarItem newItem) {
            if (list.Any (item => item.TheSameItem (newItem))) {
                return false;
            }
            return true;
        }
    }
}