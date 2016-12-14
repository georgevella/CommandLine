using System.Collections.Generic;
using System.IO;
using CommandLine.Factories;
using CommandLine.Model;
using CommandLine.Parser;
using CommandLine.Tests.Commands;
using CommandLine.Tests.Utils;
using FluentAssertions;
using Xunit;
using ValueType = CommandLine.Tests.Commands.ValueType;

namespace CommandLine.Tests
{
    public class ParserTests
    {

        public static IEnumerable<object[]> ArgumentsWithDifferentSwitchFormats => new[]
        {
            new object[]
            {
                new ParserExpectedResults<RunCommand>(
                    command => command.Program(default(string)),
                    new Dictionary<string, string>()
                    {
                        {"path", "C:\\some random path\\something.exe"}
                    }
                ),
                new[] {"run", "-path", "C:\\some random path\\something.exe"},
                1,
            },
            new object[]
            {
                new ParserExpectedResults<RunCommand>(
                    command => command.Program(default(string)),
                    new Dictionary<string, string>()
                    {
                        {"path", "C:\\some random path\\something.exe"}
                    }
                ),
                new[] {"run", "-path=C:\\some random path\\something.exe"},
                1,
            },
            new object[]
            {
                new ParserExpectedResults<RunCommand>(
                    command => command.Program(default(string)),
                    new Dictionary<string, string>()
                    {
                        {"path", "C:\\some random path\\something.exe"}
                    }
                ),
                new[] {"run", "-path:C:\\some random path\\something.exe"},
                1,
            },
            new object[]
            {
                new ParserExpectedResults<ConfigCommand>(
                    command => command.Get(default(string)),
                    new Dictionary<string, string>()
                    {
                        {"name", "something"}
                    }
                ),
                new[] {"config", "-name", "something"},
                1,
            },
            new object[]
            {
                new ParserExpectedResults<ConfigCommand>(
                    command => command.Get(default(string)),
                    new Dictionary<string, string>()
                    {
                        {"name", "something"}
                    }
                ),
                new[] {"config", "get", "-name", "something"},
                1,
            },
            new object[]
            {
                new ParserExpectedResults<ConfigCommand>(
                    command => command.Set(default(string), default(string), ValueType.String),
                    new Dictionary<string, string>()
                    {
                        {"name", "something"},
                        {"value", "xyz"},
                        {"type", $"{ValueType.String}"}
                    }
                ),
                new[] {"config", "set", "-name", "something", "-value:xyz", $"-type:{ValueType.String}"},
                3,
            },
            new object[]
            {
                new ParserExpectedResults<AdvancedRunCommand>(
                    command => command.Program(default(FileInfo)),
                    new Dictionary<string, string>()
                    {
                        {"path", "C:\\advancedrun\\something.exe"}
                    }
                ),
                new[] {"run", "-path", "C:\\advancedrun\\something.exe"},
                1,
            },
        };

        [Theory]
        [MemberData(nameof(ArgumentsWithDifferentSwitchFormats))]
        public void ShouldHandleDifferentSwitchFormats(IParserTestData parserTestData, string[] args, int expectedArgumentCount)
        {
            var cmdDescriptor = CommandDescriptorFactory.CreateFor(parserTestData.GetCommandType());
            var appDescriptor = new ApplicationDescription(new[] { cmdDescriptor });
            var parser = new Parser.Parser(appDescriptor, new ParserConfiguration());

            var result = parser.Parse(args);

            result.Result.Should().Be(ParserResultType.ExecuteCommand);
            result.Command.Should().Be(cmdDescriptor);
            result.Action.MethodInfo.Should().BeSameAs(parserTestData.GetActionMethod());
            result.Arguments.Should().HaveCount(expectedArgumentCount);

            foreach (var expectedArg in parserTestData.ExpectedArguments)
            {
                result.Arguments.Should()
                    .Contain(x => x.Descriptor.Name == expectedArg.Key && x.Value == expectedArg.Value);
            }
        }
    }
}