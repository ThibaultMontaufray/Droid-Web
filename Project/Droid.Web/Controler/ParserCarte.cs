namespace Droid.Web
{
    using System.Text.RegularExpressions;

    public static class ParserCarte
    {
        #region Attribute
        public static string WEBLINK = @"http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false";
        private static bool _found;
        #endregion

        #region Methods public
        public static Carte Parse(string url)
        {
            _found = false;
            Carte crt = new Carte();
            try
            {
                string webDump = Web.GetPage(url);
                GetNom(webDump, ref crt);
                GetCoord(webDump, ref crt);
                GetGMAP(webDump, ref crt);
            }
            catch (System.Exception exp)
            {
                System.Console.WriteLine(exp.Message);
            }
            return _found ? crt : null;
        }
        #endregion

        #region Methods private
        private static void GetNom(string webDump, ref Carte crt)
        {
            if (webDump.Contains("\"long_name\" : \""))
            {
                crt.Nom = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(webDump, "\"long_name\" : \"")[1], "\"")[0]);
                _found = true;
            }
        }
        private static void GetCoord(string webDump, ref Carte crt)
        {
            if (webDump.Contains("\"location\" :"))
            {
                string partDump = Regex.Split(webDump, "\"location\" :")[1];
                if (partDump.Contains("\"lat\" : "))
                {
                    crt.Latitude = double.Parse(HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(partDump, "\"lat\" : ")[1], ",")[0]).Replace(',', '.'));
                    _found = true;
                }
                if (partDump.Contains("\"lng\" : "))
                {
                    crt.Longitude = double.Parse(HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(partDump, "\"lng\" : ")[1], "\n")[0].Replace(',', '.')));
                    _found = true;
                }
            }
            _found = true;
        }
        private static void GetGMAP(string webDump, ref Carte crt)
        {
            crt.FrameVue = string.Format("https://www.google.com/maps/embed/v1/streetview?key=AIzaSyAkioX9C0qiGw9qGlewUMDSo7rhSd0ogso&location={0},{1}&heading=210%20&pitch=10%20&fov=100", crt.Latitude.ToString().Replace(',', '.'), crt.Longitude.ToString().Replace(',', '.'));
            crt.FrameCarte = string.Format("https://www.google.com/maps/embed/v1/search?key=AIzaSyAkioX9C0qiGw9qGlewUMDSo7rhSd0ogso&q={0}", crt.Nom);
            _found = true;
        }
        #endregion
    }
}
