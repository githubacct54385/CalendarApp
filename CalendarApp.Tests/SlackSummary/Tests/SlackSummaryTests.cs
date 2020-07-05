using System;
using System.Collections.Generic;
using CalendarApp.Core.CreateSummary;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;
using CalendarApp.Core.SlackSummary;
using CalendarApp.Core.SlackSummary.Interface;
using Moq;
using Xunit;

namespace CalendarApp.Tests.SlackSummary.Tests {
    public class SlackSummaryTests {

        [Fact]
        public void Creates_Correct_Json () {
            var expectedJson = "{\"blocks\":[{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\"*Upcoming Events*\"}},{\"type\":\"context\",\"elements\":[{\"type\":\"mrkdn\",\"text\":\"Your event summary for November 25, 2020\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\":calendar: 1 Event happening tomorrow :calendar:\"}},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\"\u006011/26/2020\u0060 Sharon Winters\u0027 Birthday --- Say Happy Birthday to Sharon\"}},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\":calendar: 2 Events happening 3 days from now :calendar:\"}},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\"\u006011/28/2020\u0060 John Doe\u0027s Birthday --- Say Happy Birthday to John\"}},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\"\u006011/28/2020\u0060 Mary Jane\u0027s Birthday --- Say Happy Birthday to Mary\"}},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\":calendar: 1 Event happening a week from now :calendar:\"}},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\"\u006012/02/2020\u0060 Peter Baker\u0027s Birthday --- Say Happy Birthday to Peter\"}},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\":calendar: 1 Event happening 2 weeks from now :calendar:\"}},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\"\u006012/09/2020\u0060 Cliff Brooks\u0027 Birthday --- Say Happy Birthday to Cliff\"}},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\":calendar: 1 Event happening one month from now :calendar:\"}},{\"type\":\"section\",\"text\":{\"type\":\"mrkdn\",\"text\":\"\u006012/25/2020\u0060 Christmas --- Say Merry Christmas\"}}]}";

            DateTime date = new DateTime (2020, 11, 25);
            var creator = new SlackSummaryCreator (DateMock (date), CalendarSummary (date));

            creator.AddBoldSection ("Upcoming Events");
            creator.AddContextHeader ();
            creator.AddDividerBlock ();
            creator.AddOneDayAwayItems ();
            creator.AddDividerBlock ();
            creator.AddThreeDaysAwayItems ();
            creator.AddDividerBlock ();
            creator.AddOneWeekAwayItems ();
            creator.AddDividerBlock ();
            creator.AddTwoWeeksAwayItems ();
            creator.AddDividerBlock ();
            creator.AddOneMonthAwayItems ();
            string actual = creator.Serialize ();

            Assert.Equal (expectedJson, actual);
        }

        private CalendarSummary CalendarSummary (DateTime date) {
            var calSummary = new CalendarSummary (SummaryItems (), DateMock (date));
            return calSummary;
        }

        private static List<CalendarSummaryItem> SummaryItems () {
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

        public static IDateProvider DateMock (DateTime date) {
            var dateMock = new Mock<IDateProvider> ();
            dateMock.Setup (x => x.GetToday ()).Returns (date);
            return dateMock.Object;
        }
    }
}