using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class BooleanValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            var value = rawArgument.ToLower();
            return bool.TrueString.ToLower().Equals(value) ||
                   "yes".Equals(value);
        }
    }
}