using System.Web.Mvc;
using IranSoftjo.Core.Web.Mvc.Html;

namespace IranSoftjo.Core.Web.Mvc
{
    public enum MessageType
    {
        None,
        Error,
        Warn,
        Success
    }

    public static class TempDataDictionaryExtensions
    {
        public const string MessageKey = "message";
        public const string MessageTypeKey = "messageType";
        private const string MessageHtmlTemplate =
            @"<span class=""msgWrraper""> <span class=""msg {1}Msg""> {2}{0} </span> </span>";

        public static void SetMessage(this TempDataDictionary tempData, string message,
            MessageType messageType = MessageType.None)
        {
            tempData[MessageKey] = message;
            tempData[MessageTypeKey] = messageType;
        }

        public static string GetMessage(this TempDataDictionary tempData)
        {
            return tempData[MessageKey] as string;
        }

        public static MessageType GetMessageType(this TempDataDictionary tempData)
        {
            return (MessageType) tempData[MessageTypeKey];
        }

        public static string GetMessageTypeClass(this TempDataDictionary tempData, string postfix)
        {
            MessageType messageType = tempData.GetMessageType();
            if (messageType == MessageType.None) return "";
            return messageType.ToString().ToLower() + postfix;
        }

        public static bool IsMessageExists(this TempDataDictionary tempData)
        {
            return !string.IsNullOrWhiteSpace(tempData.GetMessage());
        }

        public static MvcHtmlString GetMessage(this HtmlHelper html, string imageUrl)
        {
            TempDataDictionary tempData = html.ViewContext.TempData;
            if (tempData.IsMessageExists())
            {
                string messageTypeClass = tempData.GetMessageTypeClass("");
                string imageHtml = html.SpriteImage(imageUrl, "msgImg " + messageTypeClass + "Img").ToString();
                return
                    MvcHtmlString.Create(string.Format(MessageHtmlTemplate, tempData.GetMessage(), messageTypeClass,
                        imageHtml));
            }
            return MvcHtmlString.Empty;
        }
    }
}