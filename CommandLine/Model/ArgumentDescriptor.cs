using System;
using CommandLine.Contracts;

namespace CommandLine.Model
{
    public class ArgumentDescriptor
    {
        public ArgumentDescriptor(
            string name,
            Type type,
            IValueSetter setter,
            string shortName = null,
            bool isOptional = false,
            object defaultValue = null,
            string description = null,
            bool multiValued = false)
        {
            Name = name;
            Type = type;
            Setter = setter;
            ShortName = shortName;
            IsOptional = isOptional;
            DefaultValue = defaultValue;
            Description = description;
            MultiValued = multiValued;
        }

        public string Name { get; }

        public Type Type { get; }

        public IValueSetter Setter { get; }

        public string ShortName { get; }

        public bool IsOptional { get; }

        public bool HasDefaultValue => DefaultValue != null;

        public object DefaultValue { get; }

        public string Description { get; }

        public bool MultiValued { get; }
    }
}