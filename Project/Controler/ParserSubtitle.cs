using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Droid_web
{
    public class ParserSubtitle
    {
        #region Attribute
        #endregion

        #region Properties
        #endregion

        #region Methods public
        public static string Parse(string movie, string language)
        {
            string webDump = Web.GetLucky(string.Format("subtitle YIFY {0} {1}", movie.Replace('.', ' '), language));
            string[] tab = Regex.Split(webDump, language, RegexOptions.IgnoreCase);
            string link = string.Empty;
            for (int i = 1; i < tab.Length - 1; i++)
            {
                link += tab[i];
                if (tab[i].Contains(">subtitle<")) break;
                link += language;
            }
            link = link.Split('=')[1];
            link = link.Split('"')[1];
            string downloadPage = Web.GetPage("https://www.yifysubtitles.com" + link);
            string subtitleLink = Regex.Split(downloadPage, "<a class=\"btn-icon download-subtitle\" href=\"")[1];
            subtitleLink = subtitleLink.Split('"')[0];

            return subtitleLink;
        }
        public static List<string> Languages(string movie)
        {
            string lang;
            List<string> languages = new List<string>();

            string webDump = Web.GetLucky(string.Format("subtitle YIFY {0}", movie.Replace('.', ' ')));
            string[] tab = Regex.Split(webDump, "<span class=\"sub-lang\">", RegexOptions.IgnoreCase);

            foreach (var item in tab)
            {
                lang = item.Split('<')[0];
                if (!string.IsNullOrEmpty(lang) && !languages.Contains(lang)) languages.Add(lang);
            }

            return languages;
        }
        #endregion

        #region Methods private
        #endregion
    }
}