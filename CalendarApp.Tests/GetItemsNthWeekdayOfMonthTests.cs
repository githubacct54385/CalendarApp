using System;
using System.Linq;
using CalendarApp.Core.GetCalendar;
using Xunit;

namespace CalendarApp.Tests {
    public class GetItemsNthWeekdayOfMonthTests {

        [Theory]
        [InlineData (2, 2020, 04, 09, 2020)]
        [InlineData (2, 2020, 05, 21, 2020)]
        public void GetItems_Returns_No_Items (int numItems, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthWeekdayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            var items = getCalendarItems.GetItems ();

            //Then
            Assert.Empty (items);
        }

        [Theory]
        [InlineData (2, 0, 2020, 04, 10, 2020)] // correct
        [InlineData (2, 0, 2019, 04, 12, 2019)]
        [InlineData (2, 1, 2020, 05, 22, 2020)] // correct
        [InlineData (2, 1, 2019, 05, 17, 2019)]
        public void GetItems_Returns_Calendar_Item_Exactly_One_Month_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthWeekdayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthWeekdayOfMonthSampleData.SampleDates (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 04, 26, 2020)] // correct
        [InlineData (2, 0, 2019, 04, 28, 2019)]
        [InlineData (2, 1, 2020, 06, 07, 2020)] // correct
        [InlineData (2, 1, 2019, 06, 02, 2019)]
        public void GetItems_Returns_Calendar_Item_Exactly_Two_Weeks_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthWeekdayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthWeekdayOfMonthSampleData.SampleDates (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 05, 10 - 7, 2020)] // correct
        [InlineData (2, 0, 2019, 05, 12 - 7, 2019)]
        [InlineData (2, 1, 2020, 06, 21 - 7, 2020)] // correct
        [InlineData (2, 1, 2019, 06, 16 - 7, 2019)]
        public void GetItems_Returns_Calendar_Item_Exactly_One_Week_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthWeekdayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthWeekdayOfMonthSampleData.SampleDates (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 05, 10 - 3, 2020)] // correct
        [InlineData (2, 0, 2019, 05, 12 - 3, 2019)]
        [InlineData (2, 1, 2020, 06, 21 - 3, 2020)] // correct
        [InlineData (2, 1, 2019, 06, 16 - 3, 2019)]
        public void GetItems_Returns_Calendar_Item_Exactly_Three_Days_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthWeekdayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthWeekdayOfMonthSampleData.SampleDates (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 05, 10 - 1, 2020)] // correct
        [InlineData (2, 0, 2019, 05, 12 - 1, 2019)]
        [InlineData (2, 1, 2020, 06, 21 - 1, 2020)] // correct
        [InlineData (2, 1, 2019, 06, 16 - 1, 2019)]
        public void GetItems_Returns_Calendar_Item_Exactly_One_Day_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var calendarItemProvider = InterfaceMocks.NthWeekdayOfMonthCalendarItemMock (numItems, mockYear);
            var dateProvider = InterfaceMocks.DateMock (new DateTime (year, month, day));
            var futureDateProvider = new NthWeekdayOfMonthFutureDateProvider (dateProvider);
            var getCalendarItems = new GetCalendarItems (calendarItemProvider, dateProvider, futureDateProvider);

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = NthWeekdayOfMonthSampleData.SampleDates (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }
    }
}