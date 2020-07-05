using System;
using CalendarApp.Core.PostToSlack.Interface;
using RestSharp;

namespace CalendarApp.Core.PostToSlack {
    public class SlackWriter {
        private readonly ISlackCredentialProvider _slackCredentialProvider;

        public SlackWriter (ISlackCredentialProvider slackCredentialProvider) {
            _slackCredentialProvider = slackCredentialProvider;
        }
        public void Write (string msg) {
            _slackCredentialProvider.MakePost (msg);
        }
    }
}