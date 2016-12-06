using System.ComponentModel;
using CommandLine.Attributes;

namespace CommandLine.Tests.Commands
{
    [Command]
    public class ConfigCommand
    {
        [Action(Name = "get", Default = true)]
        public int Get([Argument]string name)
        {
            return 0;
        }

        [Action]
        public int Set(string name, string value, ValueType type)
        {
            return 0;
        }

    }

    public enum ValueType
    {
        String,
        Integer,
        Boolean,
        Array,
        Object
    }
}