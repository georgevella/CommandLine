using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class IntArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            int value;
            return int.TryParse(rawArgument, out value);
        }
    }
}