using System;
using System.ComponentModel;
using System.Reflection;
using CommandLine.Attributes;
using CommandLine.Contracts;
using CommandLine.Internals;
using CommandLine.Model;
using CommandLine.Validators;
using CommandLine.ValueSetters;

namespace CommandLine.Factories
{
    public static class ArgumentDescriptorFactory
    {
        public static ArgumentDescriptor BuildFromAttribute(ParameterInfo param)
        {
            var argumentAttribute = param.GetCustomAttribute<ArgumentAttribute>();
            var defaultValueAttribute = param.GetCustomAttribute<DefaultValueAttribute>();
            var descriptionAttribute = param.GetCustomAttribute<DescriptionAttribute>();

            var argumentName = string.IsNullOrEmpty(argumentAttribute.Name)
                ? param.Name.ToLower()
                : argumentAttribute.Name;

            var argumentType = SupportedPrimitiveTypes.GetActualType(param.ParameterType);
            if (argumentType == null)
                throw new InvalidOperationException();

            var validator = ArgumentValidatorFactory.GetValidatorFromType(argumentType);
            var valueProvider = ArgumentValueProviderFactory.GetValueProviderForType(argumentType);

            var argDescriptor = new ArgumentDescriptor(
                argumentName,
                argumentType,
                new MethodArgumentValueSetter(param),
                validator,
                valueProvider,
                $"{argumentAttribute.ShortName}",
                argumentAttribute.IsOptional,
                defaultValueAttribute?.Value,
                descriptionAttribute?.Description
            );

            return argDescriptor;
        }

        public static ArgumentDescriptor BuildFromStandardConventions(ParameterInfo param)
        {
            var argumentType = SupportedPrimitiveTypes.GetActualType(param.ParameterType);
            if (argumentType == null)
                throw new InvalidOperationException();

            var validator = ArgumentValidatorFactory.GetValidatorFromType(argumentType);
            var valueProvider = ArgumentValueProviderFactory.GetValueProviderForType(argumentType);

            var argDescriptor = new ArgumentDescriptor(
                param.Name.ToLower(),
                argumentType,
                new MethodArgumentValueSetter(param),
                validator,
                valueProvider,
                isOptional: param.IsOptional,
                defaultValue: param.DefaultValue
            );

            return argDescriptor;
        }
    }
}