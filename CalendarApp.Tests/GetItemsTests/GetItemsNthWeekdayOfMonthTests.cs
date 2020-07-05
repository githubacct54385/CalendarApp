using System;
using System.Linq;
using CalendarApp.Core.GetCalendar;
using CalendarApp.Core.GetCalendar.Models;
using Xunit;

namespace CalendarApp.Tests.GetItemsTests {
    public class GetItemsNthWeekdayOfMonthTests {

        [Theory]
        [InlineData (2, 2020, 04, 09)]
        [InlineData (2, 2020, 05, 21)]
        public void GetItems_Returns_No_Items (int numItems, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            Assert.Empty (items);
        }

        [Theory]
        [InlineData (2, 0, 2020, 04, 10)] // correct
        [InlineData (2, 1, 2020, 05, 22)] // correct
        public void GetItems_Returns_Calendar_Item_Exactly_One_Month_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthWeekdayOfMonthSampleData.SampleDates (numItems) [index];
            Assert.True (IsEqual (expected as NthWeekdayOfMonthCalendarItem, actual, year));
        }

        [Theory]
        [InlineData (2, 0, 2020, 04, 26)] // correct
        [InlineData (2, 1, 2020, 06, 07)] // correct
        public void GetItems_Returns_Calendar_Item_Exactly_Two_Weeks_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthWeekdayOfMonthSampleData.SampleDates (numItems) [index];
            // Assert.Equal (expected.ToCalendarSummaryItem (), actual);
            Assert.True (IsEqual (expected as NthWeekdayOfMonthCalendarItem, actual, year));
        }

        [Theory]
        [InlineData (2, 0, 2020, 05, 10 - 7)] // correct
        [InlineData (2, 1, 2020, 06, 21 - 7)] // correct
        public void GetItems_Returns_Calendar_Item_Exactly_One_Week_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthWeekdayOfMonthSampleData.SampleDates (numItems) [index];
            Assert.True (IsEqual (expected as NthWeekdayOfMonthCalendarItem, actual, year));

        }

        [Theory]
        [InlineData (2, 0, 2020, 05, 10 - 3)] // correct
        [InlineData (2, 1, 2020, 06, 21 - 3)] // correct
        public void GetItems_Returns_Calendar_Item_Exactly_Three_Days_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthWeekdayOfMonthSampleData.SampleDates (numItems) [index];
            Assert.True (IsEqual (expected as NthWeekdayOfMonthCalendarItem, actual, year));
        }

        [Theory]
        [InlineData (2, 0, 2020, 05, 10 - 1)] // correct
        [InlineData (2, 1, 2020, 06, 21 - 1)] // correct
        public void GetItems_Returns_Calendar_Item_Exactly_One_Day_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthWeekdayOfMonthSampleData.SampleDates (numItems) [index];
            Assert.True (IsEqual (expected as NthWeekdayOfMonthCalendarItem, actual, year));

        }

        // helper method for testing
        private bool IsEqual (NthWeekdayOfMonthCalendarItem expected, CalendarSummaryItem actual, int expectedYear) {
            var calculatedDay = expected.CalculateCalendarDateForYear (expectedYear);
            return expected.Id == actual.Id &&
                expected.Name == actual.Name &&
                expected.ReminderText == actual.ReminderText &&
                calculatedDay == actual.DateOfCalItemThisYear;
        }
    }
}