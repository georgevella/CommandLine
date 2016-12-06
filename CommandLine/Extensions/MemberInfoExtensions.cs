using System;
using System.Linq;
using System.Reflection;

namespace CommandLine.Extensions
{
    public static class MemberInfoExtensions
    {
        public static bool HasCustomAttribute<TAttribute>(this ICustomAttributeProvider memberinfo) where TAttribute : Attribute
        {
            var attributes = memberinfo.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>().ToArray();

            return attributes.Length > 0;
        }
    }
}