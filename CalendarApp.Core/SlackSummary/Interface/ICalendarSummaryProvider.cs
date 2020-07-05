using CalendarApp.Core.CreateSummary;

namespace CalendarApp.Core.SlackSummary.Interface {
    public interface ICalendarSummaryProvider {
        CalendarSummary GetSummary ();
    }
}