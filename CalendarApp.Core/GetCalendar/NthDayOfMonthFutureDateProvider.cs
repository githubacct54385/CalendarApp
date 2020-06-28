using System;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.GetCalendar {
    public class NthDayOfMonthFutureDateProvider : IFutureDateProvider {
        private readonly IDateProvider _dateProvider;
        public NthDayOfMonthFutureDateProvider (IDateProvider dateProvider) {
            _dateProvider = dateProvider;
        }

        public CalendarItem CalcCalendarItemForThisYear (CalendarItem calendarItem) {
            throw new NotImplementedException ();
        }

        public CalendarItem CalcFutureDate (CalendarItem item) {
            var repeatRules = item.RepeatRules;
            var startOn = repeatRules.StartOn;
            var date = new DateTime (_dateProvider.GetToday ().Year, startOn.Month, startOn.Day);
            var newRepeatRules = new RepeatRules (date, repeatRules.EndOn);
            return new CalendarItem (item.Id, item.Name, item.Reminder, item.RepeatsYearly, item.When, newRepeatRules, item.Reminders);
        }
    }
}