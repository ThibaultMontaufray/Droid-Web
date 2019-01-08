namespace Droid.Web
{
    using System;
    using System.Text.RegularExpressions;

    public class ParserMeteo
    {
        #region Attribute
        public static string WEBLINK = @"https://www.google.fr/search?hl=fr&q=meteo+{0}";
        public static event MeteoEventHandler OnEnrichmentCompleted;
        private static string _dumpWeb;
        private static bool _found;
        #endregion

        #region Methods public
        public static Meteo Parse(string url)
        {
            _found = false;
            string dump = Web.GetPage(url);
            Meteo meteo = new Meteo();
            if (Regex.Split(dump, "<div id=\"topstuff\">").Length > 1)
            {
                GetImage(dump, ref meteo);
                GetTemparature(dump, ref meteo);
                GetPrecipitation(dump, ref meteo);
                GetHumidite(dump, ref meteo);
                GetVent(dump, ref meteo);
                GetVille(dump, ref meteo);
                GetDescription(dump, ref meteo);
                GetFrame(dump, ref meteo);
            }
            if (OnEnrichmentCompleted != null) { OnEnrichmentCompleted(meteo); }
            return _found ? meteo : null;
        }
        #endregion

        #region Methods private
        private static void GetFrame(string tab, ref Meteo meteo)
        {
            //meteo.FrameTerre = "http://earth.nullschool.net";
            meteo.FrameTerre = "http://www.meteoearth.com/";        
        }
        private static void GetImage(string tab, ref Meteo meteo)
        {
            try
            {
                if (Regex.Split(tab, "<img class=\"_Lbd\"").Length > 1)
                {
                    _dumpWeb = Regex.Split(tab, "<img class=\"_Lbd\"")[Regex.Split(tab, "<img class=\"_Lbd\"").Length - 1];
                    meteo.Image = "https:" + Regex.Split(_dumpWeb, "src=\"")[1].Split('"')[0];
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetVille(string tab, ref Meteo meteo)
        {
            try
            {
                if (Regex.Split(tab, "</b>-<b>").Length > 1)
                {
                    meteo.Ville = Regex.Split(tab, "</b>-<b>")[Regex.Split(tab, "</b>-<b>").Length - 1].Split('<')[0];
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetDescription(string tab, ref Meteo meteo)
        {
            try
            {
                if (Regex.Split(tab, "<td style=\"white-space:nowrap;padding-right:15px;color:#666\">").Length > 1)
                {
                    meteo.Description = Regex.Split(tab, "<td style=\"white-space:nowrap;padding-right:15px;color:#666\">")[1].Split('<')[0];
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetTemparature(string tab, ref Meteo meteo)
        {
            try
            {
                if (Regex.Split(tab, "wob_tm").Length > 1)
                {
                    meteo.Temperature = Regex.Split(tab, "wob_tm")[Regex.Split(tab, "wob_tm").Length - 1].Split('>')[1].Split('<')[0] + "°";
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetPrecipitation(string tab, ref Meteo meteo)
        {
            try
            {
                if (Regex.Split(tab, "wob_pp").Length > 1)
                {
                    meteo.Precipitation = Regex.Split(tab, "wob_pp")[Regex.Split(tab, "wob_pp").Length - 1].Split('>')[1].Split('<')[0];
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetHumidite(string tab, ref Meteo meteo)
        {
            try
            {
                if (Regex.Split(tab, "wob_hm").Length > 1)
                {
                    meteo.Humidite = Regex.Split(tab, "wob_hm")[Regex.Split(tab, "wob_hm").Length - 1].Split('>')[1].Split('<')[0];
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        private static void GetVent(string tab, ref Meteo meteo)
        {
            try
            {
                if (Regex.Split(tab, "wob_ws").Length > 1)
                {
                    meteo.Vent = Regex.Split(tab, "wob_ws")[Regex.Split(tab, "wob_ws").Length - 1].Split('>')[1].Split('<')[0];
                    _found = true;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + exp.Message);
            }
        }
        #endregion
    }
}
