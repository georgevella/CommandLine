using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class UShortValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return ushort.Parse(rawArgument);
        }
    }
}