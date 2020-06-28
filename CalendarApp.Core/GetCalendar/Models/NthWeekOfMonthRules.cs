namespace CalendarApp.Core.GetCalendar.Models {
    public class NthWeekdayOfMonthRules {
        public NthWeekdayOfMonthRules (int weekday, int nthWeekday) {
            Weekday = weekday;
            NthWeekday = nthWeekday;
        }

        public int Weekday { get; }
        public int NthWeekday { get; }

        public override bool Equals (object obj) {
            return obj is NthWeekdayOfMonthRules rules &&
                Weekday == rules.Weekday &&
                NthWeekday == rules.NthWeekday;
        }

        public override int GetHashCode () {
            int hashCode = -1034814;
            hashCode = hashCode * -1521134295 + Weekday.GetHashCode ();
            hashCode = hashCode * -1521134295 + NthWeekday.GetHashCode ();
            return hashCode;
        }
    }
}