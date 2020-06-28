using System;

namespace CalendarApp.Core.GetCalendar.Models {
    public class CalendarSummaryItem {
        public Guid Id { get; }
        public string Name { get; }
        public string Reminder { get; }
        public Threshold Threshold { get; }
        public CalendarSummaryItem (Guid id, string name, string reminder, Threshold threshold) {
            Id = id;
            Name = name;
            Reminder = reminder;
            Threshold = threshold;
        }

        internal bool TheSameItem (CalendarItem item) {
            if (this.Id == item.Id &&
                this.Name == item.Name &&
                this.Reminder == item.Reminder) {
                return true;
            }
            return false;
        }
    }

    public enum Threshold {
        Month,
        FourWeeks,
        ThreeWeeks,
        TwoWeeks,
        OneWeek,
        LessThanWeek,
        OneDay
    }
}