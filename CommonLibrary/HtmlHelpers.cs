// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ModelMetadata = System.Web.Mvc.ModelMetadata;
using ModelMetadataProvider = System.Web.Mvc.ModelMetadataProvider;

namespace CommonLibrary
{
    public static class DisplayNameExtensions
    {
        #region Internal Methods
        internal static MvcHtmlString DisplayNameInternal(this HtmlHelper html, string expression, ModelMetadataProvider metadataProvider)
        {
            return DisplayNameHelper(ModelMetadata.FromStringExpression(expression, html.ViewData),
                                     expression);
        }

        //[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "This is an extension method")]
        internal static MvcHtmlString DisplayNameForInternal<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> html, Expression<Func<TModel, TValue>> expression, ModelMetadataProvider metadataProvider)
        {
            return DisplayNameHelper(ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>()),
                                     ExpressionHelper.GetExpressionText(expression));
        }



        internal static MvcHtmlString DisplayNameForInternal<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, ModelMetadataProvider metadataProvider)
        {
            return DisplayNameHelper(ModelMetadata.FromLambdaExpression(expression, html.ViewData),
                                     ExpressionHelper.GetExpressionText(expression));
        }

        internal static MvcHtmlString DisplayNameHelper(ModelMetadata metadata, string htmlFieldName)
        {
            // We don't call ModelMetadata.GetDisplayName here because we want to fall back to the field name rather than the ModelType.
            // This is similar to how the LabelHelpers get the text of a label.
            string resolvedDisplayName = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            return new MvcHtmlString(HttpUtility.HtmlEncode(resolvedDisplayName));
        }
        #endregion

        public static MvcHtmlString DisplayNameForSingleModelItem<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> html, Expression<Func<TModel, TValue>> expression)
        {
            return DisplayNameForInternal(html, expression, metadataProvider: null);
        }

        public static IHtmlString DisplyPluralNameFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            if (metadata.AdditionalValues.ContainsKey("DisplyNamePlural"))
                return new HtmlString((string)metadata.AdditionalValues["DisplyNamePlural"]);

            return new HtmlString("");
        }
        
        public static IHtmlString DisplyModelPluralName<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> html, Expression<Func<TModel, TValue>> expression)
        {
            HtmlString singleModelItem = DisplayNameForSingleModelItem(html, expression);
            HtmlString ModelPluralName = null;
            
            var metadata = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            if (metadata.AdditionalValues.ContainsKey("DisplyNamePlural"))
                ModelPluralName = new HtmlString((string)metadata.AdditionalValues["DisplyNamePlural"]);

            return ModelPluralName ?? singleModelItem ?? new HtmlString("");
        }

        public static IHtmlString TooltipFor<TModel, TValue>(this HtmlHelper<TModel> html,Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData);
            if (metadata.AdditionalValues.ContainsKey("Tooltip"))
                return new HtmlString((string)metadata.AdditionalValues["Tooltip"]);

            return new HtmlString("");
        }
    }
}