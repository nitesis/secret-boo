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
            reader.Dispose();
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

        #region Lab04: FindShortestPath helper function
        /// <summary>
        /// Find all cities between 2 cities 
        /// </summary>
        /// <param name="from">source city</param>
        /// <param name="to">target city</param>
        /// <returns>list of cities</returns>
        public List<City> FindCitiesBetween(City from, City to)
        {
            var foundCities = new List<City>();
            if (from == null || to == null)
                return foundCities;

            foundCities.Add(from);

            var minLat = Math.Min(from.Location.Latitude, to.Location.Latitude);
            var maxLat = Math.Max(from.Location.Latitude, to.Location.Latitude);
            var minLon = Math.Min(from.Location.Longitude, to.Location.Longitude);
            var maxLon = Math.Max(from.Location.Longitude, to.Location.Longitude);

            // rename the name of the "cities" variable to your name of the internal City-List
            foundCities.AddRange(cityList.FindAll(c =>
                c.Location.Latitude > minLat && c.Location.Latitude < maxLat
                        && c.Location.Longitude > minLon && c.Location.Longitude < maxLon));

            foundCities.Add(to);
            return foundCities;
        }
        #endregion

  
    }


}
;