using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine.Model;

namespace CommandLine
{
    public class CommandInvoker
    {
        private readonly CommandDescriptor _descriptor;
        private readonly ActionDescriptor _action;
        private readonly Arguments _args;
        private readonly object _commandInstance;

        public CommandInvoker(CommandDescriptor descriptor, ActionDescriptor action, Arguments args, object commandInstance)
        {
            if (descriptor == null) throw new ArgumentNullException(nameof(descriptor));
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (commandInstance == null) throw new ArgumentNullException(nameof(commandInstance));


            var targetType = commandInstance.GetType();
            if (targetType != descriptor.Type)
                throw new InvalidOperationException();

            _descriptor = descriptor;
            _action = action;
            _args = args;
            _commandInstance = commandInstance;
        }

        public void Run()
        {
            var methodArguments = new List<object>(
                Enumerable.Repeat(default(object), _action.Arguments.Count)
                );

            foreach (var arg in _action.Arguments)
            {
                if (!_args.Contains(arg.Name))
                {
                    // argument is missing from CLI args
                    throw new InvalidOperationException();
                }

                var parsedArgument = _args[arg.Name];

                var actualValue = ChangeType(parsedArgument.Value, arg);

                arg.Setter.SetValue(methodArguments, actualValue);
            }

            _action.MethodInfo.Invoke(_commandInstance, methodArguments.ToArray());
        }

        private object ChangeType(string rawValue, ArgumentDescriptor argumentDescriptor)
        {
            return Convert.ChangeType(rawValue, argumentDescriptor.Type);
        }
    }
}