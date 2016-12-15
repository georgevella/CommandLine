using System.ComponentModel;
using CommandLine.Attributes;

namespace TestCliApp.Commands
{
    [Command]
    [Description("Client configuration manager.")]
    public class ConfigCommand
    {
        [Action(Name = "get", Default = true)]
        [Description("Gets the value assigned to the configuration key.")]
        public int Get(
            [Argument(ShortName = 'n')] [Description("Name of configuration item.")] string name)
        {
            return 0;
        }

        [Action]
        [Description("Sets the value assigned to the configuration key.")]
        public int Set(
            [Argument(ShortName = 'n')] [Description("Name of configuration item.")] string name,
            [Argument(ShortName = 's')] [Description("New value for configuration item.")] string value)
        {
            return 0;
        }
    }
}