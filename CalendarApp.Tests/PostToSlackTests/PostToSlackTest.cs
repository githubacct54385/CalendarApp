using CalendarApp.Core.PostToSlack;
using CalendarApp.Core.PostToSlack.Interface;
using Moq;
using Xunit;

namespace CalendarApp.Tests.PostToSlackTests {
    public class PostToSlackTest {
        [Fact]
        public void SlackWriter_Write_Is_Called_Once () {
            var samplePayload = "{\"blocks\":[{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\"*Upcoming Events*\"}},{\"type\":\"context\",\"elements\":[{\"type\":\"mrkdwn\",\"text\":\"Your event summary for November 25, 2020\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\":calendar: 1 Event happening tomorrow\"}},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n11/26/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nSharon Winters Birthday\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\":calendar: 2 Events happening 3 days from now\"}},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n11/28/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nJohn Doe Birthday\"}]},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n11/28/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nMary Jane Birthday\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\":calendar: 1 Event happening 1 week from now\"}},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n12/02/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nPeter Baker Birthday\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\":calendar: 1 Event happening 2 weeks from now\"}},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n12/09/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nCliff Brooks Birthday\"}]},{\"type\":\"divider\"},{\"type\":\"section\",\"text\":{\"type\":\"mrkdwn\",\"text\":\":calendar: 1 Event happening 1 month from now\"}},{\"type\":\"section\",\"fields\":[{\"type\":\"mrkdwn\",\"text\":\"*When*\n12/25/2020\"},{\"type\":\"mrkdwn\",\"text\":\"*Event*\nChristmas\"}]}]}";

            var mock = SlackCredentialMock ();
            var slackWriter = new SlackWriter (mock.Object);
            slackWriter.Write (samplePayload);

            mock.Verify (x => x.MakePost (It.IsAny<string> ()), Times.Once);
        }

        private Mock<ISlackCredentialProvider> SlackCredentialMock () {
            var mock = new Mock<ISlackCredentialProvider> ();
            mock.Setup (x => x.WebhookSecret ()).Returns ("123abc");
            mock.Setup (x => x.MakePost (It.IsAny<string> ()));
            return mock;
        }
    }
}