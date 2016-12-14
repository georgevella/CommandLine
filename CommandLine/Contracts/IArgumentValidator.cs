using CommandLine.Model;

namespace CommandLine.Contracts
{
    public interface IArgumentValidator
    {
        bool IsValid(string rawArgument);
    }
}