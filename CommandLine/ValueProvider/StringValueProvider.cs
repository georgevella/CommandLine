using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class StringValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return rawArgument;
        }
    }
}