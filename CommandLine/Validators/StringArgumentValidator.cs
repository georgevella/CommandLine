using System.IO;
using System.Reflection;
using CommandLine.Contracts;
using CommandLine.Model;

namespace CommandLine.Validators
{
    public class StringArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            if (!string.IsNullOrEmpty(rawArgument))
                return true;

            return false;
        }
    }
}