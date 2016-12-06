using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace CommandLine.Model
{
    public class ActionDescriptor
    {
        public ActionDescriptor(
            MethodInfo methodInfo,
            string name,
            IEnumerable<ArgumentDescriptor> arguments,
            bool isDefault,
            bool switchesAreOptional,
            string description
            )
        {
            MethodInfo = methodInfo;
            Name = name;
            IsDefault = isDefault;
            SwitchesAreOptional = switchesAreOptional;
            Description = description;
            Arguments = arguments.ToList();
        }

        public MethodInfo MethodInfo { get; }

        public string Name { get; }

        public bool IsDefault { get; }

        public IList<ArgumentDescriptor> Arguments { get; }

        public bool SwitchesAreOptional { get; }

        public string Description { get; }
    }
}