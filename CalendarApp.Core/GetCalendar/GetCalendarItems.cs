using System;
using System.Collections.Generic;
using System.Linq;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.GetCalendar {
    public class GetCalendarItems {

        private readonly ICalendarItemProvider _calendarItemProvider;
        private readonly IDateProvider _dateProvider;
        public GetCalendarItems (ICalendarItemProvider calendarItemProvider, IDateProvider dateProvider) {
            _calendarItemProvider = calendarItemProvider;
            _dateProvider = dateProvider;
        }

        public List<CalendarItem> GetItems () {
            var items = _calendarItemProvider.GetItems ();
            var newItems = GetNewStartDatesIfNecessary (items);
            var afterToday = newItems.Where (p => p.RepeatRules.StartOn > _dateProvider.GetToday ()).ToList ();
            if (!afterToday.Any ()) {
                return new List<CalendarItem> ();
            }
            var remindersToReturn = new List<CalendarItem> ();
            var nthDayOfMonthRules = afterToday.Where (p => p.When.NthDayOfMonthRules != null).ToList ();
            var nthWeekdayOfMonthRules = afterToday.Where (p => p.When.NthWeekdayOfMonthRules != null).ToList ();

            // nthWeekdayOfMonthRules.ForEach (entry => {
            //     entry.Reminders.Split (',').ToList ().ForEach (reminder => {
            //         if (reminder.Length != 2) {
            //             throw new ArgumentException ("Reminder length is not length of 2.  Configuration is wrong.");
            //         }
            //     });
            // });

            nthDayOfMonthRules.ForEach (entry => {
                entry.Reminders.Split (',').ToList ().ForEach (reminder => {
                    if (reminder.Length != 2) {
                        throw new ArgumentException ("Reminder length is not length of 2.  Configuration is wrong.");
                    }
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

        private List<CalendarItem> GetNewStartDatesIfNecessary (List<CalendarItem> calendarItems) {
            return calendarItems.Select (item => {
                var previousYear = item.RepeatRules.StartOn.Year < _dateProvider.GetToday ().Year;
                if (previousYear) {
                    return CalendarItemWithThisYear (item);
                } else {
                    return item;
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