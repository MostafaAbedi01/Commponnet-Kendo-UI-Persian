using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mehr.Web
{
    /// <summary>    
    /// Custom HTTP Module for Cloaking IIS7 Server Settings to allow anonymity    
    /// </summary>    
    public class CloakHttpHeaderModule : IHttpModule
    {
        /// <summary>        
        /// List of Headers to remove        
        /// </summary>        
        private static List<string> headersToCloak = new List<string>() { "Server" };

        /// <summary>        
        /// Handles the current request.
        /// </summary>        
        /// <param name="context">        
        /// The HttpApplication context.    
        /// </param>       
        public void Init(HttpApplication context) { context.PreSendRequestHeaders += this.OnPreSendRequestHeaders; }

        /// <summary>   
        /// Remove all headers from the HTTP Response.        
        /// </summary>       
        /// <param name="sender">        
        /// The object raising the event       
        /// </param>        
        /// <param name="e">        
        /// The event data.        
        /// </param>        
        private void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            headersToCloak.ForEach(h => HttpContext.Current.Response.Headers.Remove(h));
        }

        public void Dispose() { }

    }
}

