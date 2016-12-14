using System.IO;
using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class DirectoryInfoValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return new DirectoryInfo(rawArgument);
        }
    }
}