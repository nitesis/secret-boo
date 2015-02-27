﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        static void Main(string[] args)     
        {
            Console.WriteLine("Welcome to RoutePlanner {0}",
                    Assembly.GetExecutingAssembly().GetName().Version);
            var wayPoint = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            Console.WriteLine("{0}",wayPoint);
            Console.ReadKey();
        }

      
    }
}
