namespace Droid_web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
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
        public static string GetLucky(string word)
        {
            string page = GetPage(string.Format("https://www.google.com/search?q={0}", word));
            _http.Abort();
            string url = Regex.Split(page, "<h3 class=\"r\"><a href=\"")[1];

            url = Regex.Split(url, "=")[1];
            url = Regex.Split(url, "&amp;")[0];

            return GetPage(url);
        }
        public static string GetFile(string url)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, System.IO.Path.GetFileName(url));
            }
            return string.Empty;
        }
        public static string GetLuckyImage(string keyWord)
        {
            List<string> urls = GetImages(keyWord);
            return urls.Count > 0 ? urls[0] : null;
        }
        public static List<string> GetImages(string keyWord)
        {
            return GoogleImage.GetUrls(GoogleImage.GetHtmlCode(keyWord));
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
            try
            {
                HttpWebResponse response = _http.GetResponse() as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    retVal = reader.ReadToEnd();
                }
                response.Close();
            }
            catch (WebException exp)
            {
                using (var sr = new StreamReader(exp.Response.GetResponseStream()))
                { 
                    retVal = sr.ReadToEnd();
                }
            }
            return retVal;
        }
        #endregion
    }
}
