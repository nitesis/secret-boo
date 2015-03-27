
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
            if (Type.GetType(algorithmClassName) != null)
            {
                try
                {
                    Assembly a = Assembly.LoadFrom("RoutePlannerLib.dll");
                    var tOfAssembly = a.GetType(algorithmClassName);
                    if (tOfAssembly != null)
                    {
                        Type type = Type.GetType(algorithmClassName);
                        ConstructorInfo ctor = type.GetConstructor(new[] { typeof(Cities) });
                        object instance = ctor.Invoke(new object[] { cities });
                        return (IRoutes)instance;
                    }
                }
                catch (Exception) { }
            }
            return null;         
        }
    }
}
