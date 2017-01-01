namespace Droid_web
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Forms;

    public delegate void WebSeekerEventHandler(string htmlDisplay, object result);
    public class WebSeeker
    {
        #region Attribute
        public static event WebSeekerEventHandler ResultFound;
        public static event WebSeekerEventHandler ConnectionIssue;

        private static object _result;
        private static string _currentResearch;
        #endregion

        #region Properties
        public static List<string> Components
        {
            get
            {
                List<string> components = new List<string>();
                Assembly thisAssembly = Assembly.GetExecutingAssembly();
                foreach (Type type in thisAssembly.GetTypes())
                {
                    if (type.Name.StartsWith("Parser"))
                    {
                        components.Add(type.Name.Replace("Parser", string.Empty));
                    }
                }
                return components;
            }
        }
        public static object Result
        {
            get { return _result; }
            set { _result = value; }
        }
        #endregion

        #region Methods public
        public static void Search(string type, string lookingFor)
        {
            try
            {
                _currentResearch = type;
                Type parserType = Type.GetType("Droid_web.Parser" + _currentResearch.Substring(0, 1).ToUpper() + _currentResearch.Substring(1, _currentResearch.Length-1).ToLower());
                if (parserType != null)
                {
                    string url = string.Format(parserType.GetField("WEBLINK").GetValue(null).ToString(), lookingFor);
                    if (!string.IsNullOrEmpty(url))
                    {
                        MethodInfo parseMethod = parserType.GetMethod("Parse");
                        _result = parseMethod.Invoke(null, new object[] { url });
                        if (ResultFound != null) { ResultFound(ObjectToHtml.GetHtml(_result), _result); }
                    }
                }
                else { ConnectionIssue("Aucune connexion à Internet.", string.Empty); }
            }
            catch (Exception exp)
            {
                if (ConnectionIssue != null) { ConnectionIssue("Erreur lors de la connexion à internet.", exp.Message); }
            }
        }
        #endregion

        #region Methods private
        #endregion

        #region Event
        #endregion
    }
}
