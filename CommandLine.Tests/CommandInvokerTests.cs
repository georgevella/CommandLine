using System;
using System.Linq;
using CommandLine.Factories;
using CommandLine.Model;
using CommandLine.Tests.Commands;
using FluentAssertions;
using Xunit;

namespace CommandLine.Tests
{
    public class CommandInvokerTests
    {
        [Fact]
        public void ArgumentsValid_CommandInvoked()
        {
            const string expectedFilePath = "C:\\file.txt";

            var descriptor = CommandDescriptorFactory.CreateFor<RunCommand>();

            var commandInstance = new RunCommand();

            var args = new Arguments(
                new[]
                {
                    new ParsedArgument(0, descriptor.DefaultAction.Arguments.First(), expectedFilePath),
                });

            var invoker = new CommandInvoker(descriptor, descriptor.DefaultAction, args, commandInstance);
            invoker.Run();

            commandInstance.Path.Should().Be(expectedFilePath);
        }

        [Fact]
        public void NoArgumentsProvided_ExceptionThrown()
        {
            var descriptor = CommandDescriptorFactory.CreateFor<RunCommand>();

            var commandInstance = new RunCommand();

            var invoker = new CommandInvoker(descriptor, descriptor.DefaultAction, new Arguments(), commandInstance);

            System.Action a = () => invoker.Run();
            a.ShouldThrow<InvalidOperationException>();
        }
    }
}

// test multiple actions without default action
// test action invocation without switches
// test handling of arguments without switches but with action