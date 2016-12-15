using System;
using System.Collections.Generic;
using System.IO;
using CommandLine.Contracts;
using CommandLine.Validators;

namespace CommandLine.Factories
{
    public class ArgumentValidatorFactory
    {
        private static readonly Dictionary<Type, Type> _primitiveTypeToValidatorMapping = new Dictionary<Type, Type>()
        {
            // standard primitives
            { typeof(int), typeof(IntArgumentValidator) },
            { typeof(long), typeof(LongArgumentValidator) },
            { typeof(short), typeof(ShortArgumentValidator) },
            { typeof(uint), typeof(UIntArgumentValidator) },
            { typeof(ulong), typeof(ULongArgumentValidator) },
            { typeof(ushort), typeof(UShortArgumentValidator) },
            { typeof(string), typeof(StringArgumentValidator) },
            { typeof(bool), typeof(BooleanArgumentValidator) },

            // argument with guid value
            { typeof(Guid), typeof(GuidArgumentValidator) },

            // argument with date/time, time, date, values
            { typeof(DateTime), typeof(DateTimeArgumentValidator) },

            // argument with URI value
            { typeof(Uri), typeof(UriArgumentValidator) },

            //// argument with URI or file-path value, that is loaded and managed by the CLI framework
            //{ typeof(Stream), typeof(PathArgumentValidator) },
            //{ typeof(StreamReader), typeof(PathArgumentValidator) },
            //{ typeof(StreamWriter), typeof(PathArgumentValidator) },

            //// arguments with file-path value
            //{ typeof(FileInfo), typeof(FilePathArgumentValidator) },
            //{ typeof(FileStream), typeof(FilePathArgumentValidator) },

            //// arguments with directory-path value
            //{ typeof(DirectoryInfo), typeof(DirectoryPathArgumentValidator) },
        };

        public static IArgumentValidator GetValidatorFromType(Type argumentType)
        {
            if (argumentType.IsEnum)
            {
                return new EnumArgumentValidator(argumentType);
            }

            if (_primitiveTypeToValidatorMapping.ContainsKey(argumentType))
            {
                return (IArgumentValidator)Activator.CreateInstance(_primitiveTypeToValidatorMapping[argumentType]);
            }

            return new StringArgumentValidator();
        }
    }
}