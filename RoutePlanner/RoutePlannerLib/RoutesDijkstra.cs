using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesDijkstra : Routes
    {
        // Konstruktor ruft Routes auf
        public RoutesDijkstra (Cities c):base(c) {}
        #region Lab04: Dijkstra implementation

        public override Task<List<Link>> FindShortestRouteBetweenAsync(string fromCity, string toCity, TransportModes mode, IProgress<string> progress)
        {   
            return Task.Run(() =>
            {
                var citiesBetween = cities.FindCitiesBetween(cities.FindCity(fromCity), cities.FindCity(toCity));
                if (progress != null)
                {
                    progress.Report("find cities between done");
                }
                if (citiesBetween == null || citiesBetween.Count < 1 || routes == null || routes.Count < 1)
                    return null;

                var source = citiesBetween[0];
                var target = citiesBetween[citiesBetween.Count - 1];

                Dictionary<City, double> dist;
                Dictionary<City, City> previous;
                var q = FillListOfNodes(citiesBetween, out dist, out previous);
                if (progress != null)
                {
                    progress.Report("fill list of nodes done");
                }
                dist[source] = 0.0;

                // the actual algorithm
                previous = SearchShortestPath(mode, q, dist, previous);
                if (progress != null)
                {
                    progress.Report("search shortesd path done");
                }

                // create a list with all cities on the route
                var citiesOnRoute = GetCitiesOnRoute(source, target, previous);
                if (progress != null)
                {
                    progress.Report("get cities on route done");
                }
                // prepare final list of links
                if (progress != null)
                {
                    progress.Report("prepare final list done");
                }
                return FindPath(citiesOnRoute, mode);
            });
            
        }
        public override List<Link> FindShortestRouteBetween(string fromCity, string toCity,
                                              TransportModes mode)
        {
            return FindShortestRouteBetweenAsync(fromCity, toCity, mode, null).Result;
        }

        public List<Link> FindPath(List<City> cityList, TransportModes mode)
        {
            if (cityList == null || cityList.Count == 1)
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

        public Link FindLink(City u, City n, TransportModes mode)
        {

            foreach (Link l in routes)
            {
                if ((l.FromCity.Equals(u)) && (l.ToCity.Equals(n)) && (l.TransportMode == mode))
                    return l;

                if ((l.FromCity.Equals(n)) && (l.ToCity.Equals(u)) && (l.TransportMode == mode))
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

            foreach (var r in routes.Where(r => r.TransportMode.Equals(mode)))
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

            var listOfCities = routes.Where(r => r.TransportMode == transportMode)
                .Select(c => c.FromCity)
                .Concat(routes.Where(r => r.TransportMode == transportMode)
                .Select(c => c.ToCity))
                .Distinct();


            return listOfCities.Distinct().ToArray();

        }
        #endregion
    }
}
