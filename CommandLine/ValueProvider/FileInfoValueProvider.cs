using System.IO;
using CommandLine.Contracts;

namespace CommandLine.ValueProvider
{
    public class FileInfoValueProvider : IArgumentValueProvider
    {
        public object GetValue(string rawArgument)
        {
            return new FileInfo(rawArgument);
        }
    }
}