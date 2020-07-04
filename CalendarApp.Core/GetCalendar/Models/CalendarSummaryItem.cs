using System;
using System.Collections.Generic;

namespace CalendarApp.Core.GetCalendar.Models {
    public class CalendarSummaryItem {
        public Guid Id { get; }
        public string Name { get; }
        public string ReminderText { get; }
        public DateTime DateOfCalItemThisYear { get; }
        public CalendarSummaryItem (Guid id, string name, string reminderText, DateTime dateOfCalItemThisYear) {
            Id = id;
            Name = name;
            ReminderText = reminderText;
            DateOfCalItemThisYear = dateOfCalItemThisYear;
        }
    }
}