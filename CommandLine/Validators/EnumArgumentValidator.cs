using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class EnumArgumentValidator : IArgumentValidator
    {
        private readonly List<string> _values;

        public EnumArgumentValidator(Type enumType)
        {
            _values = Enum.GetValues(enumType).Cast<object>().Select(x => x.ToString().ToLower()).ToList();
        }

        public bool IsValid(string rawArgument)
        {
            return _values.Contains(rawArgument.ToLower());
        }
    }
}