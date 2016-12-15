using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace TestCliApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var cli = new CommandLineInterface();

            cli.AutoDiscoverCommands();
            cli.Run(args);

            Console.ReadKey();
        }
    }
}
