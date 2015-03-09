using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestEventArgs: System.EventArgs 
    {
       public  String FromCity {get;set;}
       public String ToCity { get; set; }
       public TransportModes Mode { get; set; }

    
        public RouteRequestEventArgs(String from, String to , TransportModes mode)
        {
            this.FromCity = from;
            this.ToCity = to;
            this.Mode = mode;
        }
    }
}
