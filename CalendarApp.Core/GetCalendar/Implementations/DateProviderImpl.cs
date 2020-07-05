using System;
using CalendarApp.Core.GetCalendar.Interface;

namespace CalendarApp.Core.GetCalendar.Implementations {
    public class DateProviderImpl : IDateProvider {
        public DateTime GetToday () {
            return DateTime.Today;
        }
    }
}