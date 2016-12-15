using CommandLine.Attributes;

namespace TestCliApp.Commands
{
    [Command]
    public class VersionCommand
    {
        [Action]
        public void Display()
        {

        }
    }
}