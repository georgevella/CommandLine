using System;
using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class DateTimeArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            DateTime value;
            return DateTime.TryParse(rawArgument, out value);
        }
    }
}