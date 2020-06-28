using System;
using System.Collections.Generic;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Tests {
    public class NthWeekdayOfMonthSampleData {
        public static List<CalendarItem> SampleDates (int numItems, int year) {
            switch (numItems) {
                case 1:
                    return new List<CalendarItem> () {
                        MothersDay (year)
                    };
                case 2:
                    return new List<CalendarItem> () {
                        MothersDay (year), FathersDay (year)
                    };
                default:
                    throw new Exception ("Unknown Num items.");
            }
        }

        private static CalendarItem FathersDay (int year) {
            return new CalendarItem (
                id: Guid.Parse ("600de851-378c-4c59-baa5-5d1503206545"),
                name: "Fathers Day",
                reminder: "Call your dad.",
                repeatsYearly : true,
                when : new When (nthDayOfMonthRules: null, nthWeekdayOfMonthRules: new NthWeekdayOfMonthRules (weekday : DayOfWeek.Sunday, nthWeekday : 3)),
                repeatRules : new RepeatRules (startOn: new DateTime (year, 06, 21), endOn : null),
                reminders: "1d,3d,7d,2w,1m"
            );
        }

        private static CalendarItem MothersDay (int year) {
            return new CalendarItem (
                id: Guid.Parse ("03b420d0-f52a-4ff2-bbeb-bc3adfb68966"),
                name: "Mothers Day",
                reminder: "Call your mom.",
                repeatsYearly : true,
                when : new When (nthDayOfMonthRules: null, nthWeekdayOfMonthRules: new NthWeekdayOfMonthRules (weekday : DayOfWeek.Sunday, nthWeekday : 2)),
                repeatRules : new RepeatRules (startOn: new DateTime (year, 05, 10), endOn : null),
                reminders: "1d,3d,7d,2w,1m"
            );
        }
    }
}