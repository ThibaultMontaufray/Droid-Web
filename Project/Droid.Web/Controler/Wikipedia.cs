using LinqToWiki;
using LinqToWiki.Download;
using LinqToWiki.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Droid.Web
{
    public static class Wikipedia
    {
        #region Enum
        public enum Language
        {
            FR,
            EN
        }
        #endregion

        #region Attributes
        private const string URLDETAILS = "https://fr.wikipedia.org/w/index.php?action=raw&title={0}";
        #endregion

        #region Properties
        #endregion

        #region Methods public
        public static string SearchPageTitle(string name, Language shortLanguage = Language.EN)
        {
            try
            {
                Downloader.LogDownloading = true;
                var wiki = new Wiki("LinqToWiki.Samples", string.Format("https://{0}.wikipedia.org", shortLanguage.ToString().ToLower()), "/w/api.php");
                var result = from s in wiki.Query.search(name) select new { s.title };
                string title = result.ToList()[0].ToString().Split('=')[1];
                return title.Split('}')[0].Trim();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string SearchPage(string name, Language shortLanguage = Language.EN)
        {
            string title = SearchPageTitle(name, shortLanguage);
            return Web.GetPage(string.Format(URLDETAILS, title.Replace(' ', '_')));
        }
        #endregion

        #region Sandbox
        [Obsolete("Sandbox only")]
        public static string Test()
        {
            SearchWiki();

            var wiki = new Wiki("LinqToWiki.Samples", "https://en.wikipedia.org", "/w/api.php");
            // Login(wiki, "username", "password");

            wiki.login("AmosTrack", "");

            //var query1 = wiki.Query.allpages().Where(i => i.prefix.ToLower().Contains("black") && i.prefix.ToLower().Contains("panther") && i.prefix.ToLower().Contains("movie")).Select(s => s.title).ToList().FirstOrDefault();


            var data = wiki.Query.search("Black panther Movie").Pages;

            string text =
                wiki.CreateTitlesSource("FRANCE")
                    .Select(p => p.revisions().ToList()[0].value)
                    .ToEnumerable()
                    .Single();

            //var titles = wiki.CreateTitlesSource("Munich Frauenkirche", "Marienplatz", "Karlsplatz (Stachus)");
            //var pages =
            //    titles.Select(
            //        page => new
            //        {
            //            Title = page.info.title,
            //            Text = page.revisions()
            //                       .Where(r => r.section == "0" && r.parse)
            //                       .Select(r => r.value)
            //                       .ToList()[0],
            //            LangLinks = page.langlinks().ToEnumerable()
            //        }).ToList()[0];

            var query = wiki.Query.allimages().Where(i => i.prefix == "Microsoft").Select(s => s.title).ToList();


            var result0 = from rp in wiki.Query.random()
                         select rp.title;


            var result1 = from s in wiki.Query.search("Black Panther")
                         select new { s.title, snippet = s.snippet.Substring(0, 30) };

            var res2 = wiki.Query.search("Black Panther");
            // Categories(TitlePages(wiki));
            

            return string.Empty;
        }
        private static void SearchWiki()
        {
            Downloader.LogDownloading = true;
            var wiki = new Wiki("LinqToWiki.Samples", "https://en.wikipedia.org", "/w/api.php");
            Search(wiki);
        }

        private static void Search(Wiki wiki)
        {
            var resultSnippet = from s in wiki.Query.search("Black Panther movie") select new { s.snippet };
            var resultTitle = from s in wiki.Query.search("Black Panther movie") select new { s.sectiontitle };
            Write(resultSnippet);
            Write(resultTitle);
        }

        private static void Write<TSource, TResult>(WikiQueryResult<TSource, TResult> results)
        {
            Write(results.ToEnumerable().Take(10));
            
        }

        private static void Write<T>(IEnumerable<T> results)
        {
            var array = results.ToArray();

            foreach (var result in array)
                Console.WriteLine(result);

            Console.WriteLine("Total: {0}", array.Length);
        }
        #endregion
    }
}
