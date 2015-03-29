﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

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
                        var propType = p.GetValue(obj);
                        if (propType is string)
                        {
                            stream.Write(p.Name + "=\"" + p.GetValue(obj) + "\"\r\n");
                        }
                        else
                            if (propType is System.ValueType)
                            {

                                string s = p.Name + "=" + p.GetValue(obj) + "\r\n";
                                s=s.Replace(",", ".");
                                stream.Write(s);
                            }
                        
                           else
                           {
                            stream.Write(p.Name + " is a nested object...\r\n");
                            this.Next(p.GetValue(obj));
                           }
                    }
                    stream.Write("End of instance\r\n");
                }
            }
        }
    }
}
