namespace CalendarApp.Core.GetCalendar.Models {
    public class NthDayOfMonthRules {
        public NthDayOfMonthRules (int month, int day) {
            Month = month;
            Day = day;
        }

        public int Month { get; }
        public int Day { get; }

        public override bool Equals (object obj) {
            return obj is NthDayOfMonthRules rules &&
                Month == rules.Month &&
                Day == rules.Day;
        }

        public override int GetHashCode () {
            int hashCode = 37050634;
            hashCode = hashCode * -1521134295 + Month.GetHashCode ();
            hashCode = hashCode * -1521134295 + Day.GetHashCode ();
            return hashCode;
        }
    }
}