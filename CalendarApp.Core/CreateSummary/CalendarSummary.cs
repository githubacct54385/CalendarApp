using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.CreateSummary {
    public class CalendarSummary {
        public List<CalendarApp.Core.GetCalendar.Models.CalendarItem> Items { get; }
        public List<CalendarItem> OneMonthItems { get; set; } = new List<CalendarItem> ();
        public List<CalendarItem> TwoWeekItems { get; set; } = new List<CalendarItem> ();
        public List<CalendarItem> OneWeekItems { get; set; } = new List<CalendarItem> ();
        public List<CalendarItem> ThreeDayItems { get; set; } = new List<CalendarItem> ();
        public List<CalendarItem> OneDayItems { get; set; } = new List<CalendarItem> ();

        private readonly IDateProvider _dateProvider;
        public CalendarSummary (List<CalendarApp.Core.GetCalendar.Models.CalendarItem> items, IDateProvider dateProvider) {
            this.Items = items;
            _dateProvider = dateProvider;
        }

        public void Create () {
            this.OneMonthItems = this.Items.Where (item => {
                if (item.RepeatRules.StartOn.AddDays (30) >= _dateProvider.GetToday ()) {
                    return true;
                }
                return false;
            }).ToList ();
        }
    }
}