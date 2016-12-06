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
}