using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using CommandLine.Factories;
using CommandLine.Model;
using CommandLine.Parser;
using CommandLine.Tests.Commands;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;
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
                        {"path", "C:\\some random path\\something.exe" }
                    }
                    ),
                new[] { "run", "-path", "C:\\some random path\\something.exe"},
                1,
            },
            new object[]
            {
                new ParserExpectedResults<RunCommand>(
                    command => command.Program(default(string)),
                    new Dictionary<string, string>()
                    {
                        {"path", "C:\\some random path\\something.exe" }
                    }
                    ),
                new[] { "run", "-path=C:\\some random path\\something.exe"},
                1,
            },
            new object[]
            {
                new ParserExpectedResults<RunCommand>(
                    command => command.Program(default(string)),
                    new Dictionary<string, string>()
                    {
                        {"path", "C:\\some random path\\something.exe" }
                    }
                    ),
                new[] { "run", "-path:C:\\some random path\\something.exe"},
                1,
            },
            new object[]
            {
                new ParserExpectedResults<ConfigCommand>(
                    command => command.Get(default(string)),
                    new Dictionary<string, string>()
                    {
                        {"name", "something" }
                    }
                    ),
                new[] { "config", "-name", "something"},
                1,
            },
            new object[]
            {
                new ParserExpectedResults<ConfigCommand>(
                    command => command.Set(default(string), default(string), ValueType.String),
                    new Dictionary<string, string>()
                    {
                        {"name", "something" },
                        {"value", "xyz" },
                        {"type", $"{ValueType.String}" }
                    }
                    ),
                new[] { "config", "set", "-name", "something", "-value:xyz", $"-type:{ValueType.String}"},
                3,
            }
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

    public class ParserExpectedResults<T> : IParserTestData
        where T : class
    {
        private readonly Expression<Action<T>> _action;

        public ParserExpectedResults(Expression<Action<T>> action, Dictionary<string, string> expectedArguments)
        {
            _action = action;
            ExpectedArguments = expectedArguments;
        }

        public Dictionary<string, string> ExpectedArguments { get; }

        public Type GetCommandType()
        {
            return typeof(T);
        }

        public MethodInfo GetActionMethod()
        {
            var expectedActionMethod = ((MethodCallExpression)_action.Body).Method;
            return expectedActionMethod;
        }
    }

    public interface IParserTestData
    {
        Type GetCommandType();
        MethodInfo GetActionMethod();
        Dictionary<string, string> ExpectedArguments { get; }
    }
}