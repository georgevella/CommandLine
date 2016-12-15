using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CommandLine.Internals.Extensions;

namespace CommandLine.Internals
{
    public static class SupportedPrimitiveTypes
    {
        private static readonly List<Type> _supportedPrimitiveTypes = new List<Type>()
        {
            // standard primitives
            typeof(int),
            typeof(long),
            typeof(short),
            typeof(uint),
            typeof(ulong),
            typeof(ushort),
            typeof(string),
            typeof(bool),

            // argument with guid value
            typeof(Guid),

            // argument with date/time, time, date, values
            typeof(DateTime),

            // argument with URI value
            typeof(Uri),

            // argument with URI or file-path value, that is loaded and managed by the CLI framework
            typeof(Stream),
            typeof(StreamReader),
            typeof(StreamWriter),

            // arguments with file-path value
            typeof(FileInfo),
            typeof(FileStream),

            // arguments with directory-path value
            typeof(DirectoryInfo),
        };

        internal static bool IsSupportedPrimitiveType(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsAbstract)
                return false;

            var actualType = type;

            if (type.IsEnumerableOrCollection())
            {
                actualType = type.GetEnumerableOrCollectionItemType();
            }

            if (actualType.IsEnum)
                return true;

            if (_supportedPrimitiveTypes.Contains(actualType))
                return true;

            return false;
        }

        internal static Type GetActualType(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsAbstract)
                return null;

            var actualType = type;

            if (actualType.IsEnum)
                return actualType;

            if (type.IsEnumerableOrCollection())
            {
                actualType = type.GetEnumerableOrCollectionItemType();
            }

            if (_supportedPrimitiveTypes.Contains(actualType))
                return actualType;

            return null;
        }
    }
}