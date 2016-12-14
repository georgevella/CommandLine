using System;
using System.Collections.Generic;
using System.Reflection;

namespace CommandLine.Tests.Utils
{
    public interface IParserTestData
    {
        Type GetCommandType();
        MethodInfo GetActionMethod();
        Dictionary<string, string> ExpectedArguments { get; }
    }
}