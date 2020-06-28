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
    }

    public enum Threshold {
        Month,
        TwoWeek,
        OneWeek,
        LessThanWeek,
        OneDay
    }
}