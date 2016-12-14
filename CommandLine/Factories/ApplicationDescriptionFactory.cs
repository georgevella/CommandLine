using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLine.Attributes;
using CommandLine.Extensions;
using CommandLine.Model;

namespace CommandLine.Factories
{
    public static class ApplicationDescriptionFactory
    {
        public static ApplicationDescription BuildFromAllCommandsInAssembly(IEnumerable<CommandDescriptor> commandDescriptors)
        {
            return new ApplicationDescription(commandDescriptors);
        }

        public static ApplicationDescription BuildFromType<T>() where T : class
        {
            return new ApplicationDescription(new[] { CommandDescriptorFactory.CreateFor<T>() });
        }
    }
}