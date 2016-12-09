using CommandLine.Model;

namespace CommandLine.Parser
{
    internal class ParserResult
    {
        public ParserResultType Result { get; }
        public Arguments Arguments { get; }
        public CommandDescriptor Command { get; }
        public ActionDescriptor Action { get; set; }
        public ArgumentDescriptor ArgumentDescriptor { get; }

        public ParserResult(
            ParserResultType result,
            CommandDescriptor command = null,
            ActionDescriptor actionDescriptor = null,
            ArgumentDescriptor argumentDescriptor = null,
            Arguments arguments = null)
        {
            Result = result;
            Action = actionDescriptor;
            ArgumentDescriptor = argumentDescriptor;
            Arguments = arguments;
            Command = command;
        }
    }
}