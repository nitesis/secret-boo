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
       
       public const int radius = 6371;

    
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
                Latitude = Math.Round(this.Latitude, 2);
                Longitude = Math.Round(this.Longitude, 2);
                
                if((this.Name == null) || (this.Name == ""))
                {
                    Latitude = Latitude - (int)Latitude;
                    Longitude = Longitude - (int)Longitude;
                    return ("Way point:" + Latitude + Longitude);
                }

                return ("Way point:" + Name + " " + Latitude + "/" + Longitude);
                
                
            }
/*
        public double Distance(WayPoint target)
        {
            double distance;

            distance = radius * arccos [sin (φa) •sin(φb) + cos(φa) • cos(φb) • cos(λa - λb)]
            return distance;

        }
 */

    }
}
