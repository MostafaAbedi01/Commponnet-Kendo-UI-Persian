﻿namespace Kendo.Mvc.UI.Fluent
{
    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="SchedulerRecurrenceEditorYearlyMessages"/>.
    /// </summary>
    public class SchedulerRecurrenceEditorYearlyMessagesBuilder: IHideObjectMembers
    {
        private readonly SchedulerRecurrenceEditorYearlyMessages editorMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerRecurrenceEditorYearlyMessagesBuilder"/> class.
        /// </summary>
        /// <param name="editorMessages">The editorMessages.</param>
        public SchedulerRecurrenceEditorYearlyMessagesBuilder(SchedulerRecurrenceEditorYearlyMessages editorMessages)
        {
            this.editorMessages = editorMessages;
        }

        public SchedulerRecurrenceEditorYearlyMessagesBuilder RepeatEvery(string message)
        {
            editorMessages.RepeatEvery = message;

            return this;
        }

        public SchedulerRecurrenceEditorYearlyMessagesBuilder RepeatOn(string message)
        {
            editorMessages.RepeatOn = message;

            return this;
        }

        public SchedulerRecurrenceEditorYearlyMessagesBuilder Interval(string message)
        {
            editorMessages.Interval = message;

            return this;
        }

        public SchedulerRecurrenceEditorYearlyMessagesBuilder Of(string message)
        {
            editorMessages.Of = message;

            return this;
        }
    }
}
