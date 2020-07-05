using System.Collections.Generic;

namespace CalendarApp.Core.SlackSummary.Models {
    public class SlackJsonModel {
        public SlackJsonModel () {
            Blocks = new List<Block> ();
        }

        public List<Block> Blocks { get; }
    }
}