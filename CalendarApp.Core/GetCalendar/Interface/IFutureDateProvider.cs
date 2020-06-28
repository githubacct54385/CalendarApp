using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.GetCalendar.Interface {
    public interface IFutureDateProvider {
        CalendarItem CalcFutureDate (CalendarItem calendarItem);
        CalendarItem CalcCalendarItemForThisYear (CalendarItem calendarItem);
    }
}