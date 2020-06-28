using System.Collections.Generic;

namespace CalendarApp.Core.GetCalendar.Models {
    public class When {
        public When (NthDayOfMonthRules nthDayOfMonthRules, NthWeekdayOfMonthRules nthWeekdayOfMonthRules) {
            NthDayOfMonthRules = nthDayOfMonthRules;
            NthWeekdayOfMonthRules = nthWeekdayOfMonthRules;
        }

        public NthDayOfMonthRules NthDayOfMonthRules { get; }
        public NthWeekdayOfMonthRules NthWeekdayOfMonthRules { get; }

        public override bool Equals (object obj) {
            return obj is When when &&
                EqualityComparer<NthDayOfMonthRules>.Default.Equals (NthDayOfMonthRules, when.NthDayOfMonthRules) &&
                EqualityComparer<NthWeekdayOfMonthRules>.Default.Equals (NthWeekdayOfMonthRules, when.NthWeekdayOfMonthRules);
        }

        public override int GetHashCode () {
            int hashCode = -1454153246;
            hashCode = hashCode * -1521134295 + EqualityComparer<NthDayOfMonthRules>.Default.GetHashCode (NthDayOfMonthRules);
            hashCode = hashCode * -1521134295 + EqualityComparer<NthWeekdayOfMonthRules>.Default.GetHashCode (NthWeekdayOfMonthRules);
            return hashCode;
        }
    }
}