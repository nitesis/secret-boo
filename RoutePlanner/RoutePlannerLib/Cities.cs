using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {

        private List<City> cityList;
        public int Count
        {
            get
            {
                return cityList.Count;
            }
        }

        public City this[int index]
        {
            get{
                 if (index < 0 || index > Count-1)
                 {
                     return null;
                 }
                 return this.cityList[index];
            }
            set
            {
                this.cityList[index] = value;
            }
        }

        public Cities()
        {
            cityList = new List<City>();
        }

        

        public int ReadCities(string filename)
        {
            TextReader reader = new StreamReader(filename); 
            String line = reader.ReadLine();
            int count = 0;
            while (line != null)
            {
                String[] lineSplit = line.Split('\t');
                cityList.Add(new City(lineSplit[0], lineSplit[1], int.Parse(lineSplit[2]), Double.Parse(lineSplit[3], CultureInfo.InvariantCulture), Double.Parse(lineSplit[4], CultureInfo.InvariantCulture)));
                count++;
                line = reader.ReadLine();
            }
            return count; 
            
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            List<City> neighbours=new List<City>();
            City city;
            double d;
            //d = R arccos [sin (φa) •sin(φb) + cos(φa) • cos(φb) • cos(λa - λb)
            for(int i=0; i<Count; i++)
            {
                city=cityList[i];
                d = location.Distance(city.Location);
                if (d<= distance)
                {
                    neighbours.Add(city);
                }

            }
            return neighbours;
        }

        public City FindCity(string cityName)
        {
            Predicate<City> predicateDelegate = delegate(City c)
            {
                return c.Name.ToUpper().Equals(cityName.ToUpper());
            };

            return cityList.Find(predicateDelegate);
        }

    }
}
;