using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class IntValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return int.Parse(rawArgument);
        }
    }
}