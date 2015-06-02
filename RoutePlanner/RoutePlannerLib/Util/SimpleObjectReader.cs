using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Globalization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
     public class SimpleObjectReader
    {
        
        TextReader stream;

        public SimpleObjectReader(TextReader str)
        {
            this.stream = str;
        }

        public Object Next()
        {
            if(stream != null) 
            {
                var s = stream.ReadLine();
                if(s.Contains("Instance of "))
                {
                    var splits = s.Split(' ');                 
                    var obj = Activator.CreateInstance(Type.GetType(splits[2]));
                    
                    while (obj !=null && s != null && !s.Contains("End of instance"))
                    {
                        String[] stringSplit;
                        PropertyInfo prpinfo;
                        if (s.Contains("is a nested object"))
                        {
                 
                            stringSplit = s.Split(' ');
                            prpinfo = obj.GetType().GetProperty(stringSplit[0]);
                            Console.WriteLine(stringSplit[0]);
                            prpinfo.SetValue(obj, this.Next());
                        }
                        else
                        {
                            stringSplit = s.Split('=');
                            prpinfo = obj.GetType().GetProperty(stringSplit[0]);
                            if (prpinfo != null)
                            {
                                if (prpinfo.GetValue(obj) is string)
                                {
                                    string str=stringSplit[1].Trim('\"');
                                    prpinfo.SetValue(obj, str);
                                }
                                else if (prpinfo.GetValue(obj) is System.ValueType)
                                {
                                    if (prpinfo.GetValue(obj) is double)
                                    {

                                        prpinfo.SetValue(obj, double.Parse(stringSplit[1], NumberStyles.Float, CultureInfo.InvariantCulture));
                                    }
                                    else if (prpinfo.GetValue(obj) is int)
                                    {
                                        prpinfo.SetValue(obj, int.Parse(stringSplit[1], NumberStyles.Integer, CultureInfo.InvariantCulture));
                                    }
                                }
                            }
                        }
                        s = stream.ReadLine();
                    }
                    return obj;
                }
            }
            return null;
        }
        }
    }

