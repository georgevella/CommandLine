using System;
using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class GuidArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            Guid value;
            return Guid.TryParse(rawArgument, out value);
        }
    }
}