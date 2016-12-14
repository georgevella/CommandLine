using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using CommandLine.Tests.Utils;

namespace CommandLine.Tests
{
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
}