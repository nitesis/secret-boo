using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public static class TextReaderExtension
    {
        public static IEnumerable<string[]> GetSplittedLines(this TextReader reader, char param)
        {


            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return line.Split(param);
            }
            
            //string line = reader.ReadLine();
            //while (line != null)
            //{
            //    String[] lineSplit = line.Split(param);
            //    yield return lineSplit;
                
                   
            //    line = reader.ReadLine();
            //}
        }
    }
}
