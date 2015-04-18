using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    [Serializable]
    public class City
    { 
        public string Name { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public WayPoint Location { get; set; }
        

        public City()
        {
            this.Name = "";
            this.Country = "";
        }

        public City(string _name, string _country, int _population, double _latitude, double _longitude)
        {
            Name = _name;
            Country = _country;
            Population = _population;
            Location = new WayPoint(_name, _latitude, _longitude);
            
        }

      public override bool Equals(Object obj)
        {
            if ((obj == null))
                return false;
            else
            {
                if(this.Name.Equals(((City)obj).Name, StringComparison.InvariantCultureIgnoreCase))
                
                {
                    if (this.Country.Equals(((City)obj).Country, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
            }
          return false;
           
        }

    }
}
