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

        private ICalendarItemProvider CalendarItemMock (int numItems, int year) {
            var calendarItemsMock = new Mock<ICalendarItemProvider> ();
            calendarItemsMock.Setup (x => x.GetItems ()).Returns (GetSampleCalendarItemsWithItems (numItems, year));
            return calendarItemsMock.Object;
        }

        private List<CalendarItem> GetSampleCalendarItemsWithItems (int numItems, int year) {
            switch (numItems) {
                case 1:
                    return new List<CalendarItem> () {
                        Christmas (year)
                    };
                case 2:
                    return new List<CalendarItem> () {
                        Christmas (year), Halloween (year)
                    };
                default:
                    throw new Exception ("Unknown Num items.");
            }
        }

        private CalendarItem Christmas (int year) {
            return new CalendarItem (
                id: Guid.Parse ("71b81643-8f88-425a-b4d9-03cecb8108d5"),
                name: "Christmas",
                reminder: "Say Merry Christmas to your friends and family.",
                repeatsYearly : true,
                when : new When (new NthDayOfMonthRules (12, 25), nthWeekOfMonthRules : null),
                repeatRules : new RepeatRules (startOn: new DateTime (year, 12, 25), endOn : null),
                reminders: "1d,3d,7d,2w,1m"
            );
        }

        private CalendarItem Halloween (int year) {
            return new CalendarItem (
                id: Guid.Parse ("22884935-6378-4a8a-93ba-6cebb992bb97"),
                name: "Halloween",
                reminder: "Go trick or treating and be spooky!",
                repeatsYearly : true,
                when : new When (new NthDayOfMonthRules (10, 31), nthWeekOfMonthRules : null),
                repeatRules : new RepeatRules (startOn: new DateTime (year, 10, 31), endOn : null),
                reminders: "1d,3d,7d,2w,1m"
            );
        }

        [Theory]
        [InlineData (1, 2020, 11, 24, 2020)]
        [InlineData (1, 2020, 11, 24, 2019)]
        [InlineData (2, 2020, 09, 30, 2019)]
        [InlineData (2, 2020, 09, 30, 2018)]
        public void GetItems_Returns_No_Items (int numItems, int year, int month, int day, int mockYear) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems, mockYear), DateMock (new DateTime (year, month, day)));

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
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems, mockYear), DateMock (new DateTime (year, month, day)));

            //When
            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = GetSampleCalendarItemsWithItems (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 11, 2020)]
        [InlineData (2, 1, 2020, 10, 17, 2020)]
        [InlineData (2, 0, 2020, 12, 11, 2019)]
        [InlineData (2, 1, 2020, 10, 17, 2019)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_Two_Weeks_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems, mockYear), DateMock (new DateTime (year, month, day)));

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = GetSampleCalendarItemsWithItems (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 18, 2020)]
        [InlineData (2, 1, 2020, 10, 24, 2020)]
        [InlineData (2, 0, 2020, 12, 18, 2019)]
        [InlineData (2, 1, 2020, 10, 24, 2019)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_One_Week_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems, mockYear), DateMock (new DateTime (year, month, day)));

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = GetSampleCalendarItemsWithItems (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 22, 2020)]
        [InlineData (2, 0, 2020, 12, 22, 2019)]
        [InlineData (2, 1, 2020, 10, 28, 2020)]
        [InlineData (2, 1, 2020, 10, 28, 2019)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_Three_Days_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems, mockYear), DateMock (new DateTime (year, month, day)));

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = GetSampleCalendarItemsWithItems (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }

        [Theory]
        [InlineData (2, 0, 2020, 12, 24, 2020)]
        [InlineData (2, 0, 2020, 12, 24, 2019)]
        [InlineData (2, 1, 2020, 10, 30, 2020)]
        [InlineData (2, 1, 2020, 10, 30, 2019)]
        public void GetItems_Returns_Christmas_One_Item_Exactly_One_Day_Away (int numItems, int index, int year, int month, int day, int mockYear) {
            //Given
            var getCalendarItems = new GetCalendarItems (CalendarItemMock (numItems, mockYear), DateMock (new DateTime (year, month, day)));

            var items = getCalendarItems.GetItems ();

            //Then
            var actual = items.First ();
            var expected = GetSampleCalendarItemsWithItems (numItems, mockYear) [index];
            Assert.True (expected.EqualsWithoutRepeatRules (actual));
        }
    }
}