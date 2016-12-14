namespace CommandLine.Contracts
{
    public interface IArgumentValueProvider
    {
        object GetValue(string rawArgument);
    }
}