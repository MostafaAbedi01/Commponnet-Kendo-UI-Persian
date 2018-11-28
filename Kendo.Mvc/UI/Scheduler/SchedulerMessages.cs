﻿namespace Kendo.Mvc.UI
{
    using Kendo.Mvc.Resources;
    using System.Collections.Generic;

    public class SchedulerMessages : JsonObject
    {
        private const string DefaultToday = "Today";

        private const string DefaultSave = "Save";

        private const string DefaultCancel = "Cancel";

        private const string DefaultDestroy = "Delete";

        private const string DefaultEvent = "Event";

        private const string DefaultDate = "Date";

        private const string DefaultTime = "Time";

        private const string DefaultAllDay = "all day";

        private const string DefaultDeleteWindowTitle = "Delete event";

        private const string DefaultShowFullDay = "Show full day";

        private const string DefaultShowWorkDay = "Show business hours";

        public SchedulerMessages()
        {
            Today = Messages.Scheduler_Today;

            Save = Messages.Scheduler_Save;

            Cancel = Messages.Scheduler_Cancel;

            Destroy = Messages.Scheduler_Destroy;

            Event = Messages.Scheduler_Event;

            Date = Messages.Scheduler_Date;

            Time = Messages.Scheduler_Time;

            AllDay = Messages.Scheduler_AllDay;

            DeleteWindowTitle = Messages.Scheduler_DeleteWindowTitle;

            ShowFullDay = Messages.Scheduler_ShowFullDay;

            ShowWorkDay = Messages.Scheduler_ShowWorkDay;

            ViewMessages = new SchedulerViewMessages();

            RecurrenceMessages = new SchedulerRecurrenceMessages();

            RecurrenceEditorMessages = new SchedulerRecurrenceEditorMessages();

            EditorMessages = new SchedulerEditorMessages();
        }

        public string Today { get; set; }

        public string Save { get; set; }

        public string Cancel { get; set; }

        public string Destroy { get; set; }

        public string Event { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string AllDay { get; set; }

        public string DeleteWindowTitle { get; set; }

        public string ShowFullDay { get; set; }

        public string ShowWorkDay { get; set; }

        public SchedulerViewMessages ViewMessages { get; set; }

        public SchedulerRecurrenceMessages RecurrenceMessages { get; set; }

        public SchedulerRecurrenceEditorMessages RecurrenceEditorMessages { get; set; }

        public SchedulerEditorMessages EditorMessages { get; set; }

        protected override void Serialize(IDictionary<string, object> json)
        {
            if (Today != DefaultToday)
            {
                json["today"] = Today;
            }

            if (Save != DefaultSave)
            {
                json["save"] = Save;
            }

            if (Cancel != DefaultCancel)
            {
                json["cancel"] = Cancel;
            }

            if (Destroy != DefaultDestroy)
            {
                json["destroy"] = Destroy;
            }

            if (Event != DefaultEvent)
            {
                json["event"] = Event;
            }

            if (Date != DefaultDate)
            {
                json["date"] = Date;
            }

            if (Time != DefaultTime)
            {
                json["time"] = Time;
            }

            if (AllDay != DefaultAllDay)
            {
                json["allDay"] = AllDay;
            }

            if (DeleteWindowTitle != DefaultDeleteWindowTitle)
            {
                json["deleteWindowTitle"] = DeleteWindowTitle;
            }

            if (ShowFullDay != DefaultShowFullDay)
            {
                json["showFullDay"] = ShowFullDay;
            }

            if (ShowWorkDay != DefaultShowWorkDay)
            {
                json["showWorkDay"] = ShowWorkDay;
            }

            IDictionary<string, object> viewMessages = ViewMessages.ToJson();
            if (viewMessages.Count > 0)
            {
                json["views"] = viewMessages;
            }

            IDictionary<string, object> recurrenceMessages = RecurrenceMessages.ToJson();
            if (recurrenceMessages.Count > 0)
            {
                json["recurrenceMessages"] = recurrenceMessages;
            }

            IDictionary<string, object> editorMessages = EditorMessages.ToJson();
            if (editorMessages.Count > 0)
            {
                json["editor"] = editorMessages;
            }

            IDictionary<string, object> recurrenceEditorMessages = RecurrenceEditorMessages.ToJson();
            if (recurrenceEditorMessages.Count > 0)
            {
                json["recurrenceEditor"] = recurrenceEditorMessages;
            }
        }
    }
}
