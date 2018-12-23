using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using LOB.Core;

namespace LOB.Http
{
    internal class LogRequest
    {
        #region Variables
        
        private static List<string> urls = new List<string>();

        #endregion

        internal LogRequest()
        {
            if (urls.Count == 0)
            {
                urls.Add("localhost/");
                urls.Add("Scripts/");
                urls.Add("Users/");
                urls.Add("Content/");
                urls.Add("js/");
                urls.Add("WebResource.axd");
                urls.Add("ChartImg.axd");
                urls.Add("favicon.ico");
                urls.Add("favicon.png");
                urls.Add("logo.png");
                urls.Add("bootstrap-theme.css");
                urls.Add("bootstrap.min.css");
                urls.Add("fonts/");
                urls.Add("Pictures/");
            }
        }

        internal void ProcessRequest(HttpContext context)
        {
            //IEnumerable<string> found = context.Request.Url.Segments.Intersect(urls);

            //if (!found.Any())
            //{
            //    try
            //    {
            //        Request request = new Request();
            //        request.Ip = context.Request.UserHostAddress;
            //        request.UserAgent = context.Request.UserAgent;
            //        request.UserName = context.User.Identity.Name;
            //        request.Uri = context.Server.UrlDecode(context.Request.Url.AbsoluteUri);
            //        request.HttpMethod = context.Request.HttpMethod;

            //        if (context.Request.UrlReferrer != null)
            //        {
            //            string urlReferrer = context.Server.UrlDecode(context.Request.UrlReferrer.ToString());
            //            if (urlReferrer != null)
            //            {
            //                request.UrlReferrer = urlReferrer;
            //                request.UrlReferrerHost = context.Request.UrlReferrer.Host;
            //            }
            //        }

            //        //Data.DataAccess.Logs.InsertRequest(request);
            //    }
            //    catch
            //    {
            //        // ignore
            //    }
            //}
        }

        /// <summary>
        /// Reads SessionId from HttpCookie 
        /// </summary>
        /// <param name="context">HttpContext.Current.Request</param>
        /// <returns>if not exists returns 0</returns>
        private int ReadCookie(HttpContext context)
        {
            int sessionId = 0;
            HttpCookie httpCookie = context.Request.Cookies["SessionId"];
            if (httpCookie != null)
            {
                if (Int32.TryParse(httpCookie.Value, out sessionId))
                {
                    return sessionId;
                }
            }
            return sessionId;
        }

        private void WriteCookie(int sessionId, HttpContext context, DateTime dt)
        {
            var sessionCookie = new HttpCookie("SessionId") { Value = sessionId.ToString() };
            int addHours = 24 - dt.Hour;
            sessionCookie.Expires = dt.AddHours(addHours).ToUniversalTime();
            context.Response.Cookies.Add(sessionCookie);
        }
    }
}