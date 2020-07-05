using System;
using CalendarApp.Core.GetCalendar.Interface;
using Moq;

namespace CalendarApp.Tests.GetItemsTests {
    public class InterfaceMocks {
        public static IDateProvider DateMock (DateTime date) {
            var dateMock = new Mock<IDateProvider> ();
            dateMock.Setup (x => x.GetToday ()).Returns (date);
            return dateMock.Object;
        }

        public static IDeserializedCalendarItemProvider NthDayOfMonthCalendarMock (int numItems) {
            var calendarItemsMock = new Mock<IDeserializedCalendarItemProvider> ();
            calendarItemsMock.Setup (x => x.GetItems ()).Returns (NthDayOfMonthSampleData.SampleDates (numItems));
            return calendarItemsMock.Object;
        }

        public static IDeserializedCalendarItemProvider NthWeekdayOfMonthCalendarItemMock (int numItems) {
            var calendarItemsMock = new Mock<IDeserializedCalendarItemProvider> ();
            calendarItemsMock.Setup (x => x.GetItems ()).Returns (NthWeekdayOfMonthSampleData.SampleDates (numItems));
            return calendarItemsMock.Object;
        }
    }
}