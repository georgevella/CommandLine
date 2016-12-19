using System;
using System.IO;
using CommandLine.Attributes;

namespace TestCliApp.Commands
{
    [Command]
    public class FileCommand
    {
        [Action]
        public void Details(FileInfo path)
        {
            Console.WriteLine($"{path.FullName}");
        }
    }
}