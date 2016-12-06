using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine.Model;

namespace CommandLine
{
    public class CommandInvoker
    {
        private readonly CommandDescriptor _descriptor;
        private readonly Arguments _args;

        public CommandInvoker(CommandDescriptor descriptor, Arguments args)
        {
            _descriptor = descriptor;
            _args = args;
        }

        public void Run(object target)
        {
            var targetType = target.GetType();
            if (targetType != _descriptor.Type)
                throw new InvalidOperationException();

            if (!string.IsNullOrEmpty(_args.Action) && !_descriptor.Actions.ContainsKey(_args.Action))
                throw new InvalidOperationException();

            var actionDescriptor = string.IsNullOrEmpty(_args.Action)
                ? _descriptor.DefaultAction
                : _descriptor.Actions[_args.Action];

            var methodArguments = new List<object>(
                Enumerable.Repeat(default(object), actionDescriptor.Arguments.Count)
                );

            foreach (var arg in actionDescriptor.Arguments)
            {
                if (!_args.ContainsKey(arg.Name))
                {
                    // argument is missing from CLI args
                    throw new InvalidOperationException();
                }

                var rawValue = _args[arg.Name];

                var actualValue = ChangeType(rawValue, arg);

                arg.Setter.SetValue(methodArguments, actualValue);
            }

            actionDescriptor.MethodInfo.Invoke(target, methodArguments.ToArray());
        }

        private object ChangeType(string rawValue, ArgumentDescriptor argumentDescriptor)
        {
            return Convert.ChangeType(rawValue, argumentDescriptor.Type);
        }
    }
}