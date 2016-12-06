using System;
using System.Collections.Generic;
using System.Reflection;
using CommandLine.Contracts;

namespace CommandLine.ValueSetters
{
    public class MethodArgumentValueSetter : IValueSetter
    {
        private readonly ParameterInfo _parameterInfo;

        public MethodArgumentValueSetter(ParameterInfo parameterInfo)
        {
            _parameterInfo = parameterInfo;
        }

        public void SetValue(List<object> methodArgumentsCollection, object value)
        {
            if (value.GetType() != _parameterInfo.ParameterType)
            {
                throw new InvalidOperationException();
            }

            methodArgumentsCollection[_parameterInfo.Position] = value;
        }
    }
}