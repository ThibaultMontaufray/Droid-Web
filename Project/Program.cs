﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Droid_web
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        static void Main()
        {
            Web.GetLuckyImage("\"jason mraz\" music artist profile");

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TS_Web()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
