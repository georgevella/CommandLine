using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using CommandLine.Attributes;
using System.Reflection;
using CommandLine.Internals;
using CommandLine.Internals.Extensions;
using CommandLine.Model;
using CommandLine.ValueSetters;

namespace CommandLine.Factories
{
    internal class ActionDescriptorFactory
    {
        public static ActionDescriptor CreateFor(MethodInfo method)
        {
            var actionAttribute = method.GetCustomAttribute<ActionAttribute>();

            var arguments = new List<ArgumentDescriptor>();
            var parameters = method.GetParameters()
                .ToList()
                .OrderBy(x => x.Position);

            foreach (var param in parameters)
            {
                if (SupportedPrimitiveTypes.IsSupportedPrimitiveType(param.ParameterType))
                {
                    var descriptor = param.HasCustomAttribute<ArgumentAttribute>()
                        ? ArgumentDescriptorFactory.BuildFromAttribute(param)
                        : ArgumentDescriptorFactory.BuildFromStandardConventions(param);
                    arguments.Add(descriptor);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            // determine action name
            var name = string.IsNullOrEmpty(actionAttribute.Name)
                ? method.Name.ToLower()
                : actionAttribute.Name.ToLower();

            // get description
            var descriptionAttribute = method.GetCustomAttribute<DescriptionAttribute>();

            return new ActionDescriptor(
                method,
                name,
                arguments,
                actionAttribute.Default,
                parameters.All(x => SupportedPrimitiveTypes.IsSupportedPrimitiveType(x.ParameterType)),
                descriptionAttribute?.Description
            );
        }
    }
}