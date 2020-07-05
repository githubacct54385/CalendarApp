namespace CalendarApp.Core.SlackSummary.Models {
    public class Field {
        public Field (string type, string header, string value) {
            Type = type;
            Text = $"*{header}*\n{value}";
        }

        public string Type { get; }
        public string Text { get; }
    }
}