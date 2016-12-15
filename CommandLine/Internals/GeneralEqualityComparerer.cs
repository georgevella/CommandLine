using System;
using System.Collections.Generic;

namespace CommandLine.Internals
{
    public class GeneralEqualityComparerer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _equalityMethod;
        private readonly Func<T, int> _hashFunc;

        public GeneralEqualityComparerer(Func<T, T, bool> equalityMethod, Func<T, int> hashFunc = null)
        {
            _equalityMethod = equalityMethod;
            _hashFunc = hashFunc ?? (arg => arg.GetHashCode());
        }

        public bool Equals(T x, T y)
        {
            return _equalityMethod(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _hashFunc(obj);
        }
    }
}