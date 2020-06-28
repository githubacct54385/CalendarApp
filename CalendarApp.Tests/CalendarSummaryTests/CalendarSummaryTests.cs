using System;
using System.Collections.Generic;
using CalendarApp.Core.CreateSummary;
using CalendarApp.Core.CreateSummary.Models;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;
using Moq;
using Xunit;

namespace CalendarApp.Tests.CalendarSummaryTests {
    public class CalendarSummaryTests {
        private readonly CalendarItem _christmas = new CalendarItem (
            id: Guid.NewGuid (),
            name: "Christmas",
            reminder: "Say Merry Christmas",
            repeatsYearly : true,
            when : new When (new NthDayOfMonthRules (12, 25), null),
            repeatRules : new RepeatRules (new DateTime (2020, 12, 25), null),
            reminders: "1d,3d,7d,2w,1m"
        );
        private readonly CalendarItem _halloween = new CalendarItem (
            id: Guid.Parse ("22884935-6378-4a8a-93ba-6cebb992bb97"),
            name: "Halloween",
            reminder: "Go trick or treating and be spooky!",
            repeatsYearly : true,
            when : new When (new NthDayOfMonthRules (10, 31), nthWeekdayOfMonthRules : null),
            repeatRules : new RepeatRules (startOn: new DateTime (2020, 10, 31), endOn : null),
            reminders: "1d,3d,7d,2w,1m"
        );
        private readonly CalendarItem _fathersDay = new CalendarItem (
            id: Guid.Parse ("600de851-378c-4c59-baa5-5d1503206545"),
            name: "Fathers Day",
            reminder: "Call your dad.",
            repeatsYearly : true,
            when : new When (nthDayOfMonthRules: null, nthWeekdayOfMonthRules: new NthWeekdayOfMonthRules (weekday : DayOfWeek.Sunday, nthWeekday : 3)),
            repeatRules : new RepeatRules (startOn: new DateTime (2020, 06, 21), endOn : null),
            reminders: "1d,3d,7d,2w,1m"
        );

        public static IDateProvider DateMock (DateTime date) {
            var dateMock = new Mock<IDateProvider> ();
            dateMock.Setup (x => x.GetToday ()).Returns (date);
            return dateMock.Object;
        }

        public List<CalendarItem> Items () {
            return new List<CalendarItem> () {
                _christmas,
                _halloween,
                _fathersDay
            };
        }

        [Fact]
        public void CalendarSummary_Puts_One_Month_ () {
            //Given
            var calendarSummary = new CalendarSummary (Items (), DateMock (new DateTime (2020, 12, 01)));
            var expected = new List<CalendarItem> () { _christmas };

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