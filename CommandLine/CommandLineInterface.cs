using System;
using System.Reflection;
using CommandLine.Factories;
using CommandLine.Model;
using CommandLine.Parser;

namespace CommandLine
{
    public class CommandLineInterface
    {
        private ApplicationDescription _applicationDescription;

        public void AutoDiscoverCommands()
        {
            var callingAssembly = Assembly.GetCallingAssembly();

            _applicationDescription = ApplicationDescriptionFactory.BuildFromAllCommandsInAssembly(callingAssembly);
        }

        public void Run(string[] args)
        {
            if (_applicationDescription == null)
                AutoDiscoverCommands();

            var parser = new Parser.Parser(_applicationDescription, new ParserConfiguration());

            var parserResult = parser.Parse(args);

            if (parserResult.Result == ParserResultType.ExecuteCommand)
            {
                var commandType = parserResult.Command.Type;
                var instance = Activator.CreateInstance(commandType);
                var invoker = new CommandInvoker(parserResult.Command, parserResult.Action, parserResult.Arguments, instance);

                invoker.Run();
            }

            // TODO: show help text
        }
    }
}