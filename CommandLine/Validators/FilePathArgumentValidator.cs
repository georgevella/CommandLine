using System;
using System.IO;
using CommandLine.Contracts;

namespace CommandLine.Validators
{
    public class FilePathArgumentValidator : IArgumentValidator
    {
        public bool IsValid(string rawArgument)
        {
            FileInfo fi = null;

            try
            {
                fi = new FileInfo(rawArgument);
            }
            catch (Exception) { }

            return fi != null;

        }
    }
}