using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class LongValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return long.Parse(rawArgument);
        }
    }
}