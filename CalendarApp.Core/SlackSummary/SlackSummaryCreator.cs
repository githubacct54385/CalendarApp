using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CalendarApp.Core.CreateSummary;
using CalendarApp.Core.GetCalendar.Interface;
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
            var sectionBlock = new SectionBlock ("mrkdwn", $"{header}");
            var block = new Block (type: "section", text : sectionBlock, elements : null, fields : null);
            AddBlock (block);
        }

        public void AddBoldSection (string header) {
            var sectionBlock = new SectionBlock ("mrkdwn", $"*{header}*");
            var block = new Block (type: "section", text : sectionBlock, elements : null, fields : null);
            AddBlock (block);
        }

        public void AddContextHeader () {
            var dateString = _dateProvider.GetToday ().ToString ("MMMM dd, yyyy");
            var text = $"Your event summary for {dateString}";

            var elements = new List<Element> () { new Element (type: "mrkdwn", text) };
            var block = new Block (type: "context", text : null, elements, fields : null);
            AddBlock (block);
        }

        public void AddFieldsSection (List<Field> fields) {
            var block = new Block (type: "section", text : null, elements : null, fields : fields);
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
                    AddSection ($":calendar: {eventCount} {eventsText} happening tomorrow");
                    break;
                case 3:
                    AddSection ($":calendar: {eventCount} {eventsText} happening 3 days from now");
                    break;
                case 7:
                    AddSection ($":calendar: {eventCount} {eventsText} happening 1 week from now");
                    break;
                case 14:
                    AddSection ($":calendar: {eventCount} {eventsText} happening 2 weeks from now");
                    break;
                case 30:
                    AddSection ($":calendar: {eventCount} {eventsText} happening 1 month from now");
                    break;
                default:
                    throw new ArgumentException ($"Unexpected value {daysUntil}", nameof (daysUntil));
            }
        }

        private void AddSectionForCalendarSummary (GetCalendar.Models.CalendarSummaryItem item) {
            string date = item.DateOfCalItemThisYear.ToString ("MM/dd/yyyy");
            var fields = new List<Field> ();
            fields.Add (new Field (type: "mrkdwn", header: "When", value : date));
            fields.Add (new Field (type: "mrkdwn", header: "Event", value : item.Name));
            AddFieldsSection (fields);
        }

        public string Serialize () {
            string json = JsonSerializer.Serialize<SlackJsonModel> (SlackJsonModel, SerializationOptionsWithoutIndentation ());
            return json.Replace ("\\n", "\n");
        }

        public SlackJsonModel SlackJsonModel { get; set; }
        private void AddBlock (Block block) {
            this.SlackJsonModel.Blocks.Add (block);
        }

        private JsonSerializerOptions SerializationOptionsWithIndentation () {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                IgnoreNullValues = true,
            };
            return options;
        }

        private JsonSerializerOptions SerializationOptionsWithoutIndentation () {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                IgnoreNullValues = true,
            };
            return options;
        }
    }
}