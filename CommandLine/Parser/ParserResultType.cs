namespace CommandLine.Parser
{
    internal enum ParserResultType
    {
        ExecuteCommand,
        ShowHelpText,
        UnknownCommand,
        UnknownArgument,
        MissingArgumentValue,
        ArgumentValueInvalid
    }
}