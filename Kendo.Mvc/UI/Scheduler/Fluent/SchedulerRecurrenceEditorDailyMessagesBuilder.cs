﻿namespace Kendo.Mvc.UI.Fluent
{
    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="SchedulerRecurrenceEditorDailyMessages"/>.
    /// </summary>
    public class SchedulerRecurrenceEditorDailyMessagesBuilder: IHideObjectMembers
    {
        private readonly SchedulerRecurrenceEditorDailyMessages editorMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerRecurrenceEditorDailyMessagesBuilder"/> class.
        /// </summary>
        /// <param name="editorMessages">The editorMessages.</param>
        public SchedulerRecurrenceEditorDailyMessagesBuilder(SchedulerRecurrenceEditorDailyMessages editorMessages)
        {
            this.editorMessages = editorMessages;
        }

        public SchedulerRecurrenceEditorDailyMessagesBuilder RepeatEvery(string message)
        {
            editorMessages.RepeatEvery = message;

            return this;
        }

        public SchedulerRecurrenceEditorDailyMessagesBuilder Interval(string message)
        {
            editorMessages.Interval = message;

            return this;
        }
    }
}
