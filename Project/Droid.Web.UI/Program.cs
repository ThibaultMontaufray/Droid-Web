using CefSharp;
using CefSharp.WinForms;
using Droid.Web.UI.View;
using System;
using System.Windows.Forms;

namespace Droid.Web.UI
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        static void Main()
        {
            try
            {
                var settings = new CefSettings();
                settings.BrowserSubprocessPath = @"x86\CefSharp.BrowserSubprocess.exe";

                Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
               
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception exp)
            {
                Console.WriteLine("NAOOON, Fatal error : " + exp.Message);
            }

            // TEST WIKIPEDIA
            //Wikipedia.Test();

            // DEMO TAKE PICTURE
            //Web.GetLuckyImage("\"jason mraz\" music artist profile");

            // DEMO FOR NETWORK

            // STARTING FOR SERVICE
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new TS_Web()
            //};
            //ServiceBase.Run(ServicesToRun);
        }
    }
}
