using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCliApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Arguments:");

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"[{i}]: {args[i]}");
            }

            Console.WriteLine("done.");
        }
    }
}
