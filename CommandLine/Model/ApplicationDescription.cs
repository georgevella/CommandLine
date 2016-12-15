using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommandLine.Model
{
    public class ApplicationDescription
    {
        private readonly Dictionary<string, CommandDescriptor> _commandMap;

        public ApplicationDescription(IEnumerable<CommandDescriptor> commands)
        {
            _commandMap = commands.ToDictionary(x => x.Command);
        }

        public IEnumerable<CommandDescriptor> Commands => _commandMap.Values;

        public bool IsKnownCommand(string command)
        {
            return _commandMap.ContainsKey(command.ToLower());
        }

        public CommandDescriptor GetCommand(string command)
        {
            CommandDescriptor descriptor;

            if (_commandMap.TryGetValue(command.ToLower(), out descriptor))
                return descriptor;

            return null;
        }
    }
}