using System.Collections.Generic;
using System.Linq;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Core.CreateSummary {
    public class CalendarSummary {
        public List<CalendarSummaryItem> Items { get; }
        public List<CalendarSummaryItem> OneMonthItems { get; set; } = new List<CalendarSummaryItem> ();
        public List<CalendarSummaryItem> TwoWeekItems { get; set; } = new List<CalendarSummaryItem> ();
        public List<CalendarSummaryItem> OneWeekItems { get; set; } = new List<CalendarSummaryItem> ();
        public List<CalendarSummaryItem> ThreeDayItems { get; set; } = new List<CalendarSummaryItem> ();
        public List<CalendarSummaryItem> OneDayItems { get; set; } = new List<CalendarSummaryItem> ();

        private readonly IDateProvider _dateProvider;
        public CalendarSummary (List<CalendarSummaryItem> items, IDateProvider dateProvider) {
            this.Items = items;
            _dateProvider = dateProvider;
        }

        public void Create () {
            var today = _dateProvider.GetToday ();

            var oneMonthOutItems = this.Items
                .Select (calItem => {
                    if (today.AddDays (30) == calItem.DateOfCalItemThisYear) {
                        return calItem;
                    }
                    return null;
                }).Where (p => p != null).ToList ();

            this.OneMonthItems.AddRange (oneMonthOutItems);
        }
    }
}