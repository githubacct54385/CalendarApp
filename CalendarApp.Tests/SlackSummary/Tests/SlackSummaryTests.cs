using System;
using System.Collections.Generic;
using CalendarApp.Core.CreateSummary;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;
using CalendarApp.Core.SlackSummary;
using Moq;
using Xunit;

namespace CalendarApp.Tests.SlackSummary.Tests {
    public class SlackSummaryTests {

        [Fact]
        public void Creates_Correct_Json () {
            var expectedJson = "{\"blocks\":[{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\"*Upcoming Events*\"}},{\"type\":\"context\",\"elements\":[{\"type\":\"mrkdwn\",\"text\":\"Your event summary for November 25, 2020\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\":calendar: 1 Event happening tomorrow\"}},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n11/26/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nSharon Winters Birthday\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\":calendar: 2 Events happening 3 days from now\"}},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n11/28/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nJohn Doe Birthday\"}]},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n11/28/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nMary Jane Birthday\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\":calendar: 1 Event happening 1 week from now\"}},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n12/02/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nPeter Baker Birthday\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\":calendar: 1 Event happening 2 weeks from now\"}},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n12/09/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nCliff Brooks Birthday\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\":calendar: 1 Event happening 1 month from now\"}},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n12/25/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nChristmas\"}]}]}";

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