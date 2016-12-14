using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class ULongValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return ulong.Parse(rawArgument);
        }
    }
}