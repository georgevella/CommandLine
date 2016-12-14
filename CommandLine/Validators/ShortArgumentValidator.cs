using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class ShortArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            short value;
            return short.TryParse(rawArgument, out value);
        }
    }
}