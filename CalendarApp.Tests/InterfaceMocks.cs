using System;
using CalendarApp.Core.GetCalendar.Interface;
using Moq;

namespace CalendarApp.Tests {
    public class InterfaceMocks {
        public static IDateProvider DateMock (DateTime date) {
            var dateMock = new Mock<IDateProvider> ();
            dateMock.Setup (x => x.GetToday ()).Returns (date);
            return dateMock.Object;
        }

        public static ICalendarItemProvider CalendarItemMock (int numItems, int year) {
            var calendarItemsMock = new Mock<ICalendarItemProvider> ();
            calendarItemsMock.Setup (x => x.GetItems ()).Returns (SampleData.GetSampleCalendarItemsWithItems (numItems, year));
            return calendarItemsMock.Object;
        }
    }
}