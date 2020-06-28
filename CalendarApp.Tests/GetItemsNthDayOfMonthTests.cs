using System;
using System.Collections.Generic;
using System.Linq;
using CalendarApp.Core.GetCalendar;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;
using Moq;
using Xunit;

namespace CalendarApp.Tests {
    public class GetItemsNthDayOfMonthTests {
        private IDateProvider DateMock (DateTime date) {
            var dateMock = new Mock<IDateProvider> ();
            dateMock.Setup (x => x.GetToday ()).Returns (date);
            return dateMock.Object;
        }

        private ICalendarItemProvider CalendarItemMock (int numItems) {
            var calendarItemsMock = new Mock<ICalendarItemProvider> ();
            calendarItemsMock.Setup (x => x.GetItems ()).Returns (GetSampleCalendarItemsWithItems (numItems));
            return calendarItemsMock.Object;
        }

        private List<CalendarItem> GetSampleCalendarItemsWithItems (int numItems) {
            switch (numItems) {
                case 1:
                    return new List<CalendarItem> () {
                        Christmas ()
                    };
                case 2:
                    return new List<CalendarItem> () {
                        Christmas (), Halloween ()
                    };
                default:
                    throw new Exception ("Unknown Num items.");
            }
        }

        private CalendarItem Christmas () {
            return new CalendarItem (
                id: Guid.Parse ("c49b92b2-55ee-4581-a866-06fa7e274a84"),
                name: "Christmas",
                reminder: "Say Merry Christmas to your friends and family.",
                repeatsYearly : true,
                when : new When (new NthDayOfMonthRules (12, 25), nthWeekOfMonthRules : null),
                repeatRules : new RepeatRules (startOn: new DateTime (2020, 12, 25), endOn : null),
                reminders: "1d,3d,7d,2w,1m"
            );
        }

        private CalendarItem Halloween () {
            return new CalendarItem (
                id: Guid.Parse ("22884935-6378-4a8a-93ba-6cebb992bb97"),
                name: "Halloween",
                reminder: "Go trick or treating and be spooky!",
                repeatsYearly : true,
                when : new When (new NthDayOfMonthRules (10, 31), nthWeekOfMonthRules : null),
                repeatRules : new RepeatRules (startOn: new DateTime (2020, 10, 31), endOn : null),
                reminders: "1d,3d,7d,2w,1m"
            );
        }

        [Theory]
        [InlineData (1, 2020, 11, 24)]
        [InlineData (2, 2020, 09, 30)]
        public void GetItems_Returns_No_Items (int numItems, int year, int month, int day) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems), DateMock (new DateTime (year, month, day)));

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            Assert.Empty (items);
        }

        [Theory]
        [InlineData (2, 0, 2020, 11, 25)]
        [InlineData (2, 1, 2020, 10, 1)]
        public void GetItems_Returns_Calendar_Item_Exactly_One_Month_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems), DateMock (new DateTime (year, month, day)));

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = GetSampleCalendarItemsWithItems (numItems) [index];
            Assert.Equal (expected, actual);
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 11)]
        [InlineData (2, 1, 2020, 10, 17)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_Two_Weeks_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems), DateMock (new DateTime (year, month, day)));

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = GetSampleCalendarItemsWithItems (numItems) [index];
            Assert.Equal (expected, actual);
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 18)]
        [InlineData (2, 1, 2020, 10, 24)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_One_Week_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems), DateMock (new DateTime (year, month, day)));

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = GetSampleCalendarItemsWithItems (numItems) [index];
            Assert.Equal (expected, actual);
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 22)]
        [InlineData (2, 1, 2020, 10, 28)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_Three_Days_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems), DateMock (new DateTime (year, month, day)));

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = GetSampleCalendarItemsWithItems (numItems) [index];
            Assert.Equal (expected, actual);
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 24)]
        [InlineData (2, 1, 2020, 10, 30)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_One_Day_Away (int numItems, int index, int year, int month, int day) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems), DateMock (new DateTime (year, month, day)));

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = GetSampleCalendarItemsWithItems (numItems) [index];
            Assert.Equal (expected, actual);
        }
    }
}