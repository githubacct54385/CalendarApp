using System.Collections.Generic;
using System.Linq;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.CreateSummary {
    public class CalendarSummary {
        public List<CalendarSummaryItem> Items { get; }
        public List<CalendarSummaryItem> OneMonthItemsList { get; set; } = new List<CalendarSummaryItem> ();
        public List<CalendarSummaryItem> TwoWeekItems { get; set; } = new List<CalendarSummaryItem> ();
        public List<CalendarSummaryItem> OneWeekItems { get; set; } = new List<CalendarSummaryItem> ();
        public List<CalendarSummaryItem> ThreeDayItems { get; set; } = new List<CalendarSummaryItem> ();
        public List<CalendarSummaryItem> OneDayItems { get; set; } = new List<CalendarSummaryItem> ();

        private readonly IDateProvider _dateProvider;
        public CalendarSummary (List<CalendarSummaryItem> items, IDateProvider dateProvider) {
            this.Items = items;
            _dateProvider = dateProvider;
            Create ();
        }

        private void Create () {
            this.OneMonthItemsList.AddRange (GetItems (daysAway: 30));
            this.TwoWeekItems.AddRange (GetItems (daysAway: 14));
            this.OneWeekItems.AddRange (GetItems (daysAway: 7));
            this.ThreeDayItems.AddRange (GetItems (daysAway: 3));
            this.OneDayItems.AddRange (GetItems (daysAway: 1));
        }

        private List<CalendarSummaryItem> GetItems (int daysAway) {
            return this.Items.Where (calItem => {
                if (_dateProvider.GetToday ().AddDays (daysAway) == calItem.DateOfCalItemThisYear) {
                    return true;
                }
                return false;
            }).ToList ();
        }
    }
}