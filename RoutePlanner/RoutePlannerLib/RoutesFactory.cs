
using RoutePlannerLib.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesFactory
    {
        static public IRoutes Create(Cities cities)
        {
            return Create(cities, Settings.Default.RouteAlgorithm);
        }
        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            return null;
        }
    }
}
