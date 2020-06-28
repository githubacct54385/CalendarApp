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

        public List<CalendarItem> GetItems () {
            var today = _dateProvider.GetToday ();
            var items = _calendarItemProvider.GetItems ();
            var newItems = GetNewStartDatesIfNecessary (items);
            var afterToday = newItems.Where (p => p.RepeatRules.StartOn > today).ToList ();
            if (!afterToday.Any ()) {
                return new List<CalendarItem> ();
            }
            var remindersToReturn = new List<CalendarItem> ();
            var nthDayOfMonthRules = afterToday.Where (p => p.When.NthDayOfMonthRules != null).ToList ();
            var nthWeekdayOfMonthRules = afterToday.Where (p => p.When.NthWeekdayOfMonthRules != null).ToList ();

            remindersToReturn.AddRange (AddNthDayOfMonthRules (nthDayOfMonthRules));
            remindersToReturn.AddRange (AddNthWeekdayOfMonthRules (nthWeekdayOfMonthRules));
            return remindersToReturn;
        }

        private IEnumerable<CalendarItem> AddNthWeekdayOfMonthRules (List<CalendarItem> nthWeekdayOfMonthRules) {
            var remindersToReturn = new List<CalendarItem> ();
            nthWeekdayOfMonthRules.ForEach (entry => {
                entry.Reminders.Split (',').ToList ().ForEach (reminder => {

                    CheckReminderLength (reminder);

                    int.TryParse (reminder[0].ToString (), out int amount);
                    var unit = reminder[1];
                    switch (unit) {
                        case 'm':
                            var daysInMonth = 30;
                            if (IsWithinReminderThreshold (amount, daysInMonth, entry)) {
                                if (CanAddToReminders (remindersToReturn, entry)) {
                                    remindersToReturn.Add (entry);
                                }
                            }
                            break;
                        case 'w':
                            var daysInWeek = 7;
                            if (IsWithinReminderThreshold (amount, daysInWeek, entry)) {
                                if (CanAddToReminders (remindersToReturn, entry)) {
                                    remindersToReturn.Add (entry);
                                }
                            }
                            break;
                        case 'd':
                            var day = 1;
                            if (IsWithinReminderThreshold (amount, day, entry)) {
                                if (CanAddToReminders (remindersToReturn, entry)) {
                                    remindersToReturn.Add (entry);
                                }
                            }
                            break;
                        default:
                            throw new ArgumentException ("Unknown unit.  Only m,w,d are allowed.");
                    }
                });
            });
            return remindersToReturn;
        }

        private List<CalendarItem> AddNthDayOfMonthRules (List<CalendarItem> nthDayOfMonthRules) {
            var remindersToReturn = new List<CalendarItem> ();
            nthDayOfMonthRules.ForEach (entry => {
                entry.Reminders.Split (',').ToList ().ForEach (reminder => {

                    CheckReminderLength (reminder);

                    int.TryParse (reminder[0].ToString (), out int amount);
                    var unit = reminder[1];
                    switch (unit) {
                        case 'm':
                            var daysInMonth = 30;
                            if (IsWithinReminderThreshold (amount, daysInMonth, entry)) {
                                if (CanAddToReminders (remindersToReturn, entry)) {
                                    remindersToReturn.Add (entry);
                                }
                            }
                            break;
                        case 'w':
                            var daysInWeek = 7;
                            if (IsWithinReminderThreshold (amount, daysInWeek, entry)) {
                                if (CanAddToReminders (remindersToReturn, entry)) {
                                    remindersToReturn.Add (entry);
                                }
                            }
                            break;
                        case 'd':
                            var day = 1;
                            if (IsWithinReminderThreshold (amount, day, entry)) {
                                if (CanAddToReminders (remindersToReturn, entry)) {
                                    remindersToReturn.Add (entry);
                                }
                            }
                            break;
                        default:
                            throw new ArgumentException ("Unknown unit.  Only m,w,d are allowed.");
                    }
                });
            });
            return remindersToReturn;
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

        private bool CanAddToReminders (List<CalendarItem> list, CalendarItem newItem) {
            if (list.Contains (newItem)) {
                return false;
            }
            return true;
        }
    }
}