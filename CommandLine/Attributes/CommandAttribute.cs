using System;

namespace CommandLine.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandAttribute : Attribute
    {
        public string Name { get; set; }
    }
}