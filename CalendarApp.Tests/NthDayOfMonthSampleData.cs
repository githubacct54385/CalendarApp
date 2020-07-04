using System;
using System.Collections.Generic;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Tests {
    public class NthDayOfMonthSampleData {
        public static List<CalendarItemBase> SampleDates (int numItems) {
            switch (numItems) {
                case 1:
                    return new List<CalendarItemBase> () {
                        Christmas ()
                    };
                case 2:
                    return new List<CalendarItemBase> () {
                        Christmas (), Halloween ()
                    };
                default:
                    throw new Exception ("Unknown Num items.");
            }
        }

        private static NthDayOfMonthCalendarItem Christmas () {
            return new NthDayOfMonthCalendarItem (
                id: Guid.Parse ("71b81643-8f88-425a-b4d9-03cecb8108d5"),
                name: "Christmas",
                reminderText: "Say Merry Christmas to your friends and family.",
                remindAtCsv: "1d,3d,7d,2w,1m",
                month : 12,
                day : 25
            );
        }

        private static NthDayOfMonthCalendarItem Halloween () {
            return new NthDayOfMonthCalendarItem (
                id: Guid.Parse ("22884935-6378-4a8a-93ba-6cebb992bb97"),
                name: "Halloween",
                reminderText: "Go trick or treating and be spooky!",
                remindAtCsv: "1d,3d,7d,2w,1m",
                month : 10,
                day : 31
            );
        }
    }
}