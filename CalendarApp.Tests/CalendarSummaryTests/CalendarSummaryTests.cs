using System;
using System.Collections.Generic;
using CalendarApp.Core.CreateSummary;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;
using Moq;
using Xunit;

namespace CalendarApp.Tests.CalendarSummaryTests {
    public class CalendarSummaryTests {
        private readonly CalendarSummaryItem _christmas = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "Christmas",
            reminderText: "Say Merry Christmas",
            dateOfCalItemThisYear : new DateTime (2020, 12, 25)
        );

        private readonly CalendarSummaryItem _johnDoeBirthday = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "John Doe's Birthday",
            reminderText: "Say Happy Birthday to John",
            dateOfCalItemThisYear : new DateTime (2020, 12, 23)
        );

        private readonly CalendarSummaryItem _halloween = new CalendarSummaryItem (
            id: Guid.Parse ("22884935-6378-4a8a-93ba-6cebb992bb97"),
            name: "Halloween",
            reminderText: "Go trick or treating and be spooky!",
            dateOfCalItemThisYear : new DateTime (2020, 10, 31)
        );
        private readonly CalendarSummaryItem _fathersDay = new CalendarSummaryItem (
            id: Guid.Parse ("600de851-378c-4c59-baa5-5d1503206545"),
            name: "Fathers Day",
            reminderText: "Call your dad.",
            dateOfCalItemThisYear : new DateTime (2020, 6, 21)
        );

        public static IDateProvider DateMock (DateTime date) {
            var dateMock = new Mock<IDateProvider> ();
            dateMock.Setup (x => x.GetToday ()).Returns (date);
            return dateMock.Object;
        }

        public List<CalendarSummaryItem> Items () {
            return new List<CalendarSummaryItem> () {
                _christmas,
                _johnDoeBirthday,
                _halloween,
                _fathersDay
            };
        }

        [Fact]
        public void CalendarSummary_Puts_One_Month_Items_In_OneMonthItems_Property () {
            //Given
            var calendarSummary = new CalendarSummary (Items (), DateMock (new DateTime (2020, 11, 25)));
            var expected = new List<CalendarSummaryItem> () { _christmas };

            //When
            calendarSummary.Create ();

            //Then
            Assert.Equal (expected, calendarSummary.OneMonthItems);
            Assert.Empty (calendarSummary.TwoWeekItems);
            Assert.Empty (calendarSummary.OneWeekItems);
            Assert.Empty (calendarSummary.ThreeDayItems);
            Assert.Empty (calendarSummary.OneDayItems);
        }
    }
}