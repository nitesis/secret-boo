using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectWriter
        
    {
        StringWriter stream;

        public SimpleObjectWriter(StringWriter str)
        {
            this.stream = str;
        }

        public void Next(Object obj)
        {
            {

                if (stream != null && obj != null)
                {
                    stream.Write("Instance of " + obj.GetType().FullName + "\r\n");
                    foreach (var p in obj.GetType().GetProperties())
                    {
                        if (p.GetCustomAttributes(false).Any(a => a is XmlIgnoreAttribute))
                        {
                            continue;
                        }

                        var propType = p.GetValue(obj);
                        if (propType is string)
                        {
                            stream.WriteLine(p.Name + "=\"" + p.GetValue(obj) + "\"", CultureInfo.InvariantCulture);
                        }
                        else
                            if (propType is System.ValueType)
                            {

                                Object temp = p.GetValue(obj);
                               // string objString = p.GetValue(obj).ToString();
                                string s ;
                                if (temp is float)
                                    s = p.Name + "=" + ((float)temp).ToString(CultureInfo.InvariantCulture);
                                else if (temp is double)
                                    s = p.Name + "=" + ((double)temp).ToString(CultureInfo.InvariantCulture);
                                else
                                 s = p.Name + "=" + p.GetValue(obj);

                                
                                stream.WriteLine(s, CultureInfo.InvariantCulture);
                            }

                        else
                           {
                               stream.WriteLine(p.Name + " is a nested object...", CultureInfo.InvariantCulture);
                            this.Next(p.GetValue(obj));
                           }
                    }
                    stream.WriteLine("End of instance", CultureInfo.InvariantCulture);
                }
            }
        }
    }
}
