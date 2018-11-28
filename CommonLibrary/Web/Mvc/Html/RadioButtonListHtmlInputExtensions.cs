using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Routing;

namespace CommonLibrary.Web.Mvc.Html
{
    public enum InputListType
    {
        RadioButton,
        Checkbox,
    }

    public static class InputListHtmlInputExtensions
    {
        public static MvcHtmlString RadioButtonListFor<TModel, TValue>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression,
           IEnumerable<SelectListItem> selectList,
           object htmlAttributes = null)
        {
            return htmlHelper.InputListFor(expression, selectList, new RouteValueDictionary(htmlAttributes), InputListType.RadioButton);
        }

        public static MvcHtmlString CheckboxListFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          IEnumerable<SelectListItem> selectList,
          object htmlAttributes = null)
        {
            return htmlHelper.InputListFor(expression, selectList, new RouteValueDictionary(htmlAttributes), InputListType.Checkbox);
        }

        public static MvcHtmlString InputListFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectList,
            object htmlAttributes = null,
            InputListType inputListType = InputListType.RadioButton)
        {

            return htmlHelper.InputListFor(expression, selectList, new RouteValueDictionary(htmlAttributes), inputListType);
        }

        public static MvcHtmlString InputListFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectList,
            IDictionary<string, object> htmlAttributes,
            InputListType inputListType = InputListType.RadioButton)
        {
            var inputName = GetInputName(expression);

            TValue value = (TValue)(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);

            // IEnumerable<SelectListItem> radioButtonList = GetValue(htmlHelper, expression);
            if (selectList == null) return MvcHtmlString.Empty;

            var divTag = new TagBuilder("div");
            divTag.MergeAttribute("id", inputName);
            divTag.MergeAttribute("class", inputListType == InputListType.RadioButton ? "radio" : "checkbox");
            foreach (var item in selectList)
            {
                var selectItem = new SelectListItem { Text = item.Text, Selected = item.Selected, Value = item.Value.ToString() };
                if (value != null && selectItem.Value.Equals(value.ToString()))
                    selectItem.Selected = true;
                var radioButtonTag =
                    InputButton(htmlHelper,
                    inputName,
                    selectItem,
                    htmlAttributes,
                    inputListType);
                divTag.InnerHtml += radioButtonTag;
            }
            return MvcHtmlString.Create(divTag.ToString());
        }

        public static string GetInputName<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            return htmlFieldName;
            //if (expression.Body.NodeType == ExpressionType.Call)
            //{
            //    var methodCallExpression = (MethodCallExpression)expression.Body;
            //    string name = GetInputName(methodCallExpression); return name.Substring(expression.Parameters[0].Name.Length + 1);
            //}

            //return expression.Body.ToString().Substring(expression.Parameters[0].Name.Length + 1);
        }

        private static string GetInputName(MethodCallExpression expression)
        {
            // p => p.Foo.Bar().Baz.ToString() => p.Foo OR throw...        
            var methodCallExpression = expression.Object as MethodCallExpression;
            if (methodCallExpression != null)
            {
                return GetInputName(methodCallExpression);
            } return expression.Object.ToString();
        }

        public static string InputButton(this HtmlHelper htmlHelper, 
            string name, 
            SelectListItem listItem, 
            IDictionary<string, object> htmlAttributes,
            InputListType inputListType = InputListType.RadioButton)
        {
            var inputIdSb = new StringBuilder();
            inputIdSb.Append(name).Append("_").Append(listItem.Value);
            var sb = new StringBuilder();
            var builder = new TagBuilder("input");
            if (listItem.Selected)
                builder.MergeAttribute("checked", "checked");
            builder.MergeAttribute("type", inputListType == InputListType.RadioButton ? "radio" : "checkbox");
            builder.MergeAttribute("value", listItem.Value);
            builder.MergeAttribute("id", inputIdSb.ToString());
            builder.MergeAttribute("name", name);
            builder.MergeAttributes(htmlAttributes);
            sb.Append(builder.ToString(TagRenderMode.SelfClosing));
            sb.Append(RadioButtonLabel(inputIdSb.ToString(), listItem.Text, htmlAttributes));
            sb.Append("<br>");
            return sb.ToString();
        }

        public static string RadioButtonLabel(string inputId, string displayText, IDictionary<string, object> htmlAttributes)
        {
            var labelBuilder = new TagBuilder("label"); labelBuilder.MergeAttribute("for", inputId); labelBuilder.MergeAttributes(htmlAttributes);
            labelBuilder.InnerHtml = displayText; return labelBuilder.ToString(TagRenderMode.Normal);
        }


        public static TProperty GetValue<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            TModel model = htmlHelper.ViewData.Model; if (model == null) { return default(TProperty); }
            Func<TModel, TProperty> func = expression.Compile(); return func(model);
        }
    }


}
