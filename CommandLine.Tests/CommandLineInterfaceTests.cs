using CommandLine.Tests.Commands;
using FluentAssertions;
using Xunit;

namespace CommandLine.Tests
{
    public class CommandLineInterfaceTests
    {
        [Fact]
        public void ShouldBeAbleToParsePathsIntoFileInfos()
        {
            var cli = new CommandLineInterface();
            cli.Register<AdvancedRunCommand>();


            const string expectedPath = "C:\\advancedrun\\something.exe";
            cli.Run(new[] { "run", "-path", expectedPath });

            cli.Command.Should()
                .NotBeNull()
                .And.BeOfType<AdvancedRunCommand>()
                .Which.Path.ToString()
                .Should().Be(expectedPath);
        }

        [Fact]
        public void InvalidPathsShouldNotBeParsedIntoFileInfo()
        {
            var cli = new CommandLineInterface();
            cli.Register<AdvancedRunCommand>();

            const string expectedPath = "abc";
            cli.Run(new[] { "run", "-path", expectedPath });

            var command = cli.Command.Should()
                .NotBeNull()
                .And.BeOfType<AdvancedRunCommand>().Subject;

            command.Path.ToString()
                .Should().Be(expectedPath);
        }
    }
}