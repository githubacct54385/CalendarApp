using System.Collections.Generic;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.GetCalendar.Interface {
    public interface IDeserializedCalendarItemProvider {
        List<CalendarItemBase> GetItems ();
    }
}