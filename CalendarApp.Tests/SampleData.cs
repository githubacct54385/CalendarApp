using System;
using System.Collections.Generic;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Tests {
    public class SampleData {
        public static List<CalendarItem> GetSampleCalendarItemsWithItems (int numItems, int year) {
            switch (numItems) {
                case 1:
                    return new List<CalendarItem> () {
                        Christmas (year)
                    };
                case 2:
                    return new List<CalendarItem> () {
                        Christmas (year), Halloween (year)
                    };
                default:
                    throw new Exception ("Unknown Num items.");
            }
        }

        private static CalendarItem Christmas (int year) {
            return new CalendarItem (
                id: Guid.Parse ("71b81643-8f88-425a-b4d9-03cecb8108d5"),
                name: "Christmas",
                reminder: "Say Merry Christmas to your friends and family.",
                repeatsYearly : true,
                when : new When (new NthDayOfMonthRules (12, 25), nthWeekOfMonthRules : null),
                repeatRules : new RepeatRules (startOn: new DateTime (year, 12, 25), endOn : null),
                reminders: "1d,3d,7d,2w,1m"
            );
        }

        private static CalendarItem Halloween (int year) {
            return new CalendarItem (
                id: Guid.Parse ("22884935-6378-4a8a-93ba-6cebb992bb97"),
                name: "Halloween",
                reminder: "Go trick or treating and be spooky!",
                repeatsYearly : true,
                when : new When (new NthDayOfMonthRules (10, 31), nthWeekOfMonthRules : null),
                repeatRules : new RepeatRules (startOn: new DateTime (year, 10, 31), endOn : null),
                reminders: "1d,3d,7d,2w,1m"
            );
        }
    }
}