using System;
using CalendarApp.Core.GetCalendar.Interface;

namespace CalendarApp.Core.GetCalendar.Models {
    public abstract class CalendarItemBase {
        public Guid Id { get; }
        public string Name { get; }
        public string ReminderText { get; }
        public string RemindAtCsv { get; }
        public int Month { get; }
        public int Year { get; protected set; } = -1; // this will be set later
        public DateTime DateOfCalItemThisYear { get; set; } = DateTime.MinValue; // this will be set later
        public CalendarItemBase (Guid id, string name, string reminderText, string remindAtCsv, int month) {
            this.Id = id;
            this.Name = name;
            this.ReminderText = reminderText;
            this.RemindAtCsv = remindAtCsv;
            this.Month = month;
        }
        public abstract void HandleYearUpdate (IDateProvider dateProvider);

        public CalendarSummaryItem ToCalendarSummaryItem () {
            return new CalendarSummaryItem (this.Id, this.Name, this.ReminderText, this.DateOfCalItemThisYear);
        }
    }
}