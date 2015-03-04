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
       
        public const int Radius = 6371;

    
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
                    return ("WayPoint: " + Latitude + "/" + Longitude);
                }
           /* "WayPoint: {0} {1:N2}/{2:N2}" */
                return ("WayPoint: " + Name + " " + Latitude + "/" + Longitude);
                
                
            }

        public double Distance(WayPoint target)
        {

                double dAB;
                double c;
                double d;
                WayPoint a = this;
                WayPoint b=target;
                dAB = a.Longitude - b.Longitude;
                c = (Math.Sin(a.Latitude) * Math.Sin(b.Latitude)) + (Math.Cos(a.Latitude) * Math.Cos(b.Latitude) * Math.Cos(dAB));
                d = Radius * Math.Acos(c);
                return d;

          /*  double distance;
            distance = Radius * Math.Acos (Math.Sin (this.Latitude) * Math.Sin(target.Latitude) + Math.Cos(this.Latitude) * Math.Cos(target.Latitude) * Math.Cos(this.Longitude - target.Longitude));
            return distance; */

        }
 

    }
}
