using System;
using System.Collections.Generic;

namespace CommonLibrary
{
    public class Guard
    {
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);
        }

        public static void ArgumentNotNullOrEmpty<T>(ICollection<T> argumentValue, string argumentName)
        {
            if (argumentValue == null || argumentValue.Count == 0)
                throw new ArgumentNullException(argumentName);
        }

        //public static void ArgumentNotNullOrEmpty(Array argumentValue, string argumentName)
        //{
        //    if (argumentValue == null || argumentValue.Length == 0)
        //        throw new ArgumentNullException(argumentName);
        //}

        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentNullException(argumentName);
        }

        public static void ArgumentNotDefault<T>(T argumentValue, string argumentName)
            where T : IEquatable<T>
        {
            if (argumentValue.Equals(default(T)))
                throw new ArgumentException("Default value", argumentName);
        }

        public static void ObjectNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new InvalidOperationException(argumentName);
        }
    }
}