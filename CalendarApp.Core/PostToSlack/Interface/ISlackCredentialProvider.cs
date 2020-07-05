namespace CalendarApp.Core.PostToSlack.Interface {
    public interface ISlackCredentialProvider {
        string WebhookSecret ();
        void MakePost (string msg);
    }
}