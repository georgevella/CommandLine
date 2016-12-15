using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CommandLine.Internals;
using CommandLine.Internals.Extensions;
using CommandLine.Model;

namespace CommandLine.HelpText
{
    public class HelpTextPrinter
    {
        private const string NO_DESCRIPTION_TEXT = "(no description)";
        private readonly ApplicationDescription _applicationDescription;

        public HelpTextPrinter(ApplicationDescription applicationDescription)
        {
            _applicationDescription = applicationDescription;
        }

        public void PrintCommands()
        {
            var hostName = GetHostName();

            Console.WriteLine();
            Console.WriteLine($"Usage: {hostName} COMMAND");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine();

            var maxLength = _applicationDescription.Commands.Select(x => x.Command.Length).Max() + 5;

            foreach (var item in _applicationDescription.Commands)
            {
                var description = string.IsNullOrEmpty(item.Description) ? NO_DESCRIPTION_TEXT : item.Description;
                Console.Write($"  {item.Command}".PadRight(maxLength, ' '));
                Console.Write(" - ");
                Console.WriteLine($"{description}");
            }
        }

        private string GetHostName()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            return Path.GetFileNameWithoutExtension(entryAssembly.ManifestModule.Name).ToLower();
        }

        public void PrintCommandHelp(string command)
        {
            var hostName = GetHostName();

            var commandDescriptor = _applicationDescription.GetCommand(command);

            Console.WriteLine();

            Console.WriteLine(
                $"{hostName} {commandDescriptor.Command} - {commandDescriptor.Description.DefaultIfWhitespace(NO_DESCRIPTION_TEXT)}");

            // dump usage of command
            Console.WriteLine();
            Console.WriteLine("Usage: ");
            Console.Write($"  {hostName} {commandDescriptor.Command}");

            var commandHasArgs = false;
            if (commandDescriptor.Actions.Any())
            {
                Console.Write(" ");
                if (commandDescriptor.HasDefaultAction)
                    Console.Write("[");

                Console.Write("ACTION");

                if (commandDescriptor.HasDefaultAction)
                    Console.Write("]");

                commandHasArgs = commandDescriptor.Actions.Values.Select(x => x.Arguments.Any()).Any(x => x);
            }

            if (commandDescriptor.HasDefaultAction)
            {
                commandHasArgs |= commandDescriptor.DefaultAction.Arguments.Any();
            }

            if (commandHasArgs)
            {
                Console.WriteLine(" <options>");
            }

            // dump action details
            Console.WriteLine();

            if (commandDescriptor.Actions.Any())
            {
                Console.WriteLine("Actions: ");
                foreach (var item in commandDescriptor.Actions.Values)
                {
                    // write action description
                    Console.Write($"  {item.Description.DefaultIfWhitespace(NO_DESCRIPTION_TEXT)}");
                    if (item.IsDefault)
                    {
                        Console.Write(" (default)");
                    }
                    Console.WriteLine();

                    // write action usage details, with any non-optional parameters
                    var actionName = item.IsDefault ? $"[{item.Name}]" : item.Name;
                    Console.Write($"  > {hostName} {commandDescriptor.Command} {actionName}");

                    foreach (var arg in item.Arguments.Where(x => !x.IsOptional))
                    {
                        Console.Write($" --{arg.Name}");
                        if (!arg.IsBooleanSwitch)
                        {
                            Console.Write(" <value>");
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                }
            }

            if (commandHasArgs)
            {
                Console.WriteLine();
                Console.WriteLine("Arguments: ");

                var arguments = commandDescriptor.Actions.Values.SelectMany(x => x.Arguments).ToList();

                if (commandDescriptor.HasDefaultAction)
                    arguments.AddRange(commandDescriptor.DefaultAction.Arguments);

                arguments = arguments.Distinct(
                        new GeneralEqualityComparerer<ArgumentDescriptor>(
                            (x, y) => x.Name.Equals(y.Name),
                            (x) => x.Name.GetHashCode()
                        )
                    )
                    .ToList();

                var argumentText = new StringBuilder(50);


                foreach (var argumentDescriptor in arguments)
                {
                    argumentText.Clear();

                    if (!string.IsNullOrEmpty(argumentDescriptor.ShortName))
                    {
                        argumentText.Append("-");
                        argumentText.Append(argumentDescriptor.ShortName);
                        argumentText.Append(", ");
                    }
                    else
                    {
                        argumentText.Append(' ', GetShortNameArgumentLength());
                    }

                    argumentText.Append("--");
                    argumentText.Append(argumentDescriptor.Name);

                    if (argumentDescriptor.IsBooleanSwitch)
                    {
                        Console.Write($"  {argumentText}".PadRight(30));
                    }
                    else
                    {
                        Console.Write($"  {argumentText} <value>".PadRight(30));

                    }

                    Console.WriteLine(argumentDescriptor.Description.DefaultIfWhitespace(NO_DESCRIPTION_TEXT));
                }
            }
        }

        private int GetShortNameArgumentLength()
        {
            // calculate this on settings.
            return 4;
        }
    }
}