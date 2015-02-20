using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        static void Main(string[] args)     
        {
            Console.WriteLine("Welcome to our RoutePlanner {0}",
                    Assembly.GetExecutingAssembly().GetName().Version);
            Console.ReadKey();
        }
    }
}
