using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class UIntValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return uint.Parse(rawArgument);
        }
    }
}