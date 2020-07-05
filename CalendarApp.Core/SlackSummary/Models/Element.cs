namespace CalendarApp.Core.SlackSummary.Models {
    public class Element {
        public string Type { get; }
        public string Text { get; }
        public Element () { }

        public Element (string type, string text) {
            Type = type;
            Text = text;
        }
    }
}