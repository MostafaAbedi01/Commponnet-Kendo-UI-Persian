namespace Kendo.Mvc.UI.Fluent
{
    using System;
    using System.Web;
    using Kendo.Mvc.Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring bound columns filterable options
    /// </summary>
    /// <typeparam name="T">The type of the data item</typeparam>
    public class GridBoundColumnFilterableBuilder : GridFilterableSettingsBuilderBase<GridBoundColumnFilterableBuilder>
    {
        private readonly GridBoundColumnFilterableSettings settings;
        private System.Web.Mvc.ViewContext viewContext;
        private IUrlGenerator urlGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridBoundColumnFilterableBuilder"/> class.
        /// </summary>
        /// <param name="column">The column.</param>
        public GridBoundColumnFilterableBuilder(GridBoundColumnFilterableSettings settings, System.Web.Mvc.ViewContext viewContext, IUrlGenerator urlGenerator) : base(settings)
        {
            this.viewContext = viewContext;
            this.urlGenerator = urlGenerator;
            this.settings = settings;
        }

        /// <summary>
        /// Sets the type of the input element of the filter menu
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Kendo().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns =>
        ///                 columns.Bound(o => o.OrderDate)
        ///                        .Filterable(filterable =>
        ///                             filterable.UI(GridFilterUIRole.DatePicker)
        ///                        )
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public GridBoundColumnFilterableBuilder UI(GridFilterUIRole role)
        {
            settings.FilterUIRole = role;
            return this;
        }

        public GridBoundColumnFilterableBuilder Cell(Action<GridColumnFilterableCellSettingsBuilder> configurator)
        {
            configurator(new GridColumnFilterableCellSettingsBuilder(settings.CellSettings, this.viewContext, this.urlGenerator));
            
            return this;
        }

        /// <summary>
        /// Sets JavaScript function which to modify the UI of the filter input.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Kendo().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns =>
        ///                 columns.Bound(o => o.OrderDate)
        ///                        .Filterable(filterable =>
        ///                             filterable.UI(@&lt;text&gt;
        ///                                 JavaScript function goes here
        ///                                 &lt;/text&gt;)
        ///                         )
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public GridBoundColumnFilterableBuilder UI(Func<object, object> handler)
        {
            settings.FilterUIHandler.TemplateDelegate = handler;
            return this;
        }

        /// <summary>
        /// Sets JavaScript function which to modify the UI of the filter input.
        /// </summary>
        /// <param name="handler">JavaScript function name</param>
        public GridBoundColumnFilterableBuilder UI(string handler)
        {
            settings.FilterUIHandler.HandlerName = handler;
            return this;
        }
    }
}
