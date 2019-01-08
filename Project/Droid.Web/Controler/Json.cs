//using System.IO;
//using System.Net;
//using System.Text;
////using System.Web.Script.Serialization;

//namespace Droid.Web
//{
//    public static class Json
//    {
//        public static WebResponse GetJsonResult(string url)
//        {
//            var webrequest = WebRequest.Create(url);
//            webrequest.Method = "GET";
//            return webrequest.GetResponse();
//        }
//        //public static T Deserialize<T>(string input)
//        //{
//        //    JavaScriptSerializer js = new JavaScriptSerializer();
//        //    return (T)js.Deserialize(input, typeof(T));
//        //}
//        public static bool Authentication(string url, string login, string password)
//        {
//            string data = string.Format("clientId={0}&clientSecret={1}", login, password);
//            var objText = SendData(url, data);
//            var result = Deserialize<string>(objText);

//            return result.Equals("Authorized");
//        }
//        public static string SendData(string url, string data)
//        {
//            byte[] dataStream = Encoding.UTF8.GetBytes(data);
//            WebRequest webRequest = WebRequest.Create(url);
//            webRequest.Method = "POST";
//            webRequest.ContentType = "application/x-www-form-urlencoded";
//            webRequest.ContentLength = dataStream.Length;
//            Stream newStream = webRequest.GetRequestStream();
            
//            newStream.Write(dataStream, 0, dataStream.Length);
//            newStream.Close();
//            WebResponse response = webRequest.GetResponse();

//            using (var reader = new StreamReader(response.GetResponseStream()))
//            {
//                JavaScriptSerializer js = new JavaScriptSerializer();
//                return reader.ReadToEnd();
//            }
//        }
//        public static string GetDataDump(string url)
//        {
//            WebRequest http = WebRequest.Create(url);
//            http.Method = "GET";
//            return GetResult(http);
//        }
//        public static string GetResult(WebRequest http)
//        {
//            string retVal = string.Empty;
//            try
//            {
//                HttpWebResponse response = http.GetResponse() as HttpWebResponse;
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    Stream dataStream = response.GetResponseStream();
//                    StreamReader reader = new StreamReader(dataStream);
//                    retVal = reader.ReadToEnd();
//                }
//                response.Close();
//            }
//            catch (WebException exp)
//            {
//                using (var sr = new StreamReader(exp.Response.GetResponseStream()))
//                {
//                    retVal = sr.ReadToEnd();
//                }
//            }
//            return retVal;
//        }

//    }
//}
