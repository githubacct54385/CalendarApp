using System;
using CalendarApp.Core.Exceptions;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.GetCalendar {
    public class NthWeekdayOfMonthFutureDateProvider : IFutureDateProvider {
        private readonly IDateProvider _dateProvider;
        private const int FIRST_DAY_OF_MONTH = 1;
        public NthWeekdayOfMonthFutureDateProvider (IDateProvider dateProvider) {
            _dateProvider = dateProvider;
        }
        public CalendarItem CalcFutureDate (CalendarItem item) {

            var year = _dateProvider.GetToday ().Year;
            var weekday = item.When.NthWeekdayOfMonthRules.Weekday;
            var nthWeek = item.When.NthWeekdayOfMonthRules.NthWeekday;
            var month = item.RepeatRules.StartOn.Month;

            var iteratorDate = new DateTime (year, month, FIRST_DAY_OF_MONTH);
            var nextMonth = iteratorDate.Month + 1;

            var weekCounter = 1;

            while (iteratorDate.Month < nextMonth) {
                var matchingWeekday = iteratorDate.DayOfWeek == weekday;
                if (matchingWeekday) {
                    bool matchingNthWeek = weekCounter == nthWeek;
                    if (matchingNthWeek) {
                        return TheFutureDateToReturn (item, iteratorDate);
                    }
                    weekCounter++;
                }
                iteratorDate = iteratorDate.AddDays (1);
            }
            throw new CannotFindDateNextYearException ($"Unable to find the {nthWeek}th {weekday.ToString()} of {month.ToString()} {year.ToString()}");
        }

        private CalendarItem TheFutureDateToReturn (CalendarItem item, DateTime theDateNextYear) {
            var repeatRules = item.RepeatRules;
            var date = new DateTime (_dateProvider.GetToday ().Year, theDateNextYear.Month, theDateNextYear.Day);
            var newRepeatRules = new RepeatRules (date, repeatRules.EndOn);
            return new CalendarItem (item.Id, item.Name, item.Reminder, item.RepeatsYearly, item.When, newRepeatRules, item.Reminders);
        }

        public CalendarItem CalcCalendarItemForThisYear (CalendarItem item) {
            var year = _dateProvider.GetToday ().Year;
            var weekday = item.When.NthWeekdayOfMonthRules.Weekday;
            var nthWeek = item.When.NthWeekdayOfMonthRules.NthWeekday;
            var month = item.RepeatRules.StartOn.Month;

            var iteratorDate = new DateTime (year, month, FIRST_DAY_OF_MONTH);
            var nextMonth = iteratorDate.Month + 1;

            var weekCounter = 1;

            while (iteratorDate.Month < nextMonth) {
                var matchingWeekday = iteratorDate.DayOfWeek == weekday;
                if (matchingWeekday) {
                    bool matchingNthWeek = weekCounter == nthWeek;
                    if (matchingNthWeek) {
                        return TheFutureDateToReturn (item, iteratorDate);
                    }
                    weekCounter++;
                }
                iteratorDate = iteratorDate.AddDays (1);
            }
            throw new CannotFindDateNextYearException ($"Unable to find the {nthWeek}th {weekday.ToString()} of {month.ToString()} {year.ToString()}");
        }
    }
}