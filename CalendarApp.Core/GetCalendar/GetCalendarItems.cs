using System;
using System.Collections.Generic;
using System.Linq;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.GetCalendar {
    public class GetCalendarItems {
        private readonly ICalendarItemProvider _calendarItemProvider;
        private readonly IDateProvider _dateProvider;
        public GetCalendarItems (
            ICalendarItemProvider calendarItemProvider,
            IDateProvider dateProvider) {
            _calendarItemProvider = calendarItemProvider;
            _dateProvider = dateProvider;
        }

        public List<CalendarSummaryItem> GetItems () {
            var today = _dateProvider.GetToday ();
            var items = _calendarItemProvider.GetItems ();
            var datesForThisYear = GetDatesForCurrentYear (items);

            if (!datesForThisYear.Any ()) {
                return new List<CalendarSummaryItem> ();
            }

            var nthDayOfMonthRules = datesForThisYear
                .Where (p => p is NthDayOfMonthCalendarItem)
                .Select (p => (NthDayOfMonthCalendarItem) p).ToList ();
            var nthWeekdayOfMonthRules = datesForThisYear
                .Where (p => p is NthWeekdayOfMonthCalendarItem)
                .Select (p => (NthWeekdayOfMonthCalendarItem) p).ToList ();

            var remindersToReturn = new List<CalendarSummaryItem> ();

            foreach (var date in datesForThisYear) {
                foreach (var reminder in date.RemindAtCsv.Split (',')) {
                    if (reminder.Length != 2) {
                        throw new ArgumentException ("Reminder length is not length of 2.  Configuration is wrong.");
                    }
                    if (IsDueForReminder (date, reminder) && !remindersToReturn.Contains (date.ToCalendarSummaryItem ())) {
                        remindersToReturn.Add (date.ToCalendarSummaryItem ());
                    }
                }
            }
            return remindersToReturn;
        }

        private List<CalendarItemBase> GetDatesForCurrentYear (List<CalendarItemBase> calendarItems) {
            var newCalItems = new List<CalendarItemBase> ();
            foreach (var calendarItem in calendarItems) {
                switch (calendarItem) {
                    case NthDayOfMonthCalendarItem nthDayCalItem:
                        nthDayCalItem.HandleYearUpdate (_dateProvider);
                        newCalItems.Add (nthDayCalItem);
                        break;
                    case NthWeekdayOfMonthCalendarItem nthWeekdayCalItem:
                        nthWeekdayCalItem.HandleYearUpdate (_dateProvider);
                        newCalItems.Add (nthWeekdayCalItem);
                        break;
                    default:
                        throw new Exception ("Unknown derived type.");
                }
            }
            return newCalItems;
        }

        private bool IsDueForReminder (CalendarItemBase date, string reminder) {
            var amount = int.Parse (reminder[0].ToString ());
            var unit = reminder[1];
            switch (unit) {
                case 'm':
                    return IsExactlyNMonthsAway (amount, date);
                case 'w':
                    return IsExactlyNWeeksAway (amount, date);
                case 'd':
                    return IsExactlyNDaysAway (amount, date);
                default:
                    throw new ArgumentException ("Unknown unit.  Only m,w,d are allowed.");
            }
        }

        private bool IsExactlyNMonthsAway (int amount, CalendarItemBase date) {
            var daysToSubtract = amount * 30;
            return IsExactlyDateTimeUnitsAway (daysToSubtract, date);
        }
        private bool IsExactlyNWeeksAway (int amount, CalendarItemBase date) {
            var daysToSubtract = amount * 7;
            return IsExactlyDateTimeUnitsAway (daysToSubtract, date);
        }
        private bool IsExactlyNDaysAway (int amount, CalendarItemBase date) {
            var daysToSubtract = amount * 1;
            return IsExactlyDateTimeUnitsAway (daysToSubtract, date);
        }

        private bool IsExactlyDateTimeUnitsAway (int daysToSubtract, CalendarItemBase date) {
            var startOn = date.DateOfCalItemThisYear;
            var dateMinusReminderLength = startOn - TimeSpan.FromDays (daysToSubtract);
            var today = _dateProvider.GetToday ();
            if (dateMinusReminderLength == today) {
                return true;
            }
            return false;
        }
    }
}