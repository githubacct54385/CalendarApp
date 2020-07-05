using System;
using CalendarApp.Core.PostToSlack.Interface;
using RestSharp;

namespace CalendarApp.Core.PostToSlack.Impementations {
    public sealed class SlackCredentialProviderImpl : ISlackCredentialProvider {
        private const string BASE_URL = "https://hooks.slack.com";

        public void MakePost (string msg) {
            var client = new RestClient (BASE_URL);
            var request = new RestRequest ($"services/{WebhookSecret()}", Method.POST);
            request.AddJsonBody (msg);
            try {
                var res = client.Execute (request);
                if (!res.IsSuccessful) {
                    throw new Exception ($"Restsharp connection to Slack did not work.  {res.ErrorMessage}");
                }

            } catch (Exception ex) {
                Console.WriteLine (ex.Message);
                Console.WriteLine (ex.StackTrace);
            }
        }

        public string WebhookSecret () {
            return System.Environment.GetEnvironmentVariable ("SlackWebhook");
        }
    }
}