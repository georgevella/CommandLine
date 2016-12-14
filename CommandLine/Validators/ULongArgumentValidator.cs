using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class ULongArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            ulong value;
            return ulong.TryParse(rawArgument, out value);
        }
    }
}