using Droid_web;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace UnitTestProject
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void TestUTRuns()
        {
            Assert.IsTrue(true);
        }
        [Test]
        public void Test_web()
        {
            try
            {
                Web.GetJson("user", "password", "https://www.google.co.nz/");
                Web.GetPage("https://www.google.co.nz/");

                Assert.IsTrue(true);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_web_getlucky()
        {
            try
            {
                string page = Web.GetLucky("Lemon tree");
                Assert.IsNotNull(page);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_webseeker_humain()
        {
            try
            {
                WebSeeker.Search("Humain", "Asimov");
                Thread.Sleep(7000);

                Assert.IsNotNull(WebSeeker.Result);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_webseeker_article()
        {
            try
            {
                WebSeeker.Search("Article", "Robot");
                Thread.Sleep(7000);

                Assert.IsNotNull(WebSeeker.Result);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_webseeker_Carte()
        {
            try
            {
                WebSeeker.Search("Carte", "Normandie");
                Thread.Sleep(7000);

                Assert.IsNotNull(WebSeeker.Result);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_webseeker_Ingredient()
        {
            try
            {
                WebSeeker.Search("Ingredient", "sel");
                Thread.Sleep(7000);

                Assert.IsNotNull(WebSeeker.Result);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_webseeker_meteo()
        {
            try
            {
                WebSeeker.Search("Meteo", "le havre");
                Thread.Sleep(7000);

                Assert.IsNotNull(WebSeeker.Result);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_webseeker_recette()
        {
            try
            {
                WebSeeker.Search("Recette", "poulet vallée d auge");
                Thread.Sleep(7000);

                Assert.IsNotNull(WebSeeker.Result);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_webseeker_subtitle()
        {
            try
            {
                string subtitle = ParserSubtitle.Parse("Iron man 2010", "English");

                Assert.IsNotNull(subtitle);
                Assert.IsTrue(subtitle.EndsWith(".zip"));
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
        [Test]
        public void Test_webseeker_subtitle_languages()
        {
            try
            {
                List<string> languages = ParserSubtitle.Languages("Iron man 2010");

                Assert.IsNotNull(languages);
                Assert.IsTrue(languages.Count > 0);
            }
            catch (Exception exp)
            {
                Assert.Fail(exp.Message);
            }
        }
    }
}
