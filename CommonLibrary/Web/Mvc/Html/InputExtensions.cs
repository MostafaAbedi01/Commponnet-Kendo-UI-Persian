using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using CommonLibrary;
using CommonLibrary.Web;
using CommonLibrary.Web.Mvc;
using CommonLibrary.Web.Mvc.Html;
using Mehr;
using Mehr.DataAnnotations;
using Mehr.Reflection;
using Mehr.Web;
using Mehr.Web.Mvc;

namespace IranSoftjo.Core.Web.Mvc.Html
{
    public static class InputExtensions
    {
        public const string captchaHtmlTemplate = @"<span class=""_dpart captcha"">" +
                                                  "<input type=\"hidden\" name=\"captcha-guid\" value=\"{0}\"  />" +
                                                  @"<img src=""{1}"" alt=""CAPTCHA"" width=""{2}""  height=""{3}"" class=""captcha"" />" +
                                                  @"<a class=""_dpartLoader captchaReload"" href=""{4}"" title=""جهت تغییر تصویر اینجا را کلیک کنید""><img src=""{5}""/></a>" +
                                                  "</span>";

        public const string submitTemplate = @"<tr><td class=\""labelWrp\""></td> <td colspan=""2"">{0}</td> </tr>";
        public const string fieldOpenning = @"<tr>";

        public const string fieldContent =
            @"<td class=""labelWrp""> {0} </td>     <td class=""inputWrp"" colspan=""{4}"">	{1} <br /> {2} {3} </td>";

        public const string fieldClosing = @"</tr>";

        public const string noteTemplate = @"<span class=""inputNote"">{0}</span>";
        public const string spriteImageTemplate = @"<img src=""{0}"" height=""1px"" width=""1px"" class=""{1}""/>";
        public const string fieldGroupStart = @"<table class=""form"">";
        public const string fieldGroupEnd = @"</table>";

        public static IEnumMetadataFactory EnumMetadataFactory
        {
            get { return new EnumMetadataFactory(new HttpContextCacheProvider()); }
        }

        public static string CaptchaImageHtml(int height, int width, string refreshImageRelativeUrl)
        {
            var image = new CaptchaImage { Height = height, Width = width, };
            HttpRuntime.Cache.Add(image.UniqueId, image, null, DateTime.Now.AddSeconds(CaptchaImage.CacheTimeOut),
                Cache.NoSlidingExpiration, CacheItemPriority.Default, null);

            string captchaUrl = "/captcha/Image/" + image.UniqueId;
            string renewUrl = "/captcha/renew/" + image.UniqueId;

            return string.Format(captchaHtmlTemplate, image.UniqueId, captchaUrl, width, height, renewUrl,
                refreshImageRelativeUrl);
        }

        public static MvcHtmlString CaptchaTextBox(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.TextBox(name, "", new
            {
                maxlength = CaptchaImage.TextLength,
                autocomplete = "off",
                @class = "Captcha required"
            });
        }

        public static MvcHtmlString EnhancedLabelFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, bool checkIsRequired = true)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            labelText = labelText + ":";

            var tag = new TagBuilder("label");
            tag.SetInnerText(labelText);
            if (checkIsRequired)
            {
                bool isRequired =
                    metadata.ContainerType.GetProperty(metadata.PropertyName).IsDefined(typeof(RequiredAttribute), true);
                if (isRequired)
                    tag.AddCssClass("reqLabel");
            }

            tag.AddCssClass("fieldLabel");
            tag.Attributes.Add("for", htmlFieldName.Replace(".", "_"));
            tag.InnerHtml = string.Concat(tag.InnerHtml);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString EnhancedLabelField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null, string note = null, bool isTextArea = false)
        {
            //this HtmlHelper htmlHelper, string caption, string inputString = "",
            //string note = "", string validationMessageString = "",
            //bool isRequired = false, FieldGroupingSetting fieldGroupingSetting = FieldGroupingSetting.CloseBoth,
            //int valueCellCount = 1

            htmlAttributes =
                new
                {
                    //disabled = "disabled",
                    //@readonly = "readonly",
                    //style = " border: 1px solid #C9C9C2;   border-spacing: 1px !important;    border-collapse: separate !important;"
                };
            if (!isTextArea)
                return htmlHelper.EnhancedField(expression, htmlHelper.DisplayFor(expression, htmlAttributes), note, checkIsRequired: false);

            htmlAttributes =
              new
              {
                  disabled = "disabled",
                  @readonly = "readonly",
                  style = " background-color: #EFEEEF;   width: 35em;"
              };
            return htmlHelper.EnhancedField(expression, htmlHelper.TextAreaFor(expression, htmlAttributes), note, checkIsRequired: false);
        }


        public static MvcHtmlString EnhancedSubmit<TModel>(this HtmlHelper<TModel> htmlHelper, string caption = "ثبت",
            object htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("input");
            if (htmlAttributes != null)
                tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tagBuilder.MergeAttribute("type", "submit");
            tagBuilder.MergeAttribute("value", caption);
            tagBuilder.MergeAttribute("class", "full");
          
            return MvcHtmlString.Create(string.Format(submitTemplate, tagBuilder.ToString(TagRenderMode.SelfClosing)));
        }

        public static MvcHtmlString EnhancedReset<TModel>(this HtmlHelper<TModel> htmlHelper, string caption = "خالی کردن",
          object htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("input");
            if (htmlAttributes != null)
                tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tagBuilder.MergeAttribute("type", "reset");
            tagBuilder.MergeAttribute("value", caption);
            tagBuilder.MergeAttribute("class", "k-button k-button-icontext");
            return MvcHtmlString.Create(string.Format(submitTemplate, tagBuilder.ToString(TagRenderMode.SelfClosing)));
        }

        public static MvcHtmlString EnhancedBackList<TModel>(this HtmlHelper<TModel> htmlHelper, string caption = "بازگشت به لیست",
          object htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("a");
            tagBuilder.SetInnerText(caption);
            if (htmlAttributes != null)
                tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tagBuilder.MergeAttribute("class", "k-button k-button-icontext");
            tagBuilder.MergeAttribute("href", "/");
            return MvcHtmlString.Create(string.Format(submitTemplate, tagBuilder.ToString(TagRenderMode.SelfClosing)));
        }

        public static MvcHtmlString EnhancedEditorField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null, string note = null)
        {
            return htmlHelper.EnhancedField(expression, htmlHelper.EditorFor(expression, htmlAttributes), note);
        }

        public static MvcHtmlString EnhancedTextBoxField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null, string note = null)
        {
            return htmlHelper.EnhancedField(expression, htmlHelper.TextBoxFor(expression, htmlAttributes), note);
        }

        public static MvcHtmlString EnhancedCheckBoxField<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression,
            object htmlAttributes = null, string note = null)
        {
            return htmlHelper.EnhancedField(expression, htmlHelper.CheckBoxFor(expression, htmlAttributes), note);
        }

        public static MvcHtmlString EnhancedTextAreaField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null, string note = null)
        {
            return htmlHelper.EnhancedField(expression, htmlHelper.TextAreaFor(expression, htmlAttributes), note);
        }

        public static MvcHtmlString EnhancedDropDownListField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectList, object htmlAttributes = null, string note = null)
        {
            Guard.ArgumentNotNull(selectList, "selectList");
            return htmlHelper.EnhancedField(expression,
                htmlHelper.DropDownListFor(expression, selectList, htmlAttributes), note);
        }

        public static MvcHtmlString EnhancedDropDownList<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, string name,
            object htmlAttributes = null, string note = null)
        {
            return htmlHelper.EnhancedField(expression, htmlHelper.DropDownList(name, String.Empty), note);
        }

        public static MvcHtmlString EnhancedRadioButtonListField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectList, object htmlAttributes = null, string note = null)
        {
            Guard.ArgumentNotNull(selectList, "selectList");
            return htmlHelper.EnhancedField(expression,
                htmlHelper.RadioButtonListFor(expression, selectList, htmlAttributes),
                note);
        }

        public static MvcHtmlString EnhancedCheckboxListField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectList, object htmlAttributes = null, string note = null)
        {
            Guard.ArgumentNotNull(selectList, "selectList");
            return htmlHelper.EnhancedField(expression,
                htmlHelper.CheckboxListFor(expression, selectList, htmlAttributes),
                note);
        }

        public static MvcHtmlString EnhanceEnumListField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null, string note = null)
        {
            MvcHtmlString t = EnumListFor(htmlHelper, expression, htmlAttributes);
            return htmlHelper.EnhancedField(expression, t, note);
        }

        public static MvcHtmlString EnumListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);
            EnumMetadata<TValue> enumMetaData = EnumMetadataFactory.Get<TValue>();
            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            PropertyInfo propertyInfo = modelMetadata.ContainerType.GetProperty(modelMetadata.PropertyName);
            var enumFilterAttribute =
                Attribute.GetCustomAttribute(propertyInfo, typeof(EnumFilterAttribute)) as EnumFilterAttribute;

            SelectList selectList = (enumFilterAttribute != null)
                ? enumMetaData.ToSelectListAsTringFiltered(
                    enumMetaData.Items.Keys.Where(enumFilterAttribute.IsFiltered).ToArray())
                : enumMetaData.ToSelectListAsString();

            return htmlHelper.DropDownListFor(expression, selectList, htmlAttributes);
        }

        public static MvcHtmlString EnhancedCapthaField(this HtmlHelper htmlHelper, string refreshImageRelativeUrl,
            string name = "captcha")
        {
            //if (!CaptchaValidationAttribute.IsTryCountReached())
            //    return MvcHtmlString.Empty;
            return CreateField("حروف و اعداد موجود در تصویر زیر را در کادر وارد نمایید." +
                               @"<a href=""#captcha"" class=""helpLink""  style=""font-weight:normal""></a>" +
                               CaptchaImageHtml(50, 180, refreshImageRelativeUrl) +
                               htmlHelper.CaptchaTextBox(name), @"حروف به کوچک و بزرگ بودن حساس نیستند.",
                @"شناسایی تصویری:", (htmlHelper.ValidationMessage("captcha") ?? MvcHtmlString.Empty).ToString());
        }

        //<tr>
        //    <td class="labelWrp">
        //        شناسایی تصویری:
        //    </td>
        //    <td class="inputWrp">
        //        حروف و اعداد موجود در تصویر زیر را در جعبه زیرین تایپ نمایید.
        //        <%= Mehr.Web.Mvc.Html.InputExtensions.CaptchaImageHtml(50, 180, Sepanta.Crm.WebUI.Controllers.CaptchaController.RefreshImageRelativeUrl)%>
        //        <%= Html.CaptchaTextBox("captcha")%>
        //        <%: Html.ValidationMessage("captcha")%>
        //        <span class="inputNote">حروف به کوچک و بزرگ بودن حساس نیستند.</span>
        //    </td>
        //</tr>


        public static MvcHtmlString EnhancedPasswordField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null, string note = null)
        {
            return htmlHelper.EnhancedField(expression, htmlHelper.PasswordFor(expression, htmlAttributes), note);
        }

        private static MvcHtmlString EnhancedField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, MvcHtmlString inputString,
            string note = null, FieldGroupingSetting fieldGroupingSetting = FieldGroupingSetting.CloseBoth, bool checkIsRequired = true)
        {
            MvcHtmlString labelString = htmlHelper.EnhancedLabelFor(expression, checkIsRequired: checkIsRequired);
            MvcHtmlString validationMessageString = htmlHelper.ValidationMessageFor(expression);

            return CreateField(inputString.ToString(), note, labelString.ToString(), validationMessageString.ToString(),
                fieldGroupingSetting);
        }

        public static MvcHtmlString EnhancedReadnlyField<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, string note = "",
            bool isRequired = false, FieldGroupingSetting fieldGroupingSetting = FieldGroupingSetting.CloseBoth)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            return htmlHelper.EnhancedField(
                labelText,
                (TValue)metadata.Model,
                note,
                isRequired,
                fieldGroupingSetting);
        }

        public static MvcHtmlString EnhancedFieldHeader(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(@"
<tr style=""visibility: collapse"">
				<td class=""lbl"">
				</td>
				<td class=""val"">
				</td>
				<td class=""lbl"">
				</td>
				<td class=""val"">
				</td>
			</tr>");
        }

        public static MvcHtmlString EnhancedField<TValue>(this HtmlHelper htmlHelper, string caption, TValue value,
            string note = "",
            bool isRequired = false, FieldGroupingSetting fieldGroupingSetting = FieldGroupingSetting.CloseBoth,
            int valueCellCount = 1)
        {
            Type valueType = typeof(TValue);
            string stringValue = null;
            if (valueType.IsGenericType &&
                valueType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                Nullable.GetUnderlyingType(valueType).IsEnum &&
                value != null)
                stringValue = EnumMetadataFactory.GetCaption(value);
            else if (valueType.IsEnum)
                stringValue = EnumMetadataFactory.GetCaption(value);
            else
                stringValue = value == null ? string.Empty : value.ToString();
            return htmlHelper.EnhancedField(
                caption,
                stringValue,
                note,
                string.Empty,
                isRequired,
                fieldGroupingSetting,
                valueCellCount);
        }

        private static MvcHtmlString EnhancedField(this HtmlHelper htmlHelper, string caption, string inputString = "",
            string note = "", string validationMessageString = "",
            bool isRequired = false, FieldGroupingSetting fieldGroupingSetting = FieldGroupingSetting.CloseBoth,
            int valueCellCount = 1)
        {
            var tag = new TagBuilder("label");
            tag.SetInnerText(caption);

            if (isRequired)
                tag.AddCssClass("reqLabel");
            tag.AddCssClass("fieldLabel");
            tag.InnerHtml = string.Concat(tag.InnerHtml);

            return CreateField(inputString, note, tag.ToString(), validationMessageString, fieldGroupingSetting,
                valueCellCount);
        }

        private static MvcHtmlString CreateField(string inputString, string note, string labelString,
            string validationMessageString,
            FieldGroupingSetting fieldGroupingSetting = FieldGroupingSetting.CloseBoth, int valueCellCount = 1)
        {
            string noteString = string.Empty;
            if (!string.IsNullOrWhiteSpace(note))
                noteString = string.Format(noteTemplate, note);

            string fieldTemplate =
                (fieldGroupingSetting.HasFlag(FieldGroupingSetting.CloseStart) ? fieldOpenning : string.Empty) +
                fieldContent +
                (fieldGroupingSetting.HasFlag(FieldGroupingSetting.CloseEnd) ? fieldClosing : string.Empty);

            string htmlString = string.Format(fieldTemplate, labelString, inputString, validationMessageString,
                noteString, valueCellCount);
            return MvcHtmlString.Create(htmlString);
        }

        public static MvcHtmlString SpriteImage(this HtmlHelper htmlHelper, string imageUrl, string className)
        {
            return MvcHtmlString.Create(string.Format(spriteImageTemplate, imageUrl ?? "", className ?? ""));
        }

        public static MvcHtmlString ImageFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, HttpPostedFileBase>> expression)
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);
            return htmlHelper.ImageFor(inputName);
        }

        public static MvcHtmlString ImageFor(this HtmlHelper htmlHelper, string inputName,
            IDictionary<string, object> htmlAttributes = null)
        {
            var tag = new TagBuilder("input");
            tag.MergeAttributes(htmlAttributes);
            tag.MergeAttribute("type", "file", true);
            tag.MergeAttribute("name", inputName, true);
            tag.GenerateId(inputName);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString EnhancedImageField<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, HttpPostedFileBase>> expression, string note = null)
        {
            return htmlHelper.EnhancedField(expression, htmlHelper.ImageFor(expression), note);
        }

        public static MvcHtmlString WindowLink(this HtmlHelper htmlHelper, string title, string url,
            object htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("a");
            if (htmlAttributes != null)
                tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tagBuilder.MergeAttribute("href", "javascript:var p=window.open('" + url + "', '',  " +
                                              "'width=650, height=600, location=no, menubar=no, status=no, toolbar=no, scrollbars=yes, resizable=1')",
                true);
            tagBuilder.SetInnerText(title);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Marquee(this HtmlHelper html, string awesomeMessage)
        {
            var tagBuilder = new TagBuilder("marquee");
            tagBuilder.MergeAttribute("onmouseover", "this.stop();", true);
            tagBuilder.MergeAttribute("onmouseout", "this.start();", true);
            tagBuilder.MergeAttribute("direction", "right", true);
            tagBuilder.SetInnerText(awesomeMessage);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
        //public static MvcHtmlString AutoCompleteListFor<TModel>(this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, int>> expression,
        //    object options = null,
        //    string controllerName = null)
        //{
        //    var hiddenField = htmlHelper.HiddenFor(expression, null);

        //    var textFieldAttrs = new { @class = AutoCompleteListOptions.InputClassName };
        //    //var inputValue = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
        //    var inputName = ExpressionHelper.GetExpressionText(expression).Replace(AutoCompleteListOptions.IdPostfix, AutoCompleteListOptions.InputPostfix);
        //    var textField = htmlHelper.TextBox(inputName, null, textFieldAttrs);

        //    var optionsDictionary = new RouteValueDictionary(options);
        //    if (!optionsDictionary.ContainsKey("Url") && !string.IsNullOrWhiteSpace(controllerName))
        //    {
        //        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);
        //        optionsDictionary.Add("Url", urlHelper.Action(AutoCompleteListOptions.FindActionName, controllerName));
        //    }

        //    StringBuilder optionsFieldValueBuilder = new StringBuilder();
        //    foreach (var item in optionsDictionary)
        //        optionsFieldValueBuilder.Append(string.Concat(item.Key, ":\"", item.Value, "\","));
        //    string optionsFieldValue = string.Concat('{', optionsFieldValueBuilder.ToString().TrimEnd(','), '}');

        //    var optionsFieldName = ExpressionHelper.GetExpressionText(expression).Replace(AutoCompleteListOptions.IdPostfix, AutoCompleteListOptions.OptionsPostfix);
        //    var optionsField = htmlHelper.Hidden(optionsFieldName, optionsFieldValue);

        //    return MvcHtmlString.Create(optionsField.ToString() + hiddenField.ToString() + textField.ToString());
        //}


        private class FieldGroup : IDisposable
        {
            public static FieldGroup Instance;
            private readonly HtmlHelper htmlHelper;

            public FieldGroup(HtmlHelper htmlHelper)
            {
                this.htmlHelper = htmlHelper;

                htmlHelper.ViewContext.RequestContext.HttpContext.Response.Write(fieldGroupStart);
                Instance = this;
            }

            public void Dispose()
            {
                htmlHelper.ViewContext.RequestContext.HttpContext.Response.Write(fieldGroupEnd);
                Instance = null;
            }
        }
    }

    [Flags]
    public enum FieldGroupingSetting
    {
        OpenBoth = 0,
        CloseEnd = 1,
        CloseStart = 2,
        CloseBoth = CloseStart | CloseEnd,
    }
}