using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;
using System.Diagnostics;


namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    /// <summary>
    /// Manages a routes from a city to another city.
    /// </summary>
    public abstract class Routes : IRoutes
    {
        protected List<Link> routes = new List<Link>();
        protected Cities cities;
        private static TraceSource RoutesFileLog = new TraceSource("Routes");
        public delegate void RouteRequestHandler(object sender, RouteRequestEventArgs e);
        public event RouteRequestHandler RouteRequestEvent;

        public bool ExecuteParallel { get; set; }
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
            RoutesFileLog.TraceEvent(TraceEventType.Information, 3, "ReadRoutes started");
            RoutesFileLog.Flush();

            try
            {
                using (TextReader reader = new StreamReader(filename))
                {
                    IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');

                    string fromCity;
                    string toCity;
                    double distance;
                    City city1;
                    City city2;

                    /*  city1.Select(cs =>
                          {
                              var fromCity = cs[0];
                        
                              return new { fromCity, toCity, distance}).Where(cs => cs.fromCity!=null && cs.toCity!=null).Select(cs => new Link(cs.fromCity, cs,toCity))
                    */
                    foreach (string[] cs in citiesAsStrings)
                    {
                        fromCity = cs[0];
                        toCity = cs[1];
                        city1 = cities.FindCity(fromCity.Trim());
                        city2 = cities.FindCity(toCity.Trim());

                        if ((city1 != null) && (city2 != null))
                        {
                            distance = city1.Location.Distance(city2.Location);
                            routes.Add(new Link(city1, city2, distance, TransportModes.Rail));
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                RoutesFileLog.TraceEvent(TraceEventType.Critical, 9, e.ToString());
                RoutesFileLog.Flush();

            }

            RoutesFileLog.TraceEvent(TraceEventType.Information, 4, "ReadRoutes ended");
            RoutesFileLog.Flush();

            return Count;
        }

        

        public abstract List<Link> FindShortestRouteBetween(string cityFrom, string cityTo, TransportModes mode);

        public Task<List<Link>> FindShortestRouteBetweenAsync(string cityFrom, string cityTo, TransportModes mode) 
        {
            return FindShortestRouteBetweenAsync(cityFrom, cityTo, mode, null);
        }

        public abstract Task<List<Link>> FindShortestRouteBetweenAsync(string cityFrom, string cityTo, TransportModes mode, IProgress<string> progress);
        
            
        
        public void NotifyObservers(string cityFrom, string cityTo, TransportModes mode)
        {
            if (RouteRequestEvent != null)
            {
                RouteRequestEvent(this, new RouteRequestEventArgs(cityFrom, cityTo, mode));
            }
        }

    }


}

