namespace CalendarApp.Core.SlackSummary.Models {
    public sealed class SectionBlock {
        public SectionBlock (string type, string text) {
            Type = type;
            Text = text;
        }

        public string Type { get; }

        public string Text { get; }
    }
}