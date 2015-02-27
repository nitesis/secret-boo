using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
   public  class  WayPoint
    {
    
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public WayPoint(string _name, double _latitude, double _longitude)
        {
            Name = _name;
            Latitude = _latitude;
            Longitude = _longitude;
        }


        public override string ToString()
            {
                double lat = Math.Round(this.Latitude);
                double lon=Math.Round(this.Longitude);
                if((this.Name==null )|| (this.Name==""))
                {
                    lat = lat - (int)lat;
                    lon = lon - (int)lon;
                    return ("Way point:"+ lat+ lon);
                }
                
               return ("Way point:"+ this.Name+ " "+ lat+"/" +lon);
                
                
            }

    }
}
