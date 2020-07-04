using System;
using System.Collections.Generic;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Tests {
    public class NthWeekdayOfMonthSampleData {
        public static List<CalendarItemBase> SampleDates (int numItems) {
            switch (numItems) {
                case 1:
                    return new List<CalendarItemBase> () {
                        MothersDay ()
                    };
                case 2:
                    return new List<CalendarItemBase> () {
                        MothersDay (), FathersDay ()
                    };
                default:
                    throw new Exception ("Unknown Num items.");
            }
        }

        private static NthWeekdayOfMonthCalendarItem FathersDay () {
            return new NthWeekdayOfMonthCalendarItem (
                id: Guid.Parse ("600de851-378c-4c59-baa5-5d1503206545"),
                name: "Fathers Day",
                reminderText: "Call your dad.",
                remindAtCsv: "1d,3d,7d,2w,1m",
                month : 6,
                nthWeek : 3,
                dayOfWeek : DayOfWeek.Sunday
            );
        }

        private static NthWeekdayOfMonthCalendarItem MothersDay () {
            return new NthWeekdayOfMonthCalendarItem (
                id: Guid.Parse ("03b420d0-f52a-4ff2-bbeb-bc3adfb68966"),
                name: "Mothers Day",
                reminderText: "Call your mom.",
                remindAtCsv: "1d,3d,7d,2w,1m",
                month : 5,
                nthWeek : 2,
                dayOfWeek : DayOfWeek.Sunday
            );
        }
    }
}