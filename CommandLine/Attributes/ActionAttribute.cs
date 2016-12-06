using System;

namespace CommandLine.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionAttribute : Attribute
    {
        public string Name { get; set; }

        public bool Default { get; set; }
    }
}