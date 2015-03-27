using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
     public class SimpleObjectReader
    {
        
        BinaryFormatter formatter =new BinaryFormatter();
        TextReader stream;

        public SimpleObjectReader(TextReader str)
        {
            this.stream = str;
        }

        public SimpleObjectReader()
        {

        }

        public Object Next()
        {
            return stream.ReadLine();
        }
    }
}
