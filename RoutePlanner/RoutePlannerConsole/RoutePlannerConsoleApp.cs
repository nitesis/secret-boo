using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System.Diagnostics;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        private static TraceSource routesLogger = new TraceSource("Routes");
        static void Main(string[] args)     
        {
            
            Console.WriteLine("Welcome to RoutePlanner {0}",
                    Assembly.GetExecutingAssembly().GetName().Version);
            var wayPointWindisch = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            var wayPointBern = new WayPoint("Bern", 46.948811, 7.444480); 
            var wayPointTripolis = new WayPoint("Tripolis", 32.804241, 13.098579);
            var cityBern = new City("Bern", "Schweiz", 75000, 47.479319847061966, 8.212966918945312);
            var cities = new Cities();
            City city;
            Console.WriteLine("{0}", wayPointWindisch);
            Console.WriteLine("{0} {1}", "Distance Bern - Tripolis: ", wayPointBern.Distance(wayPointTripolis));
            /* txt file has to be here ..\secret-boo\RoutePlanner\RoutePlannerConsole\bin\Debug\citiesTestDataLab2.txt */
            Console.WriteLine("{0} {1}", "Anzahl eingelesene Städte: ", cities.ReadCities("data/citiesTestDataLab2.txt"));
            city=cities.FindCity("IstAnbUl");
            Console.WriteLine("{0} {1}", "City Found: ", city.Name);
            Console.ReadKey();



            //Lab9 a1 c) Console & File Test of Readcities
            var routes=new Routes();
            var cities1 = new Cities();
            cities1.ReadCities("citiesTestDataLab4.txt");
            IRoutes routes2 = RoutesFactory.Create(cities);

            //Lab9 a1 b) Loading from existing file
            var count3 = routes.ReadRoutes("linksTestDataLab4.txt");

            //Lab9 a1 b) Writing to file but not to console
            routesLogger.TraceEvent(TraceEventType.Information, 01, "this should not be on the console");


            //Lab9 a1 b) Loding not existing file
            var count4 = routes.ReadRoutes("linksTestDataLab42.txt");


            Console.ReadLine();
        }

      
    }
}
