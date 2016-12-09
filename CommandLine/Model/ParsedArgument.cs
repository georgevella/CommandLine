using System;
using CommandLine.Parser;

namespace CommandLine.Model
{
    public sealed class ParsedArgument : IEquatable<string>, IEquatable<ParsedArgument>
    {
        public ParsedArgument(int position, ArgumentDescriptor descriptor, string value)
        {
            Position = position;
            Descriptor = descriptor;
            Value = value;
        }

        public int Position { get; }
        public ArgumentDescriptor Descriptor { get; }

        public string Value { get; }

        public bool Equals(ParsedArgument other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return string.Equals(Descriptor.Name, other.Descriptor.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ParsedArgument)obj);
        }

        public override int GetHashCode()
        {
            return Descriptor.Name?.GetHashCode() ?? 0;
        }

        public bool Equals(string other)
        {
            return Descriptor.Name.Equals(other);
        }
    }
}