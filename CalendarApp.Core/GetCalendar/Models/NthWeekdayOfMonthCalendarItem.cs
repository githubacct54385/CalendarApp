using System;
using CalendarApp.Core.Exceptions;
using CalendarApp.Core.GetCalendar.Interface;

namespace CalendarApp.Core.GetCalendar.Models {
    public sealed class NthWeekdayOfMonthCalendarItem : CalendarItemBase {
        private const int FIRST_DAY_OF_MONTH = 1;
        private const int FIRST_WEEK_OF_MONTH = 1;
        public NthWeekdayOfMonthCalendarItem (Guid id, string name, string reminderText, string remindAtCsv, int month, int nthWeek, DayOfWeek dayOfWeek) : base (id, name, reminderText, remindAtCsv, month) {
            this.NthWeek = nthWeek;
            this.DayOfWeek = dayOfWeek;
        }

        public int NthWeek { get; }
        public DayOfWeek DayOfWeek { get; }

        public DateTime CalculateCalendarDateForYear (int year) {
            var iteratorDate = new DateTime (year, this.Month, FIRST_DAY_OF_MONTH);
            var nextMonth = iteratorDate.Month + 1;

            var weekCounter = FIRST_WEEK_OF_MONTH;

            while (iteratorDate.Month < nextMonth) {
                var matchingWeekday = iteratorDate.DayOfWeek == this.DayOfWeek;
                if (matchingWeekday) {
                    bool matchingNthWeek = weekCounter == this.NthWeek;
                    if (matchingNthWeek) {
                        return iteratorDate;
                    }
                    weekCounter++;
                }
                iteratorDate = iteratorDate.AddDays (1);
            }
            throw new CannotFindDateNextYearException ($"Unable to find the {this.NthWeek}th {this.DayOfWeek.ToString()} of {this.Month.ToString()} {this.Year.ToString()}");
        }

        public override void HandleYearUpdate (IDateProvider dateProvider) {
            this.Year = dateProvider.GetToday ().Year;

            var iteratorDate = new DateTime (this.Year, this.Month, FIRST_DAY_OF_MONTH);
            var nextMonth = iteratorDate.Month + 1;

            var weekCounter = FIRST_WEEK_OF_MONTH;

            while (iteratorDate.Month < nextMonth) {
                var matchingWeekday = iteratorDate.DayOfWeek == this.DayOfWeek;
                if (matchingWeekday) {
                    bool matchingNthWeek = weekCounter == this.NthWeek;
                    if (matchingNthWeek) {
                        this.DateOfCalItemThisYear = iteratorDate;
                        return;
                    }
                    weekCounter++;
                }
                iteratorDate = iteratorDate.AddDays (1);
            }
            throw new CannotFindDateNextYearException ($"Unable to find the {this.NthWeek}th {this.DayOfWeek.ToString()} of {this.Month.ToString()} {this.Year.ToString()}");
        }
    }
}