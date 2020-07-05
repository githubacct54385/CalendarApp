using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CalendarApp.Core.CreateSummary;
using CalendarApp.Core.GetCalendar.Interface;
using CalendarApp.Core.SlackSummary.Interface;
using CalendarApp.Core.SlackSummary.Models;

namespace CalendarApp.Core.SlackSummary {
    public class SlackSummaryCreator {
        private readonly IDateProvider _dateProvider;
        private readonly CalendarSummary _calendarSummary;

        public SlackSummaryCreator (IDateProvider dateProvider, CalendarSummary calendarSummary) {
            _dateProvider = dateProvider;
            _calendarSummary = calendarSummary;
            SlackJsonModel = new SlackJsonModel ();
        }

        public void AddSection (string header) {
            var sectionBlock = new SectionBlock ("mrkdn", $"{header}");
            var block = new Block (type: "section", text : sectionBlock, elements : null);
            AddBlock (block);
        }

        public void AddBoldSection (string header) {
            var sectionBlock = new SectionBlock ("mrkdn", $"*{header}*");
            var block = new Block (type: "section", text : sectionBlock, elements : null);
            AddBlock (block);
        }

        public void AddContextHeader () {
            var dateString = _dateProvider.GetToday ().ToString ("MMMM dd, yyyy");
            var text = $"Your event summary for {dateString}";

            var elements = new List<Element> () { new Element (type: "mrkdn", text) };
            var block = new Block (type: "context", text : null, elements);
            AddBlock (block);
        }

        public void AddDividerBlock () {
            var block = new Block (isDividerBlock: true);
            AddBlock (block);
        }

        public void AddOneDayAwayItems () {
            var calItems = _calendarSummary.OneDayItems;
            if (calItems.Any ()) {
                AddSectionWithRemainingDays (eventCount: calItems.Count, daysUntil: 1);
                foreach (var item in calItems) {
                    AddSectionForCalendarSummary (item);
                }
            }
        }

        public void AddThreeDaysAwayItems () {
            var calItems = _calendarSummary.ThreeDayItems;
            if (calItems.Any ()) {
                AddSectionWithRemainingDays (eventCount: calItems.Count, daysUntil: 3);
                foreach (var item in calItems) {
                    AddSectionForCalendarSummary (item);
                }
            }
        }

        public void AddOneWeekAwayItems () {
            var calItems = _calendarSummary.OneWeekItems;
            if (calItems.Any ()) {
                AddSectionWithRemainingDays (eventCount: calItems.Count, daysUntil: 7);
                foreach (var item in calItems) {
                    AddSectionForCalendarSummary (item);
                }
            }
        }

        public void AddTwoWeeksAwayItems () {
            var calItems = _calendarSummary.TwoWeekItems;
            if (calItems.Any ()) {
                AddSectionWithRemainingDays (eventCount: calItems.Count, daysUntil: 14);
                foreach (var item in calItems) {
                    AddSectionForCalendarSummary (item);
                }
            }
        }

        public void AddOneMonthAwayItems () {
            var calItems = _calendarSummary.OneMonthItemsList;
            if (calItems.Any ()) {
                AddSectionWithRemainingDays (eventCount: calItems.Count, daysUntil: 30);
                foreach (var item in calItems) {
                    AddSectionForCalendarSummary (item);
                }
            }
        }

        private void AddSectionWithRemainingDays (int eventCount, int daysUntil) {
            var eventsText = eventCount == 1 ? "Event" : "Events";
            switch (daysUntil) {
                case 1:
                    AddSection ($":calendar: {eventCount} {eventsText} happening tomorrow :calendar:");
                    break;
                case 3:
                    AddSection ($":calendar: {eventCount} {eventsText} happening 3 days from now :calendar:");
                    break;
                case 7:
                    AddSection ($":calendar: {eventCount} {eventsText} happening a week from now :calendar:");
                    break;
                case 14:
                    AddSection ($":calendar: {eventCount} {eventsText} happening 2 weeks from now :calendar:");
                    break;
                case 30:
                    AddSection ($":calendar: {eventCount} {eventsText} happening one month from now :calendar:");
                    break;
                default:
                    throw new ArgumentException ($"Unexpected value {daysUntil}", nameof (daysUntil));
            }
        }

        private void AddSectionForCalendarSummary (GetCalendar.Models.CalendarSummaryItem item) {
            AddSection ($"`{item.DateOfCalItemThisYear.ToString("MM/dd/yyyy")}` {item.Name} --- {item.ReminderText}");
        }

        public string Serialize () {
            Console.Write (JsonSerializer.Serialize<SlackJsonModel> (SlackJsonModel, SerializationOptionsWithoutIndentation ()));
            return JsonSerializer.Serialize<SlackJsonModel> (SlackJsonModel, SerializationOptionsWithoutIndentation ());
        }

        public SlackJsonModel SlackJsonModel { get; set; }
        private void AddBlock (Block block) {
            this.SlackJsonModel.Blocks.Add (block);
        }

        public string HeaderJson () {
            throw new NotImplementedException ();
        }

        private JsonSerializerOptions SerializationOptionsWithIndentation () {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                IgnoreNullValues = true
            };
            return options;
        }

        private JsonSerializerOptions SerializationOptionsWithoutIndentation () {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                IgnoreNullValues = true
            };
            return options;
        }
    }
}