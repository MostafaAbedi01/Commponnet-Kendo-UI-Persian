﻿namespace Kendo.Mvc.UI.Fluent
{
    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="SchedulerRecurrenceEditorMonthlyMessages"/>.
    /// </summary>
    public class SchedulerRecurrenceEditorMonthlyMessagesBuilder: IHideObjectMembers
    {
        private readonly SchedulerRecurrenceEditorMonthlyMessages editorMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerRecurrenceEditorMonthlyMessagesBuilder"/> class.
        /// </summary>
        /// <param name="editorMessages">The editorMessages.</param>
        public SchedulerRecurrenceEditorMonthlyMessagesBuilder(SchedulerRecurrenceEditorMonthlyMessages editorMessages)
        {
            this.editorMessages = editorMessages;
        }

        public SchedulerRecurrenceEditorMonthlyMessagesBuilder RepeatEvery(string message)
        {
            editorMessages.RepeatEvery = message;

            return this;
        }

        public SchedulerRecurrenceEditorMonthlyMessagesBuilder RepeatOn(string message)
        {
            editorMessages.RepeatOn = message;

            return this;
        }

        public SchedulerRecurrenceEditorMonthlyMessagesBuilder Interval(string message)
        {
            editorMessages.Interval = message;

            return this;
        }

        public SchedulerRecurrenceEditorMonthlyMessagesBuilder Day(string message)
        {
            editorMessages.Day = message;

            return this;
        }
    }
}
