using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class UShortArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            ushort value;
            return ushort.TryParse(rawArgument, out value);
        }
    }
}