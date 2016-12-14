using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class UIntArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            uint value;
            return uint.TryParse(rawArgument, out value);
        }
    }
}