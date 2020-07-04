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

            var oneMonthOutItems = this.Items.Where (calItem => {
                if (today.AddDays (30) == calItem.DateOfCalItemThisYear) {
                    return true;
                }
                return false;
            }).ToList ();

            var twoWeekOutItems = this.Items
                .Where (calItem => {
                    if (today.AddDays (14) == calItem.DateOfCalItemThisYear) {
                        return true;
                    }
                    return false;
                }).ToList ();

            var oneWeekOutItem = this.Items.Where (calItem => {
                if (today.AddDays (7) == calItem.DateOfCalItemThisYear) {
                    return true;
                }
                return false;
            }).ToList ();

            var threeDaysOutItem = this.Items.Where (calItem => {
                if (today.AddDays (3) == calItem.DateOfCalItemThisYear) {
                    return true;
                }
                return false;
            }).ToList ();

            var oneDayOutItem = this.Items.Where (calItem => {
                if (today.AddDays (1) == calItem.DateOfCalItemThisYear) {
                    return true;
                }
                return false;
            }).ToList ();

            this.OneMonthItems.AddRange (oneMonthOutItems);
            this.TwoWeekItems.AddRange (twoWeekOutItems);
            this.OneWeekItems.AddRange (oneWeekOutItem);
            this.ThreeDayItems.AddRange (threeDaysOutItem);
            this.OneDayItems.AddRange (oneDayOutItem);
        }
    }
}