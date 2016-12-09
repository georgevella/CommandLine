using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CommandLine.Model
{
    public class Arguments : IEnumerable<ParsedArgument>
    {
        private readonly Dictionary<string, ParsedArgument> _parsedArguments;

        public Arguments(IEnumerable<ParsedArgument> parsedArguments)
        {
            _parsedArguments = parsedArguments.ToDictionary(x => x.Descriptor.Name);
        }

        public Arguments()
        {
            _parsedArguments = new Dictionary<string, ParsedArgument>();
        }

        public bool Contains(string argument)
        {
            return _parsedArguments.ContainsKey(argument);
        }

        public ParsedArgument this[string argument] => _parsedArguments[argument];

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<ParsedArgument> GetEnumerator()
        {
            return _parsedArguments.Values.GetEnumerator();
        }
    }
}