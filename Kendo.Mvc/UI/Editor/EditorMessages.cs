namespace Kendo.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Kendo.Mvc.Resources;

    public class EditorMessages : JsonObject
    {
        public EditorMessages()
        {
            Bold = Messages.Editor_Bold;
            Italic = Messages.Editor_Italic;
            Underline = Messages.Editor_Underline;
            Strikethrough = Messages.Editor_Strikethrough;
            Superscript = Messages.Editor_Superscript;
            Subscript = Messages.Editor_Subscript;
            JustifyCenter = Messages.Editor_JustifyCenter;
            JustifyLeft = Messages.Editor_JustifyLeft;
            JustifyRight = Messages.Editor_JustifyRight;
            JustifyFull = Messages.Editor_JustifyFull;
            InsertOrderedList = Messages.Editor_InsertOrderedList;
            InsertUnorderedList = Messages.Editor_InsertUnorderedList;
            Indent = Messages.Editor_Indent;
            Outdent = Messages.Editor_Outdent;
            CreateLink = Messages.Editor_CreateLink;
            Unlink = Messages.Editor_Unlink;
            InsertImage = Messages.Editor_InsertImage;
            InsertFile = Messages.Editor_InsertFile;
            InsertHtml = Messages.Editor_InsertHtml;
            FontName = Messages.Editor_FontName;
            FontNameInherit = Messages.Editor_FontNameInherit;
            FontSize = Messages.Editor_FontSize;
            FontSizeInherit = Messages.Editor_FontSizeInherit;
            FormatBlock = Messages.Editor_FormatBlock;
            Formatting = Messages.Editor_Formatting;
            Styles = Messages.Editor_Styles;
            ForeColor = Messages.Editor_ForeColor;
            BackColor = Messages.Editor_BackColor;
            ViewHtml = Messages.Editor_ViewHtml;
            ImageWebAddress= Messages.Editor_ImageWebAddress;
            ImageAltText= Messages.Editor_ImageAltText;
            LinkWebAddress = Messages.Editor_LinkWebAddress;
            LinkText = Messages.Editor_LinkText;
            LinkToolTip = Messages.Editor_LinkToolTip;
            LinkOpenInNewWindow= Messages.Editor_LinkOpenInNewWindow;
            DialogInsert = Messages.Editor_DialogInsert;
            DialogButtonSeparator = Messages.Editor_DialogButtonSeparator;
            DialogCancel = Messages.Editor_DialogCancel;
            DialogUpdate = Messages.Editor_DialogUpdate;
            CreateTable = Messages.Editor_CreateTable;
            AddColumnLeft = Messages.Editor_AddColumnLeft;
            AddColumnRight = Messages.Editor_AddColumnRight;
            AddRowAbove = Messages.Editor_AddRowAbove;
            AddRowBelow = Messages.Editor_AddRowBelow;
            DeleteRow = Messages.Editor_DeleteRow;
            DeleteColumn = Messages.Editor_DeleteColumn;

            ImageBrowserMessages = new EditorImageBrowserMessages();
            FileBrowserMessages = new EditorFileBrowserMessages();
        }

        public string Bold { get; set; }
        public string Italic { get; set; }
        public string Underline { get; set; }
        public string Strikethrough { get; set; }
        public string Superscript { get; set; }
        public string Subscript { get; set; }
        public string JustifyCenter { get; set; }
        public string JustifyLeft { get; set; }
        public string JustifyRight { get; set; }
        public string JustifyFull { get; set; }
        public string InsertOrderedList { get; set; }
        public string InsertUnorderedList { get; set; }
        public string Indent { get; set; }
        public string Outdent { get; set; }
        public string CreateLink { get; set; }
        public string Unlink { get; set; }
        public string InsertImage { get; set; }
        public string InsertFile { get; set; }
        public string InsertHtml { get; set; }
        public string FontName { get; set; }
        public string FontNameInherit { get; set; }
        public string FontSize { get; set; }
        public string FontSizeInherit { get; set; }
        public string FormatBlock { get; set; }
        public string Formatting { get; set; }
        public string Styles { get; set; }
        public string ForeColor { get; set; }
        public string BackColor { get; set; }
        public string ViewHtml { get; set; }
        public string ImageWebAddress { get; set; }
        public string ImageAltText { get; set; }
        public string LinkWebAddress { get; set; }
        public string LinkText { get; set; }
        public string LinkToolTip { get; set; }
        public string LinkOpenInNewWindow { get; set; }
        public string DialogInsert { get; set; }
        public string DialogButtonSeparator { get; set; }
        public string DialogCancel { get; set; }
        public string DialogUpdate { get; set; }
        public string CreateTable { get; set; }
        public string AddColumnLeft { get; set; }
        public string AddColumnRight { get; set; }
        public string AddRowAbove { get; set; }
        public string AddRowBelow { get; set; }
        public string DeleteRow { get; set; }
        public string DeleteColumn { get; set; }

        public EditorImageBrowserMessages ImageBrowserMessages { get; set; }
        public EditorFileBrowserMessages FileBrowserMessages { get; set; }

        private const string DefaultBold = "Bold";
        private const string DefaultItalic = "Italic";
        private const string DefaultUnderline = "Underline";
        private const string DefaultStrikethrough = "Strikethrough";
        private const string DefaultSuperscript = "Superscript";
        private const string DefaultSubscript = "Subscript";
        private const string DefaultJustifyCenter = "Center text";
        private const string DefaultJustifyLeft = "Align text left";
        private const string DefaultJustifyRight = "Align text right";
        private const string DefaultJustifyFull = "Justify";
        private const string DefaultInsertOrderedList = "Insert ordered list";
        private const string DefaultInsertUnorderedList = "Insert unordered list";
        private const string DefaultIndent = "Indent";
        private const string DefaultOutdent = "Outdent";
        private const string DefaultCreateLink = "Insert hyperlink";
        private const string DefaultUnlink = "Remove hyperlink";
        private const string DefaultInsertImage = "Insert image";
        private const string DefaultInsertFile = "Insert file";
        private const string DefaultInsertHtml = "Insert HTML";
        private const string DefaultFontName = "Select font family";
        private const string DefaultFontNameInherit = "(inherited font)";
        private const string DefaultFontSize = "Select font size";
        private const string DefaultFontSizeInherit = "(inherited size)";
        private const string DefaultFormatBlock = "Format";
        private const string DefaultFormatting = "Format";
        private const string DefaultStyles = "Styles";
        private const string DefaultBackColor = "Background color";
        private const string DefaultForeColor = "Color";
        private const string DefaultViewHtml = "View HTML";
        private const string DefaultImageWebAddress = "Web address";
        private const string DefaultImageAltText = "Alternate text";
        private const string DefaultLinkWebAddress = "Web address";
        private const string DefaultLinkText = "Text";
        private const string DefaultLinkToolTip = "ToolTip";
        private const string DefaultLinkOpenInNewWindow = "Open link in new window";
        private const string DefaultDialogInsert = "Insert";
        private const string DefaultDialogButtonSeparator = "or";
        private const string DefaultDialogCancel = "Cancel";
        private const string DefaultDialogUpdate = "Update";
        private const string DefaultCreateTable = "Create table";
        private const string DefaultAddColumnLeft = "Add column on the left";
        private const string DefaultAddColumnRight = "Add column on the right";
        private const string DefaultAddRowAbove = "Add row above";
        private const string DefaultAddRowBelow = "Add row below";
        private const string DefaultDeleteRow = "Delete row";
        private const string DefaultDeleteColumn = "Delete column";

        protected override void Serialize(IDictionary<string, object> json)
        {
            if (Bold != DefaultBold)
            {
                json["bold"] = Bold;
            }

            if (Italic != DefaultItalic)
            {
                json["italic"] = Italic;
            }

            if (Underline != DefaultUnderline)
            {
                json["underline"] = Underline;
            }

            if (Strikethrough != DefaultStrikethrough)
            {
                json["strikethrough"] = Strikethrough;
            }

            if (Superscript != DefaultSuperscript)
            {
                json["superscript"] = Superscript;
            }

            if (Subscript != DefaultSubscript)
            {
                json["subscript"] = Subscript;
            }

            if (JustifyCenter != DefaultJustifyCenter)
            {
                json["justifyCenter"] = JustifyCenter;
            }

            if (JustifyLeft != DefaultJustifyLeft)
            {
                json["justifyLeft"] = JustifyLeft;
            }

            if (JustifyRight != DefaultJustifyRight)
            {
                json["justifyRight"] = JustifyRight;
            }

            if (JustifyFull != DefaultJustifyFull)
            {
                json["justifyFull"] = JustifyFull;
            }

            if (InsertOrderedList != DefaultInsertOrderedList)
            {
                json["insertOrderedList"] = InsertOrderedList;
            }

            if (InsertUnorderedList != DefaultInsertUnorderedList)
            {
                json["insertUnorderedList"] = InsertUnorderedList;
            }

            if (Indent != DefaultIndent)
            {
                json["indent"] = Indent;
            }

            if (Indent != DefaultIndent)
            {
                json["indent"] = Indent;
            }

            if (Outdent != DefaultOutdent)
            {
                json["outdent"] = Outdent;
            }

            if (CreateLink != DefaultCreateLink)
            {
                json["createLink"] = CreateLink;
            }

            if (Unlink != DefaultUnlink)
            {
                json["unlink"] = Unlink;
            }

            if (InsertImage != DefaultInsertImage)
            {
                json["insertImage"] = InsertImage;
            }

            if (InsertFile != DefaultInsertFile)
            {
                json["insertFile"] = InsertFile;
            }

            if (InsertHtml != DefaultInsertHtml)
            {
                json["insertHtml"] = InsertHtml;
            }

            if (FontName != DefaultFontName)
            {
                json["fontName"] = FontName;
            }

            if (FontNameInherit != DefaultFontNameInherit)
            {
                json["fontNameInherit"] = FontNameInherit;
            }

            if (FontSize != DefaultFontSize)
            {
                json["fontSize"] = FontSize;
            }

            if (FontSizeInherit != DefaultFontSizeInherit)
            {
                json["fontSizeInherit"] = FontSizeInherit;
            }

            if (FormatBlock != DefaultFormatBlock)
            {
                json["formatBlock"] = FormatBlock;
            }

            if (Formatting != DefaultFormatting)
            {
                json["formatting"] = Formatting;
            }

            if (Styles != DefaultStyles)
            {
                json["styles"] = Styles;
            }

            if (BackColor != DefaultBackColor)
            {
                json["backColor"] = BackColor;
            }

            if (ForeColor != DefaultForeColor)
            {
                json["foreColor"] = ForeColor;
            }

            if (ViewHtml != DefaultViewHtml)
            {
                json["viewHtml"] = ViewHtml;
            }

            if (ImageWebAddress != DefaultImageWebAddress)
            {
                json["imageWebAddress"] = ImageWebAddress;
            }

            if (ImageAltText != DefaultImageAltText)
            {
                json["imageAltText"] = ImageAltText;
            }

            if (LinkWebAddress != DefaultLinkWebAddress)
            {
                json["linkWebAddress"] = LinkWebAddress;
            }

            if (LinkText != DefaultLinkText)
            {
                json["linkText"] = LinkText;
            }

            if (LinkToolTip != DefaultLinkToolTip)
            {
                json["linkToolTip"] = LinkToolTip;
            }

            if (LinkOpenInNewWindow != DefaultLinkOpenInNewWindow)
            {
                json["linkOpenInNewWindow"] = LinkOpenInNewWindow;
            }

            if (DialogInsert != DefaultDialogInsert)
            {
                json["dialogInsert"] = DialogInsert;
            }

            if (DialogButtonSeparator != DefaultDialogButtonSeparator)
            {
                json["dialogButtonSeparator"] = DialogButtonSeparator;
            }

            if (DialogCancel != DefaultDialogCancel)
            {
                json["dialogCancel"] = DialogCancel;
            }

            if (DialogUpdate != DefaultDialogUpdate)
            {
                json["dialogUpdate"] = DialogUpdate;
            }

            if (CreateTable != DefaultCreateTable)
            {
                json["createTable"] = CreateTable;
            }

            if (AddColumnLeft != DefaultAddColumnLeft)
            {
                json["addColumnLeft"] = AddColumnLeft;
            }

            if (AddColumnRight != DefaultAddColumnRight)
            {
                json["addColumnRight"] = AddColumnRight;
            }

            if (AddRowAbove != DefaultAddRowAbove)
            {
                json["addRowAbove"] = AddRowAbove;
            }

            if (AddRowBelow != DefaultAddRowBelow)
            {
                json["addRowBelow"] = AddRowBelow;
            }

            if (DeleteColumn != DefaultDeleteColumn)
            {
                json["deleteColumn"] = DeleteColumn;
            }
            if (DeleteRow != DefaultDeleteRow)
            {
                json["deleteRow"] = DeleteRow;
            }
        
        }
    }
}