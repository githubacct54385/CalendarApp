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

        private readonly CalendarSummaryItem _peterBakerBirthday = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "Cliff Brooks's Birthday",
            reminderText: "Say Happy Birthday to Cliff",
            dateOfCalItemThisYear : new DateTime (2020, 12, 09)
        );

        private readonly CalendarSummaryItem _sharonWinters = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "Sharon Winters's Birthday",
            reminderText: "Say Happy Birthday to Sharon",
            dateOfCalItemThisYear : new DateTime (2020, 11, 26)
        );

        private readonly CalendarSummaryItem _cliffBrooksBirthday = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "Peter Baker's Birthday",
            reminderText: "Say Happy Birthday to Peter",
            dateOfCalItemThisYear : new DateTime (2020, 12, 2)
        );

        private readonly CalendarSummaryItem _johnDoeBirthday = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "John Doe's Birthday",
            reminderText: "Say Happy Birthday to John",
            dateOfCalItemThisYear : new DateTime (2020, 11, 28)
        );

        private readonly CalendarSummaryItem _maryJaneBirthday = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "Mary Jane's Birthday",
            reminderText: "Say Happy Birthday to Mary",
            dateOfCalItemThisYear : new DateTime (2020, 11, 28)
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
                _maryJaneBirthday,
                _peterBakerBirthday,
                _cliffBrooksBirthday,
                _sharonWinters,
                _halloween,
                _fathersDay
            };
        }

        [Fact]
        public void CalendarSummary_Puts_CalendarSummary_Items_Into_Correct_Properties () {
            //Given
            var calendarSummary = new CalendarSummary (Items (), DateMock (new DateTime (2020, 11, 25)));

            List<CalendarSummaryItem> oneMonthItems = new List<CalendarSummaryItem> () { _christmas };
            List<CalendarSummaryItem> twoweekItems = new List<CalendarSummaryItem> () { _peterBakerBirthday };
            List<CalendarSummaryItem> oneWeekItems = new List<CalendarSummaryItem> () { _cliffBrooksBirthday };
            List<CalendarSummaryItem> threeDayItems = new List<CalendarSummaryItem> () { _johnDoeBirthday, _maryJaneBirthday };
            List<CalendarSummaryItem> oneDayItems = new List<CalendarSummaryItem> () { _sharonWinters };

            //When
            calendarSummary.Create ();

            //Then

            Assert.Equal (oneMonthItems, calendarSummary.OneMonthItemsList);
            Assert.Equal (twoweekItems, calendarSummary.TwoWeekItems);
            Assert.Equal (oneWeekItems, calendarSummary.OneWeekItems);
            Assert.Equal (threeDayItems, calendarSummary.ThreeDayItems);
            Assert.Equal (oneDayItems, calendarSummary.OneDayItems);
        }
    }
}