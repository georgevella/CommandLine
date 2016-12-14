using System;
using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class UriValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return new Uri(rawArgument, UriKind.Absolute);
        }
    }
}