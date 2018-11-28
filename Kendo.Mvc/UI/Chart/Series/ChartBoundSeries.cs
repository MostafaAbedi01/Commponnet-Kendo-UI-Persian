namespace Kendo.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.Resources;

    public abstract class ChartBoundSeries<TModel, TValue, TCategory> : ChartSeriesBase<TModel>, IChartBoundSeries where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartBoundSeries{TModel, TValue}" /> class.
        /// </summary>
        /// <param name="expression">The expression used to extract the point value from the chart model.</param>
        /// <param name="colorExpression">The expression used to extract the point color from the chart model.</param>
        /// <param name="categoryExpression">The expression used to extract the point category from the chart model.</param>
        /// <param name="noteTextExpression">The expression used to extract the point note text from the chart model.</param>
        protected ChartBoundSeries(
            Expression<Func<TModel, TValue>> expression,
            Expression<Func<TModel, TCategory>> categoryExpression,
            Expression<Func<TModel, string>> noteTextExpression)
        {
            if (typeof(TModel).IsPlainType() && !expression.IsBindable())
            {
                throw new InvalidOperationException(Exceptions.MemberExpressionRequired);
            }

            Member = expression.MemberWithoutInstance();

            if (categoryExpression != null)
            {
                Category = categoryExpression.Compile();
                CategoryMember = categoryExpression.MemberWithoutInstance();
            }

            if (noteTextExpression != null)
            {
                NoteTextMember = noteTextExpression.MemberWithoutInstance();
            }

            if (typeof(TModel).IsPlainType())
            {
                var metadata = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
                Name = metadata.DisplayName;
            }

            if (string.IsNullOrEmpty(Name))
            {
                Name = Member.AsTitle();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartBoundSeries{TModel, TValue}" /> class.
        /// </summary>
        /// <param name="data">The series data.</param>
        protected ChartBoundSeries(IEnumerable data)
        {
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartBoundSeries{TModel, TValue}" /> class.
        /// </summary>
        protected ChartBoundSeries()
        {
        }

        /// <summary>
        /// The data used for binding.
        /// </summary>
        public IEnumerable Data
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the model data member name.
        /// </summary>
        /// <value>The model data member name.</value>
        public string Member
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the model data category member name.
        /// </summary>
        /// <value>The model data category member name.</value>
        public string CategoryMember
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the model color member name.
        /// </summary>
        /// <value>The model color member name.</value>
        public string ColorMember
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the model data note text member name.
        /// </summary>
        /// <value>The model data note text member name.</value>
        public string NoteTextMember
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a function which returns the category of the property to which the column is bound to.
        /// </summary>
        public Func<TModel, TCategory> Category
        {
            get;
            private set;
        }
    }

    public abstract class ChartBoundSeries<TModel, TValue> : ChartBoundSeries<TModel, TValue, string> where TModel: class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartBoundSeries{TModel, TValue}" /> class.
        /// </summary>
        /// <param name="expression">The expression used to extract the point value from the chart model.</param>
        protected ChartBoundSeries(Expression<Func<TModel, TValue>> expression)
            : base(expression, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartBoundSeries{TModel, TValue}" /> class.
        /// </summary>
        /// <param name="data">The series data.</param>
        protected ChartBoundSeries(IEnumerable data)
            : base(data)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartBoundSeries{TModel, TValue}" /> class.
        /// </summary>
        protected ChartBoundSeries()
        {
        }
    }
}
