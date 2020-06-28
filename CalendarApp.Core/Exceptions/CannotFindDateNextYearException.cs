using System;

namespace CalendarApp.Core.Exceptions {
    public class CannotFindDateNextYearException : Exception {
        public CannotFindDateNextYearException (string message) : base (message) { }
    }
}