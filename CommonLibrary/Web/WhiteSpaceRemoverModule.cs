using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;

namespace Mehr.Web
{
    public class WhiteSpaceRemoverModule : IHttpModule
    {
        static Regex whiteSpaceFilter = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}",
            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        public void Dispose()
        {
        }


        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }


        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            app.Response.Filter = new WhitespaceFilter(app.Response.Filter);
        }

        private class WhitespaceFilter : Stream
        {

            public WhitespaceFilter(Stream sink)
            {
                _sink = sink;
            }

            private Stream _sink;
            private static Regex reg = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}",
                  RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);


            #region Properites

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanSeek
            {
                get { return true; }
            }

            public override bool CanWrite
            {
                get { return true; }
            }

            public override void Flush()
            {
                _sink.Flush();
            }

            public override long Length
            {
                get { return 0; }
            }

            private long _position;
            public override long Position
            {
                get { return _position; }
                set { _position = value; }
            }

            #endregion

            #region Methods

            public override int Read(byte[] buffer, int offset, int count)
            {
                return _sink.Read(buffer, offset, count);
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return _sink.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                _sink.SetLength(value);
            }

            public override void Close()
            {
                _sink.Close();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                string html = Encoding.UTF8.GetString(buffer, offset, count);

                html = reg.Replace(html, string.Empty);

                byte[] outdata = System.Text.Encoding.UTF8.GetBytes(html);

                _sink.Write(outdata, 0, outdata.GetLength(0));
            }

            #endregion

        }
    }
}
