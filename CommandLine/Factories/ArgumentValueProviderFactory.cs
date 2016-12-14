using System;
using System.Collections.Generic;
using System.IO;
using CommandLine.Contracts;
using CommandLine.Validators;
using CommandLine.ValueProvider;

namespace CommandLine.Factories
{
    public static class ArgumentValueProviderFactory
    {
        private static readonly Dictionary<Type, Type> _primitiveTypeToValueProviderMapping = new Dictionary<Type, Type>()
        {
            // standard primitives
            { typeof(int), typeof(IntValueProvider) },
            { typeof(long), typeof(LongValueProvider) },
            { typeof(short), typeof(ShortValueProvider) },
            { typeof(uint), typeof(UIntValueProvider) },
            { typeof(ulong), typeof(ULongValueProvider) },
            { typeof(ushort), typeof(UShortValueProvider) },
            { typeof(string), typeof(StringValueProvider) },
            { typeof(bool), typeof(BooleanValueProvider) },

            // argument with guid value
            { typeof(Guid), typeof(GuidValueProvider) },

            // argument with date/time, time, date, values
            { typeof(DateTime), typeof(DateTimeValueProvider) },

            // argument with URI value
            { typeof(Uri), typeof(UriValueProvider) },

            //// argument with URI or file-path value, that is loaded and managed by the CLI framework
            //{ typeof(Stream), typeof(GenericStreamProvider) },
            //{ typeof(StreamReader), typeof(GenericStreamReaderProvider) },
            //{ typeof(StreamWriter), typeof(GenericStreamWriterProvider) },

            // arguments with file-path value
            { typeof(FileInfo), typeof(FileInfoValueProvider) },
            //{ typeof(FileStream), typeof(FileStreamValueProvider) },

            // arguments with directory-path value
            { typeof(DirectoryInfo), typeof(DirectoryInfoValueProvider) },
        };

        public static IArgumentValueProvider GetValueProviderForType(Type argumentType)
        {
            if (argumentType.IsEnum)
            {
                return new EnumValueProvider(argumentType);
            }

            if (_primitiveTypeToValueProviderMapping.ContainsKey(argumentType))
            {
                return (IArgumentValueProvider)Activator.CreateInstance(_primitiveTypeToValueProviderMapping[argumentType]);
            }

            return new StringValueProvider();
        }
    }
}