using System;
using System.Linq;
using CalendarApp.Core.GetCalendar;
using Xunit;

namespace CalendarApp.Tests {
    public class GetItemsNthDayOfMonthTests {

        [Theory]
        [InlineData (2, 2020, 11, 24, 2020)]
        [InlineData (2, 2020, 11, 24, 2019)]
        [InlineData (2, 2020, 09, 30, 2019)]
        [InlineData (2, 2020, 09, 30, 2018)]
        public void GetItems_Returns_No_Items (int numItems, int year, int month, int day, int mockYear) {
            //Given
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthDayOfMonthFutureDateProvider (dateProvider);
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems, mockYear);

            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            Assert.Empty (items);
        }

        [Theory]
        [InlineData (2, 0, 2020, 11, 25, 2020)]
        [InlineData (2, 0, 2020, 11, 25, 2019)]
        [InlineData (2, 1, 2020, 10, 1, 2020)]
        [InlineData (2, 1, 2020, 10, 1, 2019)]
        public void GetItems_Returns_Calendar_Item_Exactly_One_Month_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthDayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthDayOfMonthSampleData.SampleDates (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 11, 2020)]
        [InlineData (2, 1, 2020, 10, 17, 2020)]
        [InlineData (2, 0, 2020, 12, 11, 2019)]
        [InlineData (2, 1, 2020, 10, 17, 2019)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_Two_Weeks_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthDayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthDayOfMonthSampleData.SampleDates (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 18, 2020)]
        [InlineData (2, 1, 2020, 10, 24, 2020)]
        [InlineData (2, 0, 2020, 12, 18, 2019)]
        [InlineData (2, 1, 2020, 10, 24, 2019)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_One_Week_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthDayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthDayOfMonthSampleData.SampleDates (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 22, 2020)]
        [InlineData (2, 0, 2020, 12, 22, 2019)]
        [InlineData (2, 1, 2020, 10, 28, 2020)]
        [InlineData (2, 1, 2020, 10, 28, 2019)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_Three_Days_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthDayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthDayOfMonthSampleData.SampleDates (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 24, 2020)]
        [InlineData (2, 0, 2020, 12, 24, 2019)]
        [InlineData (2, 1, 2020, 10, 30, 2020)]
        [InlineData (2, 1, 2020, 10, 30, 2019)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_One_Day_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthDayOfMonthCalendarMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthDayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthDayOfMonthSampleData.SampleDates (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }
    }
}