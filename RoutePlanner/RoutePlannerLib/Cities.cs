using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        List<City> cityList = new List<City>();
        int count;
        public int ReadCities(string filename)
        {
            TextReader reader = new StreamReader(filename); 
            String line = reader.ReadLine();
            count = 0;
            while (line != null)
            {
                String[] lineSplit = line.Split('\t');
                City newCity = new City(lineSplit[0], lineSplit[1], Convert.ToInt32(lineSplit[2]), Convert.ToDouble(lineSplit[3]), Convert.ToDouble(lineSplit[4]));
            
                cityList.Add(newCity);
                count++;
                line = reader.ReadLine();
            }
            return count; 
            
        }

        public City GetCityPerIndex(int index)
        {
            if (index < count || index > count)
                return null;
            else
                return cityList.ElementAt(index);
        }

        public int GetCount()
        {
            return count;
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            List<City> neighbours=new List<City>();
            City city;
            var R=6371;
            double dAB;
            double c;
            double d;
            WayPoint a=location;
            WayPoint b;
            //d = R arccos [sin (φa) •sin(φb) + cos(φa) • cos(φb) • cos(λa - λb)
            for(int i=0; i<count; i++)
            {
                city=GetCityPerIndex(i);
                b=city.Location;
                dAB=a.Longitude- b.Longitude;
                c=(Math.Sin(a.Latitude)*Math.Sin(b.Latitude)) + (Math.Cos(a.Latitude)*Math.Cos(b.Latitude)*Math.Cos(dAB));
                d=R*c;
                if (d<= distance)
                {
                    neighbours.Add(city);
                }

            }
            return neighbours;
        }

    }
}
;