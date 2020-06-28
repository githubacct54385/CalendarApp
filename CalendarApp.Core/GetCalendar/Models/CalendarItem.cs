using System;
using System.Collections.Generic;

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

        public override bool Equals (object obj) {
            return obj is CalendarItem item &&
                Id.Equals (item.Id) &&
                Name == item.Name &&
                Reminder == item.Reminder &&
                RepeatsYearly == item.RepeatsYearly &&
                EqualityComparer<When>.Default.Equals (When, item.When) &&
                EqualityComparer<RepeatRules>.Default.Equals (RepeatRules, item.RepeatRules) &&
                Reminders == item.Reminders;
        }

        public override int GetHashCode () {
            int hashCode = 1554778732;
            hashCode = hashCode * -1521134295 + Id.GetHashCode ();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode (Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode (Reminder);
            hashCode = hashCode * -1521134295 + RepeatsYearly.GetHashCode ();
            hashCode = hashCode * -1521134295 + EqualityComparer<When>.Default.GetHashCode (When);
            hashCode = hashCode * -1521134295 + EqualityComparer<RepeatRules>.Default.GetHashCode (RepeatRules);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode (Reminders);
            return hashCode;
        }
    }
}