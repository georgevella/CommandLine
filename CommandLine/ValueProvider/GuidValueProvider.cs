using System;
using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class GuidValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return Guid.Parse(rawArgument);
        }
    }
}