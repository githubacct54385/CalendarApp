namespace CalendarApp.Core.GetCalendar.Models {
    public class NthWeekdayOfMonthRules {
        public NthWeekdayOfMonthRules (int weekday, int nthWeekday) {
            Weekday = weekday;
            NthWeekday = nthWeekday;
        }

        public int Weekday { get; }
        public int NthWeekday { get; }
    }
}