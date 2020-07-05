using System.Collections.Generic;
namespace CalendarApp.Core.SlackSummary.Models {
    public class Block {
        public Block (string type, SectionBlock text, List<Element> elements, List<Field> fields) {
            Type = type;
            Text = text;
            Elements = elements;
            Fields = fields;
        }

        public Block (bool isDividerBlock) {
            Type = "divider";
            Text = null;
            Elements = null;
            Fields = null;
        }

        public string Type { get; }
        public SectionBlock Text { get; }
        public List<Element> Elements { get; }
        public List<Field> Fields { get; }
    }
}