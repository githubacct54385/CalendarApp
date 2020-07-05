using System;
using CalendarApp.Core.GetCalendar.Models;

namespace CalendarApp.Tests.SampleData {
    public class SampleCalendarSummary {
        public static readonly CalendarSummaryItem Christmas = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "Christmas",
            reminderText: "Say Merry Christmas",
            dateOfCalItemThisYear : new DateTime (2020, 12, 25)
        );

        public static readonly CalendarSummaryItem PeterBakersBirthday = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "Cliff Brooks Birthday",
            reminderText: "Say Happy Birthday to Cliff",
            dateOfCalItemThisYear : new DateTime (2020, 12, 09)
        );

        public static readonly CalendarSummaryItem SharonWintersBirthday = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "Sharon Winters Birthday",
            reminderText: "Say Happy Birthday to Sharon",
            dateOfCalItemThisYear : new DateTime (2020, 11, 26)
        );

        public static readonly CalendarSummaryItem CliffBrooksBirthday = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "Peter Baker Birthday",
            reminderText: "Say Happy Birthday to Peter",
            dateOfCalItemThisYear : new DateTime (2020, 12, 2)
        );

        public static readonly CalendarSummaryItem JohnDoesBirthday = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "John Doe Birthday",
            reminderText: "Say Happy Birthday to John",
            dateOfCalItemThisYear : new DateTime (2020, 11, 28)
        );

        public static readonly CalendarSummaryItem MaryJanesBirthday = new CalendarSummaryItem (
            id: Guid.NewGuid (),
            name: "Mary Jane Birthday",
            reminderText: "Say Happy Birthday to Mary",
            dateOfCalItemThisYear : new DateTime (2020, 11, 28)
        );

        public static readonly CalendarSummaryItem Halloween = new CalendarSummaryItem (
            id: Guid.Parse ("22884935-6378-4a8a-93ba-6cebb992bb97"),
            name: "Halloween",
            reminderText: "Go trick or treating and be spooky!",
            dateOfCalItemThisYear : new DateTime (2020, 10, 31)
        );
        public static readonly CalendarSummaryItem FathersDay = new CalendarSummaryItem (
            id: Guid.Parse ("600de851-378c-4c59-baa5-5d1503206545"),
            name: "Fathers Day",
            reminderText: "Call your dad.",
            dateOfCalItemThisYear : new DateTime (2020, 6, 21)
        );
    }
}