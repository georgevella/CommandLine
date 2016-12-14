using System;
using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class DateTimeValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return DateTime.Parse(rawArgument);
        }
    }
}