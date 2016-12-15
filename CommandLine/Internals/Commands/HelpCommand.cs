using CommandLine.Attributes;
using CommandLine.HelpText;
using CommandLine.Model;

namespace CommandLine.Internals.Commands
{
    [Command(Name = "help")]
    public class HelpCommand
    {
        private readonly ApplicationDescription _applicationDescription;

        public HelpCommand(ApplicationDescription applicationDescription)
        {
            _applicationDescription = applicationDescription;
        }

        [Action]
        public void Run([Argument(IsOptional = true)]string command)
        {
            var htp = new HelpTextPrinter(_applicationDescription);

            if (string.IsNullOrEmpty(command))
            {
                htp.PrintCommands();
            }
            else
            {
                htp.PrintCommandHelp(command);
            }
        }
    }
}