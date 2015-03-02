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

    }
}
;