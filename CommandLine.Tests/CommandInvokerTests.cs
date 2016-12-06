using System;
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

            var invoker = new CommandInvoker(descriptor, new Arguments()
            {
                {"path", expectedFilePath}
            });

            invoker.Run(commandInstance);

            commandInstance.Path.Should().Be(expectedFilePath);
        }

        [Fact]
        public void NoArgumentsProvided_ExceptionThrown()
        {
            var descriptor = CommandDescriptorFactory.CreateFor<RunCommand>();

            var commandInstance = new RunCommand();

            var invoker = new CommandInvoker(descriptor, new Arguments());

            Action a = () => invoker.Run(commandInstance);
            a.ShouldThrow<InvalidOperationException>();
        }
    }
}

// test multiple actions without default action
// test action invocation without switches
// test handling of arguments without switches but with action