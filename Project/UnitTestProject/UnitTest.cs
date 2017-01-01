using Droid_web;
using NUnit.Framework;
using System;
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
    }
}
