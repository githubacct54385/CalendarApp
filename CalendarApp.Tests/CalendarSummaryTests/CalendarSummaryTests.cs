using System;
using System.Collections.Generic;
using CalendarApp.Core.CreateSummary;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;
using Moq;
using Xunit;

namespace CalendarApp.Tests.CalendarSummaryTests {
    public class CalendarSummaryTests {

        public static IDateProvider DateMock (DateTime date) {
            var dateMock = new Mock<IDateProvider> ();
            dateMock.Setup (x => x.GetToday ()).Returns (date);
            return dateMock.Object;
        }

        private static List<CalendarSummaryItem> SampleCalendarItems () {
            return new List<CalendarSummaryItem> () {
                SampleData.SampleCalendarSummary.Christmas,
                    SampleData.SampleCalendarSummary.FathersDay,
                    SampleData.SampleCalendarSummary.Halloween,
                    SampleData.SampleCalendarSummary.CliffBrooksBirthday,
                    SampleData.SampleCalendarSummary.JohnDoesBirthday,
                    SampleData.SampleCalendarSummary.MaryJanesBirthday,
                    SampleData.SampleCalendarSummary.PeterBakersBirthday,
                    SampleData.SampleCalendarSummary.SharonWintersBirthday,
            };
        }

        [Fact]
        public void CalendarSummary_Puts_CalendarSummary_Items_Into_Correct_Properties () {
            //Given
            var calendarSummary = new CalendarSummary (SampleCalendarItems (), DateMock (new DateTime (2020, 11, 25)));

            List<CalendarSummaryItem> oneMonthItems = new List<CalendarSummaryItem> () { SampleData.SampleCalendarSummary.Christmas };
            List<CalendarSummaryItem> twoweekItems = new List<CalendarSummaryItem> () { SampleData.SampleCalendarSummary.PeterBakersBirthday };
            List<CalendarSummaryItem> oneWeekItems = new List<CalendarSummaryItem> () { SampleData.SampleCalendarSummary.CliffBrooksBirthday };
            List<CalendarSummaryItem> threeDayItems = new List<CalendarSummaryItem> () { SampleData.SampleCalendarSummary.JohnDoesBirthday, SampleData.SampleCalendarSummary.MaryJanesBirthday };
            List<CalendarSummaryItem> oneDayItems = new List<CalendarSummaryItem> () { SampleData.SampleCalendarSummary.SharonWintersBirthday };

            //When
            // calendarSummary.Create ();

            //Then

            Assert.Equal (oneMonthItems, calendarSummary.OneMonthItemsList);
            Assert.Equal (twoweekItems, calendarSummary.TwoWeekItems);
            Assert.Equal (oneWeekItems, calendarSummary.OneWeekItems);
            Assert.Equal (threeDayItems, calendarSummary.ThreeDayItems);
            Assert.Equal (oneDayItems, calendarSummary.OneDayItems);
        }
    }
}