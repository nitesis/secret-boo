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

        }
       public int GetCityRequests(string cityName)
        {
            return 0;
        }

       
    }
}
