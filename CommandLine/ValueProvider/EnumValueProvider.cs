using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class EnumValueProvider : IArgumentValueProvider
    {
        private readonly Dictionary<string, object> _values;

        public EnumValueProvider(Type enumType)
        {
            _values = Enum.GetValues(enumType).Cast<object>().ToDictionary(x => x.ToString().ToLower());
        }

        public object GetValue(string rawArgument)
        {
            return _values[rawArgument.ToLower()];
        }
    }
}