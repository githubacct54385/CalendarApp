using System;
using System.Linq;
using CalendarApp.Core.GetCalendar;
using CalendarApp.Core.GetCalendar.Models;
using Xunit;

namespace CalendarApp.Tests {
    public class GetItemsNthDayOfMonthTests {

        [Theory]
        [InlineData (2, 2020, 11, 24)]
        [InlineData (2, 2020, 09, 30)]
        public void GetItems_Returns_No_Items (int numItems, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            Assert.Empty (items);
        }

        [Theory]
        [InlineData (2, 0, 2020, 11, 25)]
        [InlineData (2, 1, 2020, 10, 1)]
        public void GetItems_Returns_Calendar_Item_Exactly_One_Month_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthDayOfMonthSampleData.SampleDates (numItems) [index];
            Assert.True (IsEqual (expected as NthDayOfMonthCalendarItem, actual, year));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 11)]
        [InlineData (2, 1, 2020, 10, 17)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_Two_Weeks_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthDayOfMonthSampleData.SampleDates (numItems) [index];
            Assert.True (IsEqual (expected as NthDayOfMonthCalendarItem, actual, year));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 18)]
        [InlineData (2, 1, 2020, 10, 24)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_One_Week_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthDayOfMonthSampleData.SampleDates (numItems) [index];
            Assert.True (IsEqual (expected as NthDayOfMonthCalendarItem, actual, year));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 22)]
        [InlineData (2, 1, 2020, 10, 28)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_Three_Days_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthDayOfMonthSampleData.SampleDates (numItems) [index];
            Assert.True (IsEqual (expected as NthDayOfMonthCalendarItem, actual, year));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 24)]
        [InlineData (2, 1, 2020, 10, 30)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_One_Day_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthDayOfMonthSampleData.SampleDates (numItems) [index];
            Assert.True (IsEqual (expected as NthDayOfMonthCalendarItem, actual, year));
        }

        // helper method for testing
        private bool IsEqual (NthDayOfMonthCalendarItem expected, CalendarSummaryItem actual, int expectedYear) {

            return expected.Id == actual.Id &&
                expected.Name == actual.Name &&
                expected.ReminderText == actual.ReminderText &&
                expectedYear == actual.DateOfCalItemThisYear.Year &&
                expected.Month == actual.DateOfCalItemThisYear.Month &&
                expected.Day == actual.DateOfCalItemThisYear.Day;
        }
    }
}