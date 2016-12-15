using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine.Factories;
using CommandLine.Model;

namespace CommandLine.Parser
{
    internal class Parser
    {
        public ApplicationDescription ApplicationDescription { get; }
        public ParserConfiguration Configuration { get; }

        public Parser(ApplicationDescription applicationDescription, ParserConfiguration configuration)
        {
            ApplicationDescription = applicationDescription;
            Configuration = configuration;
        }

        public ParserResult Parse(string[] args)
        {
            if (args.Length == 0)
            {
                return new ParserResult(ParserResultType.ShowHelpText);
            }

            // determine command and parsing requirements
            var command = args[0];

            if (!ApplicationDescription.IsKnownCommand(command))
            {
                return new ParserResult(ParserResultType.UnknownCommand);
            }

            var commandDescriptor = ApplicationDescription.GetCommand(command);
            ActionDescriptor actionDescriptor;

            // start parsing arguments and build arg list
            var parsedArguments = new List<ParsedArgument>();

            var position = 0;
            var startIndex = 2;

            if (args.Length > 1 && !IsSwitch(args[1]))
            {
                // second argument is not a switch, determine if its ana action
                var possibleActionName = args[1];

                if (commandDescriptor.Actions.ContainsKey(possibleActionName))
                {
                    // second argument is an action identifier
                    actionDescriptor = commandDescriptor.Actions[possibleActionName];
                }
                else
                {
                    if (!commandDescriptor.HasDefaultAction)
                    {
                        // user supplied a second parameter that does not match an action,
                        // command does not specify a default action
                        return new ParserResult(ParserResultType.ShowHelpText, commandDescriptor);
                    }

                    actionDescriptor = commandDescriptor.DefaultAction;

                    // we're going to run the default action, add second argument to the parsed arguments list
                    // TODO identify the argument descriptor of this value
                    throw new NotSupportedException("Arguments without switches are not supported, yet");
                }

                //if (!actionDescriptor.SwitchesAreOptional)
                //{
                //    // we're specifying a command and an action, but action requires switches
                //    return new ParserResult(ParserResultType.ShowHelpText, commandDescriptor);
                //}
            }
            else
            {
                if (!commandDescriptor.HasDefaultAction)
                {
                    return new ParserResult(ParserResultType.ShowHelpText, commandDescriptor);
                }

                actionDescriptor = commandDescriptor.DefaultAction;
                startIndex = 1;
            }

            for (var i = startIndex; i < args.Length; i++)
            {
                var arg = args[i];

                if (IsSwitch(arg))
                {
                    var equalSymbolPos = 0;

                    if (arg.Contains(':') || arg.Contains('='))
                    {
                        for (; equalSymbolPos < arg.Length; equalSymbolPos++)
                        {
                            var breakLoop = arg[equalSymbolPos] == ':' || arg[equalSymbolPos] == '=';
                            if (breakLoop)
                                break;
                        }
                    }

                    bool isShortName;

                    string name;
                    string value = null;
                    ArgumentDescriptor argumentDescriptor = null;

                    if (equalSymbolPos > 0)
                    {
                        // having a check for equalSymbolPos>0 is fine, because we are handling a switch
                        name = GetActualArgumentName(arg.Substring(0, equalSymbolPos), out isShortName);
                        value = arg.Substring(equalSymbolPos + 1);
                    }
                    else
                    {
                        // value is not specified within single argument but spread onto the next
                        name = GetActualArgumentName(arg, out isShortName);
                    }

                    argumentDescriptor = actionDescriptor.Arguments.FirstOrDefault(x => x.Name == name);
                    if (argumentDescriptor == null)
                        return new ParserResult(ParserResultType.UnknownArgument, commandDescriptor);


                    if (value == null)
                    {
                        // value is not specified within single argument but spread onto the next
                        if (((i + 1) >= args.Length) || IsSwitch(args[i + 1]))
                        {
                            // we reached the end of the raw argument list
                            // or next argument is a switch
                            // verify if named argument can live without a value
                            if (!argumentDescriptor.IsBooleanSwitch)
                            {
                                return new ParserResult(ParserResultType.MissingArgumentValue,
                                    commandDescriptor,
                                    actionDescriptor,
                                    argumentDescriptor);
                            }

                            // arg is boolean, set value to true (because user did not supply one)
                            value = bool.TrueString;
                        }
                        else
                        {
                            // read value and increment for-loop counter
                            value = args[++i];
                        }
                    }

                    // check if argument has allowed values list
                    if (value != null)
                    {
                        if (!argumentDescriptor.Validator.IsValid(value))
                        {
                            return new ParserResult(ParserResultType.ArgumentValueInvalid,
                                commandDescriptor,
                                actionDescriptor,
                                argumentDescriptor);
                        }
                    }

                    parsedArguments.Add(new ParsedArgument(
                        position++,
                        argumentDescriptor,
                        value
                    ));
                }
                else
                {
                    throw new NotSupportedException("Arguments without switches are not supported, yet");
                }
            }

            return new ParserResult(ParserResultType.ExecuteCommand,
                commandDescriptor,
                actionDescriptor,
                arguments: new Arguments(parsedArguments)
                );
        }

        //private bool IsHelpRequestingArgument(string arg)
        //{
        //    var actualArgumentName = GetActualArgumentName(arg);

        //    if (actualArgumentName == null)
        //        return false;

        //    if (actualArgumentName.Equals("help") || actualArgumentName.Equals("h")) ;

        //}

        private bool IsSwitch(string arg)
        {
            return arg.StartsWith(Configuration.NameSwitchTag) || arg.StartsWith(Configuration.ShortNameSwitchTag);
        }

        private string GetActualArgumentName(string arg, out bool isShortName)
        {
            if (arg.StartsWith(Configuration.NameSwitchTag))
            {
                isShortName = false;
                return arg.Substring(Configuration.NameSwitchTag.Length);
            }

            if (arg.StartsWith(Configuration.ShortNameSwitchTag))
            {
                isShortName = true;
                return arg.Substring(Configuration.ShortNameSwitchTag.Length);
            }

            isShortName = false;
            return null;
        }
    }
}