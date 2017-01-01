namespace Droid_web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class ParserArticle
    {
        #region Attribute
        public static string WEBLINK = @"https://www.google.fr/search?hl=fr&tbm=nws&q={0}";
        private static bool _found;
        #endregion

        #region Methods public
        public static Article Parse(string url)
        {
            _found = false;
            string webDump = Web.GetPage(url);
            Article art = new Article();
            art.Dump = webDump;
            GetLink(webDump, ref art);
            GetTitle(webDump, ref art);
            GetText(webDump, ref art);
            GetSource(webDump, ref art);
            GetImagePath(webDump, ref art);
            GetCategory(webDump, ref art);
            return _found ? art : null;
        }
        #endregion

        #region Methods private
        //<h3 class=\"r\"><a href=\"/url?q=
        private static void GetLink(string webDump, ref Article art)
        {
            if (webDump.Contains("/url?q="))
            {
                art.Lien = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(webDump, @"/url\?q=")[1], "\"")[0]);
                _found = true;
            }
        }
        private static void GetText(string webDump, ref Article art)
        {
            if (webDump.Contains("<div class=\"esc-lead-snippet-wrapper\">"))
            {
                art.Texte = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(webDump, "<div class=\"esc-lead-snippet-wrapper\">")[1], "</div>")[0]);
                _found = true;
            }
            else if (webDump.Contains("<div class=\"st\">"))
            {
                art.Texte = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(webDump, "<div class=\"st\">")[1], "</div>")[0]);
                _found = true;
            }
        }
        private static void GetSource(string webDump, ref Article art)
        {
            if (webDump.Contains("<span class=\"f\">"))
            {
                string localDump = Regex.Split(Regex.Split(webDump, "<span class=\"f\">")[1], "</span>")[0];
                art.Source = Regex.Split(localDump, " - ")[0];
                if (Regex.Split(localDump, " - ").Length > 0) art.Date = Regex.Split(localDump, " - ")[1];
                _found = true;
            }
        }
        private static void GetImagePath(string webDump, ref Article art)
        {
            if (webDump.Contains("<img"))
            {
                art.ImagePath = Regex.Split(Regex.Split(webDump, "<img")[1], "src=\"")[1].Split('"')[0];
                _found = true;
            }
        }
        private static void GetTitle(string webDump, ref Article art)
        {
            if (webDump.Contains("<h3 class=\"r\">"))
            {
                string[] articles = Regex.Split(webDump, "<h3 class=\"r\">");
                string cleanHtml = string.Empty;
                string[] tab = Regex.Split(articles[1], ">");
                for (int i = 1; i < tab.Length; i++)
                {
                    if (tab[i].Contains("</h3")) break;
                    cleanHtml += " " + HtmlRemoval.StripTagsRegex(tab[i] + ">");
                }
                art.Titre = cleanHtml;
                _found = true;
            }
        }
        private static void GetCategory(string webDump, ref Article art)
        {
            if (webDump.Contains("www.lemonde.fr/"))
            {
                art.Categorie = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(webDump, "www.lemonde.fr/")[1], "/")[0]);
                _found = true;
            }
            else if (webDump.Contains("www.lemonde"))
            {
                art.Categorie = HtmlRemoval.StripTagsRegex(Regex.Split(webDump, "www.lemonde")[1].Split('.')[0]);
                _found = true;
            }
            else if (webDump.Contains("label class=\"esc-topic-heading\">"))
            {
                art.Categorie = HtmlRemoval.StripTagsRegex(Regex.Split(Regex.Split(webDump, "label class=\"esc-topic-heading\">")[1], "</label>")[0]);
                _found = true;
            }
        }
        #endregion
    }
}
