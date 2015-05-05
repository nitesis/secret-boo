using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* gcConcurrent: true, GCCpuGroup false, gcServer: false -> 41 ms */

            /* gcConcurrent: true, GCCpuGroup true, gcServer: false -> 29 ms */
            /* gcConcurrent: true, GCCpuGroup true, gcServer: true -> 71 ms */
            /* gcConcurrent: true, GCCpuGroup false, gcServer: true -> 57 ms */

            /* gcConcurrent: false, GCCpuGroup false, gcServer: true -> 40 ms */
            /* gcConcurrent: false, GCCpuGroup false, gcServer: false -> 73 ms */
            /* gcConcurrent: false, GCCpuGroup true, gcServer: false -> 55 ms */
            /* gcConcurrent: false, GCCpuGroup true, gcServer: true -> 62 ms */

            /* Best performance with all default, except: GCCpuGroup = true */
            long memoryUsedBefore = GC.GetTotalMemory(false);
            Stopwatch s = Stopwatch.StartNew();
            generateObject(200);
            
            s.Stop();
            Console.WriteLine("Time elapsed: {0} milliseconds", s.ElapsedMilliseconds);
            Console.WriteLine("Total Memory before: {0} and Total Memory after: {1}", memoryUsedBefore, GC.GetTotalMemory(false));
            Console.ReadKey();
            
        }

        public static void generateObject(int n)
        {
            for (int i = 0; i < n; i++)
            {
                // makes n 100MB objects
                var temp = new Byte[100000];
            }
        }
            
    }
}
