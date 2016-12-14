using System.Linq;
using System.Reflection;
using CommandLine.Attributes;
using CommandLine.Extensions;
using CommandLine.Model;

namespace CommandLine.Factories
{
    public static class ApplicationDescriptionFactory
    {
        public static ApplicationDescription BuildFromAllCommandsInAssembly(Assembly assembly)
        {
            var commandTypes = assembly.GetTypes()
                .Where(t => t.HasCustomAttribute<CommandAttribute>())
                .Select(CommandDescriptorFactory.CreateFor)
                .ToList();

            return new ApplicationDescription(commandTypes);
        }

        public static ApplicationDescription BuildFromType<T>() where T : class
        {
            return new ApplicationDescription(new[] { CommandDescriptorFactory.CreateFor<T>() });
        }
    }
}