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
               
                
                if((this.Name == null) || (this.Name == ""))
                {
                    Latitude = Latitude - (int)Latitude;
                    Longitude = Longitude - (int)Longitude;
                    return string.Format("WayPoint: {0:F2}/{1:F2}", Latitude, Longitude);
                    
                }

                return string.Format("WayPoint:{0} {1:F2}/{2:F2}", Name, Latitude, Longitude); 
                
                
            }

        

        public double Distance(WayPoint target)
        {

               double dAB;
                double c;
                double d;
               double  thisLongRad = WayPoint.ToRadians(this.Longitude);
               double thisLatRad = WayPoint.ToRadians(this.Latitude);
               double targetLongRad = WayPoint.ToRadians(target.Longitude);
               double targetLatRad = WayPoint.ToRadians(target.Latitude);
               dAB = thisLongRad - targetLongRad;
               c = (Math.Sin(thisLatRad) * Math.Sin(targetLatRad)) + (Math.Cos(thisLatRad) * Math.Cos(targetLatRad) * Math.Cos(dAB));
               d = Radius * Math.Acos(c);
               return d;

          /*  double distance;
            distance = Radius * Math.Acos (Math.Sin (this.Latitude) * Math.Sin(target.Latitude) + Math.Cos(this.Latitude) * Math.Cos(target.Latitude) * Math.Cos(this.Longitude - target.Longitude));
            return distance; */


        }

        public static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }

    }
}
