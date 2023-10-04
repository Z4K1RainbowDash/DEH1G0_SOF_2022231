using Grpc.Net.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests.BackendTests.TestHelpers
{
    public static class AttributeHelper
    {
        public static bool MethodHasAttributeOfType<TClass, TAttribute>(string methodName)
            where TClass : class
            where TAttribute : Attribute
        {
            TAttribute? attribute = GetMethodAttribute<TClass, TAttribute>(methodName);

            return attribute != null;
        }

        public static bool MethodHasAttributeWithPropertyValue<TClass, TAttribute, TProperty>(string methodName, string propertyName, TProperty expectedValue)
            where TClass : class
            where TAttribute : Attribute
            where TProperty : class
        {
            TAttribute? attribute = GetMethodAttribute<TClass, TAttribute>(methodName);

            if (attribute == null)
            {
                return false;
            }

            PropertyInfo? propertyInfo = attribute.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
            {
                return false;
            }

            TProperty? attributePropertyValue = (TProperty?)propertyInfo.GetValue(attribute, null) ?? default;

            return attributePropertyValue?.Equals(expectedValue) ?? false;
        }


        private static TAttribute? GetMethodAttribute<TClass, TAttribute>(string methodName)
            where TClass : class
            where TAttribute : Attribute
        {
            var type = typeof(TClass);
            MethodInfo? method = type.GetMethod(methodName);

            if (method == null)
            {
                throw new ArgumentException($"Method '{methodName}' not found in type '{type.Name}'.");
            }

            return method.GetCustomAttribute<TAttribute>();
        }


    }
}
