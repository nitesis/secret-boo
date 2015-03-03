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
        public int Count { get; set; }

        public City this[int index]
        {
            get{
                 if (index < 0 || index > Count-1)
                 {
                     return null;
                 }
                 return this.cityList.ElementAt(index);
            }
           /* set{

                if (index >0 || index < Count)
                 {
                     this.cityList.CopyTo(value,index);
                 }

            }*/
        }
        

        public int ReadCities(string filename)
        {
            TextReader reader = new StreamReader(filename); 
            String line = reader.ReadLine();
            Count = 0;
            while (line != null)
            {
                String[] lineSplit = line.Split('\t');
                City newCity = new City(lineSplit[0], lineSplit[1], Convert.ToInt32(lineSplit[2]), Convert.ToDouble(lineSplit[3]), Convert.ToDouble(lineSplit[4]));
            
                cityList.Add(newCity);
                Count++;
                line = reader.ReadLine();
            }
            return Count; 
            
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

    }
}
;