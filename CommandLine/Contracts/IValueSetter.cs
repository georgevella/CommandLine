using System.Collections.Generic;

namespace CommandLine.Contracts
{
    public interface IValueSetter
    {
        void SetValue(List<object> methodArgumentsCollection, object value);
    }
}