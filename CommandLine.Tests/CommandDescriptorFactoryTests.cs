using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CommandLine.Factories;
using CommandLine.Tests.Commands;
using FluentAssertions;
using Xunit;

namespace CommandLine.Tests
{

    public class CommandDescriptorFactoryTests
    {
        [Fact]
        public void CommandWithSingleActionNoParameters_DefaultActionSetToAction()
        {
            Expression<Action<VersionCommand>> expectedActionMethodExpression = x => x.Main();
            var expectedActionMethod = ((MethodCallExpression)expectedActionMethodExpression.Body).Method;


            var descriptor = CommandDescriptorFactory.CreateFor<VersionCommand>();

            descriptor.Command.Should().Be("version");
            descriptor.Actions.Should().HaveCount(0);
            descriptor.DefaultAction.Should().NotBeNull();
            descriptor.DefaultAction.MethodInfo.Should().Match(x => expectedActionMethod.Equals(x));
        }

        [Fact]
        public void CommandWithNoActions_ThrowsException()
        {

            System.Action a = () => CommandDescriptorFactory.CreateFor<CommandWithNoActions>();
            a.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void CommandsWithoutAttribute_ThrowException()
        {
            System.Action a = () => CommandDescriptorFactory.CreateFor<CommandWithoutAttribute>();
            a.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void CommandWithSingleAction_DefaultActionSetToAction()
        {
            Expression<Action<RunCommand>> expectedActionMethodExpression = x => x.Program(default(string));
            var expectedActionMethod = ((MethodCallExpression)expectedActionMethodExpression.Body).Method;
            var parameters = expectedActionMethod.GetParameters();
            var expectedArgumentNames = parameters.Select(x => x.Name.ToLower()).ToArray();

            var descriptor = CommandDescriptorFactory.CreateFor<RunCommand>();

            descriptor.Command.Should().Be("run");
            descriptor.Actions.Should().HaveCount(0);
            descriptor.DefaultAction.Should().NotBeNull();
            descriptor.DefaultAction.MethodInfo.Should().Match(x => expectedActionMethod.Equals(x));
            descriptor.DefaultAction.Arguments.Should().HaveCount(parameters.Length);
            descriptor.DefaultAction.Arguments.Select(x => x.Name).Should().Contain(expectedArgumentNames);
        }

        [Fact]
        public void CommandWithMultipleActions_DefaultActionSetToAction()
        {
            Expression<Action<ConfigCommand>> expectedActionMethodExpression = x => x.Set(default(string), default(string), default(Commands.ValueType));
            Expression<Action<ConfigCommand>> expectedDefaultActionMethodExpression = x => x.Get(default(string));

            var expectedActionMethod = ((MethodCallExpression)expectedActionMethodExpression.Body).Method;
            var expectedDefaultMethod = ((MethodCallExpression)expectedDefaultActionMethodExpression.Body).Method;

            var defaultActionParameters = expectedDefaultMethod.GetParameters();
            var expectedDefaultActionArguments = defaultActionParameters.Select(x => x.Name.ToLower()).ToArray();

            var descriptor = CommandDescriptorFactory.CreateFor<ConfigCommand>();

            descriptor.Command.Should().Be("config");

            descriptor.DefaultAction.Should().NotBeNull();
            descriptor.DefaultAction.MethodInfo.Should().Match(x => expectedDefaultMethod.Equals(x));
            descriptor.DefaultAction.Arguments.Should().HaveCount(defaultActionParameters.Length);
            descriptor.DefaultAction.Arguments.Select(x => x.Name)
                .Should().Contain(expectedDefaultActionArguments);

            descriptor.Actions.Should().HaveCount(2);
            descriptor.Actions.Select(x => x.Value.MethodInfo).Should().Contain(new[]
            {
                expectedActionMethod, expectedDefaultMethod
            });
        }
    }
}


// test multiple actions without default action
// test parameters defined in the command class
