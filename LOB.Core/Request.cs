using System;

namespace LOB.Core
{
    public class Request
    {
        private string _ip = String.Empty;
        private string _userAgent = String.Empty;
        private string _userName = String.Empty;
        private string _uri = String.Empty;
        private string _httpMethod = String.Empty;
        private string _urlReferrer = String.Empty;
        private string _urlReferrerHost = String.Empty;
        public int Id { get; set; }

        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }

        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }

        public string HttpMethod
        {
            get { return _httpMethod; }
            set { _httpMethod = value; }
        }

        public string UrlReferrer
        {
            get { return _urlReferrer; }
            set { _urlReferrer = value; }
        }

        public string UrlReferrerHost
        {
            get { return _urlReferrerHost; }
            set { _urlReferrerHost = value; }
        }

        public DateTime Created { get; set; }
    }
}