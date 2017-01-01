namespace Droid_web
{
    using System;
    using System.IO;
    using System.Net;
    using System.Web;
    using Tools4Libraries;
    public static class Web
    {
        #region Attribute
        private static string _proxyHost = Params.WebProxyHost;
        private static string _proxyLogin = Params.WebProxyLogin;
        private static string _proxyPassword = Params.WebProxyPassword;
        private static WebRequest _http;
        //private static int _proxyPort = Params.WebProxyPort;
        #endregion

        #region Methods public
        public static string GetPage(string url, string serviceUserName, string servicePassword)
        {
            PrepareHttp(url);

            _http.Method = "POST";
            _http.ContentType = "application/x-www-form-urlencoded";

            var formData = HttpUtility.ParseQueryString(String.Empty);
            if (!string.IsNullOrEmpty(serviceUserName)) formData.Add("Username", serviceUserName);
            if (!string.IsNullOrEmpty(serviceUserName)) formData.Add("Password", servicePassword);
            string postdata = formData.ToString();

            return GetResult();
        }
        public static string GetPage(string url)
        {
            PrepareHttp(url);
            return GetResult();
        }
        public static string GetJson(string user, string password, string url)
        {
            PrepareHttp(url);
            _http.Method = "GET";
            var formData = HttpUtility.ParseQueryString(String.Empty);
            _http.Credentials = new NetworkCredential(user, password);
            return GetResult();
        }
        #endregion

        #region Methods private
        private static void PrepareHttp(string url)
        {
            Uri proxy = WebRequest.DefaultWebProxy.GetProxy(new Uri(url));
            _http = WebRequest.Create(url);
            if (!string.IsNullOrEmpty(_proxyHost))
            { 
                _http.Proxy = new WebProxy(_proxyHost, proxy.Port);
            }
            if (!String.IsNullOrEmpty(_proxyLogin))
            {
                _http.Proxy.Credentials = new NetworkCredential(_proxyLogin, _proxyPassword);
            }
        }
        private static string GetResult()
        {
            string retVal = string.Empty;
            HttpWebResponse response = _http.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                retVal = reader.ReadToEnd();
            }
            response.Close();
            return retVal;
        }
        #endregion
    }
}
