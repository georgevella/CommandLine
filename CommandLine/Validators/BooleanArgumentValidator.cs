using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class BooleanArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            var value = rawArgument.ToLower();
            return bool.TrueString.ToLower().Equals(value) ||
                   bool.FalseString.ToLower().Equals(value) ||
                   "yes".Equals(value) ||
                   "no".Equals(value);
        }
    }
}