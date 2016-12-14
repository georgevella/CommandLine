using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class ShortValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return short.Parse(rawArgument);
        }
    }
}