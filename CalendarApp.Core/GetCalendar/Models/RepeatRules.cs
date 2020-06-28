using System;
namespace CalendarApp.Core.GetCalendar.Models {
    public class RepeatRules {
        public RepeatRules (DateTime startOn, DateTime? endOn) {
            StartOn = startOn;
            EndOn = endOn;
        }
        public DateTime StartOn { get; }
        public DateTime? EndOn { get; }
    }
}