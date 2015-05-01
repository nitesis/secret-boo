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
            /// GC.collect: 151 ms, MemoryBefore: 168812, MemoryAfter: 105040
            /// No GC.collect: 47 ms, MemoryBefore: 168812, MemoryAfter: 1005152
            // the true argument tells the GC to perform a collection first
            long memoryUsedBefore = GC.GetTotalMemory(false);
            Stopwatch s = Stopwatch.StartNew();
            generateObject(200);
            GC.Collect();
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
