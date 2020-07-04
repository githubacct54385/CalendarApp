using System;
using CalendarApp.Core.GetCalendar.Interface;

namespace CalendarApp.Core.GetCalendar.Models {
    public sealed class NthDayOfMonthCalendarItem : CalendarItemBase {
        public NthDayOfMonthCalendarItem (Guid id, string name, string reminderText, string remindAtCsv, int month, int day) : base (id, name, reminderText, remindAtCsv, month) {
            this.Day = day;
        }
        public int Day { get; }

        public override void HandleYearUpdate (IDateProvider dateProvider) {
            this.Year = dateProvider.GetToday ().Year;
            this.DateOfCalItemThisYear = new DateTime (this.Year, this.Month, this.Day);
        }
    }
}