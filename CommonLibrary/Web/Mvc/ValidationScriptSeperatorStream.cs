using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;

namespace Mehr.Web.Mvc
{
    //http://weblogs.asp.net/imranbaloch/archive/2010/09/26/moving-asp-net-mvc-client-side-scripts-to-bottom.aspx
    public class ValidationScriptSeperatorStream : Stream
    {
        public HttpContextBase HttpContext { get; set; }
        public Stream Base { get; set; }

        public int? ScriptFileVersion { get; set; }
        //Other Members are not included for brevity

        static Regex scriptRegex = new Regex(@"</form><script[^>]*>(?<script>([^<]|<[^/])*)</script>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        // static Regex bodyRegex = new Regex(@"<body(?<body>.*)/body>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        static Regex whiteSpaceFilter = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}");

        public override void Write(byte[] buffer, int offset, int count)
        {
            StringBuilder scriptContentBuilder = new StringBuilder();
            string HTML = Encoding.UTF8.GetString(buffer, offset, count);
            HTML = scriptRegex.Replace(HTML, m => { scriptContentBuilder.Append(m.Groups["script"].Value); return "</form>"; });
            string scriptContent = scriptContentBuilder.ToString();
            if (!string.IsNullOrWhiteSpace(scriptContent))
            {
                string jsFileName =
                    (HttpContext.Request.RequestContext.RouteData.Values["area"]??"").ToString() +
                    HttpContext.Request.RequestContext.RouteData.Values["controller"].ToString() +
                    HttpContext.Request.RequestContext.RouteData.Values["action"].ToString() + (ScriptFileVersion??0).ToString() +
                    ".js";
                if (HttpContext.Cache[jsFileName] == null)
                {
                    string jsFilePath = HttpContext.Server.MapPath(GlobalHelpers.DynamicSciprtVirtualPath + jsFileName);
                    if (!File.Exists(jsFilePath))
                    {
                        scriptContentBuilder.Insert(0,
                         string.Concat("if (typeof (myBar) != \"undefined\") myBar.loaded(\"", jsFileName, "\");"));
                        File.WriteAllText(jsFilePath, scriptContentBuilder.ToString());
                    }
                    HttpContext.Cache.Add(jsFileName, "true", null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                }
                HTML = HTML.Replace("<body>", string.Concat("<body><input type=\"hidden\" id=\"valJs\" value=\"", GlobalHelpers.DynamicSciprtVirtualPath, jsFileName, "\"/>"));
            }
            //HTML = whiteSpaceFilter.Replace(HTML, string.Empty);
            //HTML = ReplaceNumbersInbody(HTML);
            //HTML = bodyRegex.Replace(HTML, m => ReplaceNumbers(m.Groups["body"].Value));

            buffer = System.Text.Encoding.UTF8.GetBytes(HTML);

            this.Base.Write(buffer, 0, buffer.Length);
        }


        public const int zeroCharCode = (int)'0';

        private static string ReplaceNumbersInbody(string HTML)
        {
            if (ConfigurationManager.AppSettings["ValidationScriptSeperator.DisableNumberLocalization"] == bool.TrueString)
                return HTML;

            var chars = HTML.ToCharArray();
            bool isInsideBody = false;
            bool isHrefOrSrc = false;
            for (int i = 0; i < chars.Length; i++)
            {
                if (char.ToLower(HTML[i]) == 'b' &&
                    char.ToLower(HTML[i + 1]) == 'o' &&
                    char.ToLower(HTML[i + 2]) == 'd' &&
                    char.ToLower(HTML[i + 3]) == 'y')
                    if (isInsideBody)
                    {
                        if (HTML[i - 2] == '<' && HTML[i - 1] == '/')
                            isInsideBody = false;
                    }
                    else if (HTML[i - 1] == '<')
                        isInsideBody = true;

                if (isInsideBody)
                {
                    if (HTML[i - 1] == ' ' &&
                        char.ToLower(HTML[i]) == 's' &&
                        char.ToLower(HTML[i + 1]) == 'r' &&
                        char.ToLower(HTML[i + 2]) == 'c')
                        isHrefOrSrc = true;

                    if (HTML[i - 1] == ' ' &&
                       char.ToLower(HTML[i]) == 'h' &&
                       char.ToLower(HTML[i + 1]) == 'r' &&
                       char.ToLower(HTML[i + 2]) == 'e' &&
                       char.ToLower(HTML[i + 3]) == 'f')
                        isHrefOrSrc = true;

                    if (isHrefOrSrc && HTML[i - 1] != '=' && (HTML[i] == '"' || HTML[i] == '\'' || HTML[i] == ' ' || HTML[i] == '>'))
                        isHrefOrSrc = false;

                    if (!isHrefOrSrc)
                    {
                        int num = ((int)chars[i]) - zeroCharCode;
                        if (0 < num && num < 10)
                            chars[i] = PersianNumbers[num];
                    }
                }
            }
            return new string(chars);
        }

        private static string ReplaceNumbers(string HTML)
        {
            if (ConfigurationManager.AppSettings["ValidationScriptSeperator.DisableNumberLocalization"] == bool.TrueString)
                return HTML;

            var chars = HTML.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int num = ((int)chars[i]) - zeroCharCode;
                if (0 < num && num < 10)
                    chars[i] = PersianNumbers[num];
            }
            return new string(chars);
        }


        //result.Replace(' ', ' ');
        static readonly char[] PersianNumbers = new char[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹', };

        public override bool CanRead
        {
            get { return this.Base.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.Base.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return this.Base.CanWrite; }
        }

        public override void Flush()
        {
            this.Base.Flush();
        }

        public override long Length
        {
            get { return this.Base.Length; }
        }

        public override long Position
        {
            get
            {
                return this.Base.Position;
            }
            set
            {
                this.Base.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.Base.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.Base.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.Base.SetLength(value);
        }
    }

}
