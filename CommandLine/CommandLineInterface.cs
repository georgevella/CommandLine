using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLine.Attributes;
using CommandLine.Extensions;
using CommandLine.Factories;
using CommandLine.Model;
using CommandLine.Parser;

namespace CommandLine
{
    public class CommandLineInterface
    {
        private readonly List<CommandDescriptor> _commandDescriptors = new List<CommandDescriptor>();

        public void AutoDiscoverCommands()
        {
            var callingAssembly = Assembly.GetCallingAssembly();

            _commandDescriptors.AddRange(callingAssembly.GetTypes()
                .Where(t => t.HasCustomAttribute<CommandAttribute>())
                .Select(CommandDescriptorFactory.CreateFor));


        }

        public void Register<T>() where T : class
        {
            _commandDescriptors.Add(CommandDescriptorFactory.CreateFor<T>());
        }

        public void Run(string[] args)
        {
            var applicationDescription = ApplicationDescriptionFactory.BuildFromAllCommandsInAssembly(_commandDescriptors);
            var parser = new Parser.Parser(applicationDescription, new ParserConfiguration());

            var parserResult = parser.Parse(args);

            if (parserResult.Result == ParserResultType.ExecuteCommand)
            {
                var commandType = parserResult.Command.Type;
                var instance = Command = Activator.CreateInstance(commandType);
                var invoker = new CommandInvoker(parserResult.Command, parserResult.Action, parserResult.Arguments, instance);

                invoker.Run();
            }

            // TODO: show help text
        }

        public object Command { get; private set; }
    }
}