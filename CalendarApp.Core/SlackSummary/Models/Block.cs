using System.Collections.Generic;
namespace CalendarApp.Core.SlackSummary.Models {
    public class Block {
        public Block (string type, SectionBlock text, List<Element> elements) {
            Type = type;
            Text = text;
            Elements = elements;
        }

        public Block (bool isDividerBlock) {
            Type = "divider";
            Text = null;
            Elements = null;
        }

        public string Type { get; }
        public SectionBlock Text { get; }
        public List<Element> Elements { get; }
    }
}