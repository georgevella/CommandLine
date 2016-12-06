using CommandLine.Attributes;

namespace CommandLine.Tests.Commands
{
    [Command]
    public class VersionCommand
    {
        [Action]
        public int Main()
        {
            return 0;
        }
    }

    [Command]
    public class CommandWithNoActions
    {
        public int Main()
        {
            return 0;
        }
    }
}