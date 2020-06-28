using System;

namespace CalendarApp.Core.GetCalendar.Models {
    public class CalendarItem {
        public CalendarItem (Guid id, string name, string reminder, bool repeatsYearly, When when, RepeatRules repeatRules, string reminders) {
            Id = id;
            Name = name;
            Reminder = reminder;
            RepeatsYearly = repeatsYearly;
            When = when;
            RepeatRules = repeatRules;
            Reminders = reminders;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Reminder { get; }
        public bool RepeatsYearly { get; }
        public When When { get; }
        public RepeatRules RepeatRules { get; }
        public string Reminders { get; }
    }
}