﻿namespace Kendo.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IScheduler
    {
        DataSource DataSource
        {
            get;
        }

        IUrlGenerator UrlGenerator
        {
            get;
        }

        ViewContext ViewContext
        {
            get;
        }

        bool IsInClientTemplate
        {
            get;
        }

        DateTime? Date
        {
            get;
        }

        DateTime? StartTime
        {
            get;
        }

        DateTime? EndTime
        {
            get;
        }

        DateTime? WorkDayStart
        {
            get;
        }

        DateTime? WorkDayEnd
        {
            get;
        }

        bool ShowWorkHours
        {
            get;
            set;
        }

        int? Height
        {
            get;
        }

        string EventTemplate
        {
            get;
        }

        string EventTemplateId
        {
            get;
        }

        string AllDayEventTemplate
        {
            get;
        }

        string AllDayEventTemplateId
        {
            get;
        }

        bool AllDaySlot
        {
            get;
        }

        bool Selectable
        {
            get;
        }

        string DateHeaderTemplate
        {
            get;
        }

        string DateHeaderTemplateId
        {
            get;
        }

        int? MajorTick
        {
            get;
        }

        string MajorTimeHeaderTemplate
        {
            get;
        }

        string MajorTimeHeaderTemplateId
        {
            get;
        }

        int? MinorTickCount
        {
            get;
        }

        string MinorTimeHeaderTemplate
        {
            get;
        }

        string MinorTimeHeaderTemplateId
        {
            get;
        }

        string Timezone
        {
            get;
        }

        int? Width
        {
            get;
        }

        bool Snap
        {
            get;
        }

        int? WorkWeekStart
        {
            get;
        }

        int? WorkWeekEnd
        {
            get;
        }

        IList<SchedulerViewBase> Views
        {
            get;
        }

        SchedulerMessages Messages
        {
            get;
        }

        SchedulerGroupSettings Group
        {
            get;
        }
    }

    public interface IScheduler<TModel> : IScheduler
        where TModel : class
    {
          IList<SchedulerResource<TModel>> Resources
        {
            get;
        }
    }
}
