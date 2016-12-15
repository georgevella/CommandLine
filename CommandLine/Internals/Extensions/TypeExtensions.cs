using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLine.Internals.Extensions
{
    public static class TypeExtensions
    {
        private static readonly Type GenericEnumerableType = typeof(IEnumerable<>);
        private static readonly Type EnumerableType = typeof(IEnumerable);
        private static readonly Type BooleanType = typeof(bool);
        private static readonly Type CharType = typeof(char);
        private static readonly Type StringType = typeof(string);
        private static readonly Type ObjectType = typeof(object);
        private static readonly Type GenericCollectionType = typeof(ICollection<>);
        private static readonly Type ArrayType = typeof(Array);
        private static readonly Type ArrayListType = typeof(ArrayList);

        private static readonly List<Type> NonTypedCollections = new List<Type>()
        {
            ArrayType,
            ArrayListType,
            EnumerableType
        };

        public static bool IsEnumerableOrCollection(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            // strings inherit from IEnumerable, but shouldn't be treated as a collection here.
            if (StringType == type)
                return false;

            if (NonTypedCollections.Contains(type))
                return true;

            if (type.IsArray)
                return true;

            var interfaces = type.GetInterfaces();
            return interfaces.Contains(EnumerableType) ||
                   interfaces.Contains(GenericEnumerableType);
        }

        public static Type GetEnumerableOrCollectionItemType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (StringType == type)
                return CharType;

            if (NonTypedCollections.Contains(type))
                return ObjectType;

            if (type.IsArray)
                return type.GetElementType();

            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();
                if (genericType == GenericEnumerableType || genericType == GenericCollectionType)
                    return type.GetTypeInfo().GenericTypeArguments[0];
            }

            var implementedCollectionContracts =
                type.GetInterfaces()
                    .Where(intf => intf.IsGenericType)
                    .Select(intf => new
                    {
                        GenericInterface = intf.GetGenericTypeDefinition(),
                        Interface = intf
                    })
                    .FirstOrDefault(
                        x =>
                            x.GenericInterface == GenericCollectionType ||
                            x.GenericInterface == GenericEnumerableType);

            return implementedCollectionContracts?.Interface.GetTypeInfo().GenericTypeArguments[0];
        }

        public static bool IsBoolean(this Type type)
        {
            return type == BooleanType;
        }

        public static bool IsConstructable(this Type type)
        {
            return !type.IsInterface && !type.IsAbstract && type.HasDefaultConstructor();
        }

        public static bool HasDefaultConstructor(this Type type)
        {
            return !type.IsInterface && type.GetConstructors().Any(x => x.GetParameters().Length == 0);
        }
    }
}