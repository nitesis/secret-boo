using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;


namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    /// <summary>
    /// Manages a routes from a city to another city.
    /// </summary>
    public class Routes : IRoutes
    {
        List<Link> routes = new List<Link>();
        Cities cities;
        public delegate void RouteRequestHandler(object sender, RouteRequestEventArgs e);
        public event RouteRequestHandler RouteRequestEvent; 
        public int Count
        {
            get { return routes.Count; }
        }

        public Routes()
        {

        }

        /// <summary>
        /// Initializes the Routes with the cities.
        /// </summary>
        /// <param name="cities"></param>
        public Routes(Cities cities)
        {
            this.cities = cities;
        }

        /// <summary>
        /// Reads a list of links from the given file.
        /// Reads only links where the cities exist.
        /// </summary>
        /// <param name="filename">name of links file</param>
        /// <returns>number of read route</returns>
        public int ReadRoutes(string filename)
        {
            using (TextReader reader = new StreamReader(filename))
            {

                IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');

                string fromCity;
                string toCity;
                double distance;
                City city1;
                City city2;
                foreach (string[] cs in citiesAsStrings)
                {
                    fromCity = cs[0];
                    toCity = cs[1];
                    city1 = cities.FindCity(fromCity);
                    city2 = cities.FindCity(toCity);

                    if ((city1 != null) && (city2 != null))
                    {
                        distance = city1.Location.Distance(city2.Location);
                        routes.Add(new Link(city1, city2, distance, TransportModes.Rail));
                    }

                }
            }


            return Count;

        }

        public List<Link> FindShortestRouteBetween(string fromCity, string toCity,
                                        TransportModes mode)
        {

            if (RouteRequestEvent != null)
            {
                RouteRequestEvent(this,new RouteRequestEventArgs(fromCity, toCity, mode));
            }
            City cityFrom = cities.FindCity(fromCity);
            City cityTo = cities.FindCity(toCity);
            return FindShortestRouteBetween(cityFrom, cityTo, mode);
        }

        #region Lab04: Dijkstra implementation
        public List<Link> FindShortestRouteBetween(City fromCity, City toCity, TransportModes mode)
        {
            var citiesBetween = cities.FindCitiesBetween(fromCity, toCity);
            if (citiesBetween == null || citiesBetween.Count < 1 || routes == null || routes.Count < 1)
                return null;

            var source = citiesBetween[0];
            var target = citiesBetween[citiesBetween.Count - 1];

            Dictionary<City, double> dist;
            Dictionary<City, City> previous;
            var q = FillListOfNodes(citiesBetween, out dist, out previous);
            dist[source] = 0.0;

            // the actual algorithm
            previous = SearchShortestPath(mode, q, dist, previous);

            // create a list with all cities on the route
            var citiesOnRoute = GetCitiesOnRoute(source, target, previous);
            // prepare final list of links
            return FindPath(citiesOnRoute, mode);
        }

        public List<Link> FindPath(List<City> cityList, TransportModes mode)
        {
            if (cityList == null || cityList.Count==1)
                return null;
            else
            {
                List<Link> links = new List<Link>();
                for (int i = 1; i < cityList.Count; i++)
                {
                    links.Add(FindLink(cityList[i - 1], cityList[i], mode));
                }
                return links;
            } 
             
        }

        private static List<City> FillListOfNodes(List<City> cities, out Dictionary<City, double> dist, out Dictionary<City, City> previous)
        {
            var q = new List<City>(); // the set of all nodes (cities) in Graph ;
            dist = new Dictionary<City, double>();
            previous = new Dictionary<City, City>();
            foreach (var v in cities)
            {
                dist[v] = double.MaxValue;
                previous[v] = null;
                q.Add(v);
            }

            return q;
        }

        /// <summary>
        /// Searches the shortest path for cities and the given links
        /// </summary>
        /// <param name="mode">transportation mode</param>
        /// <param name="q"></param>
        /// <param name="dist"></param>
        /// <param name="previous"></param>
        /// <returns></returns>
        private Dictionary<City, City> SearchShortestPath(TransportModes mode, List<City> q, Dictionary<City, double> dist, Dictionary<City, City> previous)
        {
            while (q.Count > 0)
            {
                City u = null;
                var minDist = double.MaxValue;
                // find city u with smallest dist
                foreach (var c in q)
                    if (dist[c] < minDist)
                    {
                        u = c;
                        minDist = dist[c];
                    }

                if (u != null)
                {
                    q.Remove(u);
                    foreach (var n in FindNeighbours(u, mode))
                    {
                        var l = FindLink(u, n, mode);
                        var d = dist[u];
                        if (l != null)
                            d += l.Distance;
                        else
                            d += double.MaxValue;

                        if (dist.ContainsKey(n) && d < dist[n])
                        {
                            dist[n] = d;
                            previous[n] = u;
                        }
                    }
                }
                else
                    break;

            }

            return previous;
        }

        public Link FindLink(City u,City n,TransportModes mode)
            {
               
                foreach(Link l in routes){
                    if ((l.FromCity.Equals(u))&&(l.ToCity.Equals(n))&& (l.TransportMode==mode))
                        return l;

                    if ((l.FromCity.Equals(n))&&(l.ToCity.Equals(u))&& (l.TransportMode==mode))
                        return l;
                }
                return null;
            }


        /// <summary>
        /// Finds all neighbor cities of a city. 
        /// </summary>
        /// <param name="city">source city</param>
        /// <param name="mode">transportation mode</param>
        /// <returns>list of neighbor cities</returns>
        private List<City> FindNeighbours(City city, TransportModes mode)
        {
            var neighbors = new List<City>();
            foreach (var r in routes)
                if (mode.Equals(r.TransportMode))
                {
                    if (city.Equals(r.FromCity))
                        neighbors.Add(r.ToCity);
                    else if (city.Equals(r.ToCity))
                        neighbors.Add(r.FromCity);
                }

            return neighbors;
        }

        private List<City> GetCitiesOnRoute(City source, City target, Dictionary<City, City> previous)
        {
            var citiesOnRoute = new List<City>();
            var cr = target;
            while (previous[cr] != null)
            {
                citiesOnRoute.Add(cr);
                cr = previous[cr];
            }
            citiesOnRoute.Add(source);

            citiesOnRoute.Reverse();
            return citiesOnRoute;
        }

        public City[] FindCities(TransportModes transportMode)
        {
            List<City> listOfCities = new  List<City>();
            routes.ForEach(r => { 
                if (r.TransportMode == transportMode) 
                  { listOfCities.Add(r.FromCity); listOfCities.Add(r.ToCity); }
            });

            return listOfCities.Distinct().ToArray();
        }
        #endregion

    }


}

