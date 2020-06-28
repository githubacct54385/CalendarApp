using System;

namespace CalendarApp.Core.GetCalendar.Interface {
    public interface IDateProvider {
        DateTime GetToday ();
    }
}