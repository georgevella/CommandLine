using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class LongArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            long value;
            return long.TryParse(rawArgument, out value);
        }
    }
}