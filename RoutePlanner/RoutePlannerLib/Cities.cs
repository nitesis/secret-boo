using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        

        public List<City> cityList;
        private static TraceSource FileLog { get; set; }
        public int Count
        {
            get
            {
                return cityList.Count;
                
            }
        }

        public City this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
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
            FileLog = new TraceSource("Cities");
        }


        
        public int ReadCities(string filename)
        {
            int count = 0;
            using (TextReader reader = new StreamReader(filename))
            {
                FileLog.TraceEvent(TraceEventType.Information, 1, "ReadCities started");
                IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');

                List<City> result = citiesAsStrings.Select(cs => new City(cs[0].Trim(), cs[1].Trim(),
                        int.Parse(cs[2]),
                    double.Parse(cs[3], CultureInfo.InvariantCulture),
                    double.Parse(cs[4], CultureInfo.InvariantCulture))).ToList();

                cityList = cityList.Concat(result).ToList();
                count = result.Count();

            }
            FileLog.TraceEvent(TraceEventType.Information, 2, "ReadCities ended");
            return count;
            
        }
        

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            var neighbours = cityList.Where(c => location.Distance(c.Location) <= distance);
            return neighbours.OrderBy(o => location.Distance(o.Location)).ToList();
        }


        public City FindCity(string cityName)
        {
            Predicate<City> predicateDelegate;
            return cityList.Find (predicateDelegate = c => c.Name.ToUpper().Equals(cityName.ToUpper()));
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