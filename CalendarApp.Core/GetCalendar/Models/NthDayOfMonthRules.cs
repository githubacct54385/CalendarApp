namespace CalendarApp.Core.GetCalendar.Models {
    public class NthDayOfMonthRules {
        public NthDayOfMonthRules (int month, int day) {
            Month = month;
            Day = day;
        }

        public int Month { get; }
        public int Day { get; }
    }
}