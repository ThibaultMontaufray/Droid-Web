namespace Droid.Web
{
    using LinqToWiki.Generated;
    using Microsoft.AspNetCore.WebUtilities;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Text.RegularExpressions;
    using System.Web;

    public static class Web
    {
        #region Attribute
        private static string _proxyHost;
        private static string _proxyLogin;
        private static string _proxyPassword;
        private static WebRequest _http;
        private static DateTime _lastPingFailled = DateTime.MinValue;
        //private static int _proxyPort = Params.WebProxyPort;
        private const string YOUTUBEQUERY = "https://www.youtube.com/results?search_query={0}";
        private const string YOUTUBELINK = "https://www.youtube.com/embed/{0}";
        #endregion

        #region Methods public
        public static string GetPingDomain(string url)
        {
            try
            {
                string cleanUrl = url;
                if (!string.IsNullOrEmpty(url))
                {
                    string[] tab = url.Split('/');
                    if (tab.Length > 2)
                    {
                        cleanUrl = tab[2];
                    }
                }
                return cleanUrl;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return string.Empty;
            }
        }
        public static bool IsInternetAlive()
        {
            bool _online = false;
            try
            {
                if (_lastPingFailled < DateTime.Now.AddMinutes(-1))
                { 
                    Ping ping = new Ping();
                    PingReply pr = ping.Send("www.google.fr");

                    _online = pr.Status == IPStatus.Success;
                    _lastPingFailled = DateTime.MinValue;
                }
            }
            catch
            {
                _lastPingFailled = DateTime.Now;
                Console.WriteLine("Ping failled.");
                _online = false;
            }
            return _online;
        }
        public static string GetPage(string url, string serviceUserName, string servicePassword)
        {
            var parametersToAdd = new Dictionary<string, string>();

            var formData = WebUtility.UrlEncode(String.Empty);
            if (!string.IsNullOrEmpty(serviceUserName)) parametersToAdd["Username"] = serviceUserName;
            if (!string.IsNullOrEmpty(servicePassword)) parametersToAdd["Password"] = servicePassword;
            PrepareHttp(QueryHelpers.AddQueryString(url, parametersToAdd));

            _http.Method = "POST";
            _http.ContentType = "application/x-www-form-urlencoded";

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
            var formData = WebUtility.UrlEncode(String.Empty);
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
        //public static SixLabors.ImageSharp.Image GetLuckyImageObject(string keyWord)
        //{
        //    SixLabors.ImageSharp.Image ret;
        //    string picUrl = GetLuckyImage(keyWord);
        //    var request = WebRequest.Create(picUrl);

        //    using (var response = request.GetResponse())
        //    using (var stream = response.GetResponseStream())
        //    {
        //        ret = Image.FromStream(stream);
        //    }
        //    return ret;
        //}
        public static string GetVideoLucky(string searching)
        {
            string ret = string.Empty;

            string dumpPage = GetPage(string.Format(YOUTUBEQUERY, searching.Replace(' ', '+')));
            string[] tab = Regex.Split(dumpPage, "https://i.ytimg.com/vi/");

            if (tab.Length > 1) { return string.Format(YOUTUBELINK, Regex.Split(tab[1], "/")[0]); }
            else { return string.Empty; }
        }
        public static List<string> GetImages(string keyWord)
        {
            return GoogleImage.GetUrls(GoogleImage.GetHtmlCode(keyWord));
        }
        public static List<string> GetIcon(string keyWord)
        {
            return GoogleImage.GetUrls(GoogleImage.GetHtmlCode(keyWord, true));
        }
        public static Dictionary<string, string> GetDetailMovie(string videoName)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            string[] tab;
            string dump = Wikipedia.SearchPage(videoName + " film", Wikipedia.Language.FR);
            string infoBox = Regex.Split(dump, "\n\n")[0];
            string[] tabInfoBox = infoBox.Split('|');
            
            foreach (var item in tabInfoBox.Where(t => t.Contains('=')).ToList())
            {
                tab = item.Split('=');
                data[CleanText(tab[0])] = CleanText(tab[1]);
            }

            string synopsysTitle = Regex.Split(dump, "== ")[1];
            string synopsys = Regex.Split(synopsysTitle, "\n")[1];
            synopsysTitle = synopsysTitle.Split('=')[0].Trim();
            data[synopsysTitle] = CleanText(synopsys);

            return data;
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
        private static string CleanText(string original)
        {
            if (string.IsNullOrEmpty(original)) return original;

            original = original.Trim();
            original = original.Replace("Lien", string.Empty);
            original = original.Split('\r')[0];
            original = original.Split('\n')[0];
            original = original.Replace("[[", "'");
            original = original.Replace("]]", "'");
            original = original.Replace("{", string.Empty);
            original = original.Replace("}", string.Empty);
            original = original.Replace("[", string.Empty);
            original = original.Replace("]", string.Empty);
            original = original.Replace("<br />", ",");
            if (original.EndsWith(",")) { original = original.Substring(0, original.Length - 1);  }

            return original;
        }
        #endregion
    }
}
