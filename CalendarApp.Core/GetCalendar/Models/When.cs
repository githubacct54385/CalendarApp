namespace CalendarApp.Core.GetCalendar.Models {
    public class When {
        public When (NthDayOfMonthRules nthDayOfMonthRules, NthWeekdayOfMonthRules nthWeekOfMonthRules) {
            NthDayOfMonthRules = nthDayOfMonthRules;
            NthWeekdayOfMonthRules = nthWeekOfMonthRules;
        }

        public NthDayOfMonthRules NthDayOfMonthRules { get; }
        public NthWeekdayOfMonthRules NthWeekdayOfMonthRules { get; }
    }
}