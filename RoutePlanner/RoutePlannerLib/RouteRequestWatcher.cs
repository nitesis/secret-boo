using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
   public  class RouteRequestWatcher
    {
        public Dictionary<string, int> Log = new Dictionary<string, int>();
        public void LogRouteRequests(object sender, RouteRequestEventArgs e)
        {
            if (Log.ContainsKey(e.ToCity))
            {
                Log[e.ToCity]++;
            }
            else
            {
                Log.Add(e.ToCity, 1);
            }
            
        }
        public int GetCityRequests(string cityName)
        {
            if (Log.ContainsKey(cityName))
            {
                return Log[cityName];
            }
            return 0;
        }

       
    }
}
