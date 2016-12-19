using System.IO;
using System.Reflection.Emit;
using CommandLine.Attributes;

namespace CommandLine.Tests.Commands
{
    [Command]
    public class RunCommand
    {
        public string Path { get; set; }

        [Action]
        public int Program(string path)
        {
            Path = path;
            return 0;
        }
    }

    [Command(Name = "run")]
    public class AdvancedRunCommand
    {
        public FileInfo Path { get; set; }

        [Action]
        public int Program(
            [Argument] FileInfo path)
        {
            Path = path;
            return 0;
        }
    }
}