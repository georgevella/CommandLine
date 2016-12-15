using System;

namespace CommandLine.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class ArgumentAttribute : Attribute
    {
        public string Name { get; set; }

        public char ShortName { get; set; }

        public bool IsOptional { get; set; }
    }
}