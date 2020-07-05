using System;
using CalendarApp.Core.GetCalendar;
using CalendarApp.Core.GetCalendar.Implementations;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CalendarApp.Function {
    public static class CalendarAppFunction {
        [FunctionName ("CalendarAppFunction")]
        public static void Run ([TimerTrigger ("0 */5 * * * *")] TimerInfo myTimer, ILogger log) {
            log.LogInformation ($"C# Timer trigger function executed at: {DateTime.Now}");

            var calItemsProvider = new DeserializedCalendarItemProviderImpl ();
            var dateProvider = new DateProviderImpl ();
            var getCalendarItems = new GetCalendarItems (calItemsProvider, dateProvider);
            var items = getCalendarItems.GetItems ();
        }
    }
}