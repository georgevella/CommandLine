using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLine.Attributes;
using CommandLine.Factories;
using CommandLine.HelpText;
using CommandLine.Internals.Commands;
using CommandLine.Internals.Extensions;
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

        public CommandLineInterface()
        {

        }

        public void Register<T>() where T : class
        {
            _commandDescriptors.Add(CommandDescriptorFactory.CreateFor<T>());
        }

        public void Run(string[] args)
        {
            // register help command last
            Register<HelpCommand>();

            // build application description
            var applicationDescription = ApplicationDescriptionFactory.BuildFromAllCommandsInAssembly(_commandDescriptors);

            // parse
            var parser = new Parser.Parser(applicationDescription, new ParserConfiguration());
            var parserResult = parser.Parse(args);

            if (parserResult.Result == ParserResultType.ExecuteCommand)
            {
                var commandType = parserResult.Command.Type;

                var defaultConstructor = commandType.GetConstructor(new Type[] { });
                if (defaultConstructor != null)
                {
                    Command = Activator.CreateInstance(commandType);
                }
                else
                {
                    var constructorWithAppDescription =
                        commandType.GetConstructor(new[] { typeof(ApplicationDescription) });
                    if (constructorWithAppDescription != null)
                    {
                        Command = Activator.CreateInstance(commandType, new[] { applicationDescription });
                    }
                }
                var invoker = new CommandInvoker(parserResult.Command, parserResult.Action, parserResult.Arguments,
                    Command);

                invoker.Run();
            }
            else
            {
                // TODO: show help text
                new HelpTextPrinter(applicationDescription).PrintCommands();
            }
        }

        public object Command { get; private set; }
    }
}