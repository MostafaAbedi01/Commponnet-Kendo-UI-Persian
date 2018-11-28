using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;
using Mehr.Setting;

namespace CommonLibrary.Web.Mvc.ScriptSeperation
{
    //http://weblogs.asp.net/imranbaloch/archive/2010/09/26/moving-asp-net-mvc-client-side-scripts-to-bottom.aspx
    public class ScriptSeperatorStream : Stream
    {
        public HttpContextBase HttpContext { get; set; }
        public Stream Base { get; set; }
        public bool NotAddLoadedScript { get; set; }
        public bool AbsolutePath { get; set; }

        ServiceLocator serviceLocator;
        public ServiceLocator ServiceLocator
        {
            get
            {
                return serviceLocator ??
                    (serviceLocator = (ServiceLocator.Current ?? ServiceLocator.EmptyContext).CreateNewDownLevel());
            }
        }

        static Regex scriptRegex = new Regex(@"<script[^>]*filename=""(?<filename>([^""]*))""*>(?<script>([^<]|<[^/]|</[^s]|</s[^c])*)</script>", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public override void Write(byte[] buffer, int offset, int count)
        {
            var matchFiles = new Dictionary<string, string>();
            string HTML = Encoding.UTF8.GetString(buffer, offset, count);
            HTML = scriptRegex.Replace(HTML, m =>
            {
                var scriptContent = m.Groups["script"].Value;
                var fileName = m.Groups["filename"].Value;
                string content = string.Empty;
                matchFiles.TryGetValue(fileName, out content);
                content += scriptContent;
                matchFiles[fileName] = content;
                return string.Empty;
            });
            foreach (var matchFile in matchFiles)
            {
                string jsFileName = matchFile.Key + ".js";
                string cacheKey = "jsFileName" + jsFileName;
                if (HttpContext.Cache[cacheKey] == null)
                {
                    string jsFilePath = HttpContext.Server.MapPath(GlobalHelpers.DynamicScriptVirtualPath + jsFileName);
                    if (!File.Exists(jsFilePath))
                    {

                        var content = (NotAddLoadedScript ? "" : "if (typeof (myBar) != \"undefined\") myBar.loaded(\"" + jsFileName + "\");") +
                            matchFile.Value;
                        File.WriteAllText(jsFilePath, content);
                    }
                    HttpContext.Cache.Add(cacheKey, "true", null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }
            var linkAppendStrategy = ServiceLocator.Resolve<ILinkAppendStrategy>() ?? new ReplaceLinkAppendStrategy();
            var fileNames = matchFiles.Select(m =>
                (AbsolutePath ? HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) : "")
                 + GlobalHelpers.DynamicScriptVirtualPath + m.Key + ".js").ToArray();
            HTML = linkAppendStrategy.Append(HTML, fileNames);

            buffer = System.Text.Encoding.UTF8.GetBytes(HTML);

            this.Base.Write(buffer, 0, buffer.Length);
        }

        public override bool CanRead { get { return this.Base.CanRead; } }

        public override bool CanSeek { get { return this.Base.CanSeek; } }

        public override bool CanWrite { get { return this.Base.CanWrite; } }

        public override void Flush() { this.Base.Flush(); }

        public override long Length { get { return this.Base.Length; } }

        public override long Position { get { return this.Base.Position; } set { this.Base.Position = value; } }

        public override int Read(byte[] buffer, int offset, int count) { return this.Base.Read(buffer, offset, count); }

        public override long Seek(long offset, SeekOrigin origin) { return this.Base.Seek(offset, origin); }

        public override void SetLength(long value) { this.Base.SetLength(value); }
    }

}
