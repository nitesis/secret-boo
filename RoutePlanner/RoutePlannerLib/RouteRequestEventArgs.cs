﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestEventArgs: System.EventArgs 
    {
        String formCity { get; set; }
        String toCity { get; set; }
        TransportModes mode { set; get; }

    
        public RouteRequestEventArgs(String from, String to , TransportModes mode)
        {
            this.formCity = from;
            this.toCity = to;
            this.mode = mode;
        }
    }
}
