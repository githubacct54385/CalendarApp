using System.Collections.Generic;
using System.Text.Json;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.GetCalendar.Implementations {
    public class DeserializedCalendarItemProviderImpl : IDeserializedCalendarItemProvider {
        public List<CalendarItemBase> GetItems () {
            var serializedCalItems = System.Environment.GetEnvironmentVariable ("CalItemsSerialized");
            var calItems = JsonSerializer.Deserialize<List<CalendarItemBase>> (serializedCalItems);
            return calItems;
        }
    }
}