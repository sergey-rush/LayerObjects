using System;
using System.Web;
using System.Web.Mvc;

namespace LOB.Web.Code
{
    public static class UrlExtensions
    {
        public static string AbsoluteContent(this UrlHelper urlHelper, string contentPath)
        {
            // Build a URI for the requested path
            var url = new Uri(HttpContext.Current.Request.Url, urlHelper.Content(contentPath));
            // Return the absolute UrI
            return url.AbsoluteUri;
        }
    }
}