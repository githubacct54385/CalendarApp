using System;
namespace CalendarApp.Core.GetCalendar.Models {
    public class RepeatRules {
        public RepeatRules (DateTime startOn, DateTime? endOn) {
            StartOn = startOn;
            EndOn = endOn;
        }
        public DateTime StartOn { get; }
        public DateTime? EndOn { get; }

        public override bool Equals (object obj) {
            return obj is RepeatRules rules &&
                StartOn == rules.StartOn &&
                EndOn == rules.EndOn;
        }

        public override int GetHashCode () {
            int hashCode = 1012203405;
            hashCode = hashCode * -1521134295 + StartOn.GetHashCode ();
            hashCode = hashCode * -1521134295 + EndOn.GetHashCode ();
            return hashCode;
        }
    }
}