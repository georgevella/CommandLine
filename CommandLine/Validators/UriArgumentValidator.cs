using System;
using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class UriArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            Uri uri;
            return Uri.TryCreate(rawArgument, UriKind.Absolute, out uri);
        }
    }
}