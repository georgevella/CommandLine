using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using CommandLine.Extensions;
using FluentAssertions;
using Xunit;

namespace CommandLine.Tests
{
    public class TypeExtensionsTest
    {
        [Theory]
        [InlineData(typeof(IEnumerable<string>), true)]
        [InlineData(typeof(IEnumerable<int>), true)]
        [InlineData(typeof(ICollection<string>), true)]
        [InlineData(typeof(IList<Guid>), true)]
        [InlineData(typeof(List<int>), true)]
        [InlineData(typeof(Collection<string>), true)]
        [InlineData(typeof(Array), true)]
        [InlineData(typeof(int[]), true)]
        [InlineData(typeof(string[]), true)]
        [InlineData(typeof(FileInfo[]), true)]
        [InlineData(typeof(ArrayList), true)]
        [InlineData(typeof(IEnumerable), true)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(FileInfo), false)]

        public void EnumerableOrCollectionTypeDetection(Type type, bool expectedResult)
        {
            type.IsEnumerableOrCollection().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<string>), typeof(string))]
        [InlineData(typeof(IEnumerable<int>), typeof(int))]
        [InlineData(typeof(ICollection<string>), typeof(string))]
        [InlineData(typeof(IList<Guid>), typeof(Guid))]
        [InlineData(typeof(List<int>), typeof(int))]
        [InlineData(typeof(Collection<string>), typeof(string))]
        [InlineData(typeof(Array), typeof(object))]
        [InlineData(typeof(int[]), typeof(int))]
        [InlineData(typeof(string[]), typeof(string))]
        [InlineData(typeof(ArrayList), typeof(object))]
        [InlineData(typeof(IEnumerable), typeof(object))]
        [InlineData(typeof(TestCollection), typeof(string))]
        [InlineData(typeof(string), typeof(char))]
        [InlineData(typeof(int), null)]
        [InlineData(typeof(FileInfo), null)]

        public void EnumerableOrCollectionItemTypeDetection(Type type, Type expectedItemType)
        {
            var result = type.GetEnumerableOrCollectionItemType();
            if (expectedItemType == null)
                result.Should().BeNull();
            else
                result.Should().Be(expectedItemType);
        }

        class TestCollection : Collection<string>
        {

        }
    }
}