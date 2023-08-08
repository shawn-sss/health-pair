using System;
using System.Globalization;

namespace HealthPairAPI.Helpers
{
    // Custom exception class for throwing application specific exceptions (e.g. for validation)
    // that can be caught and handled within the application
    public class HealthPairAppException : Exception
    {
        public HealthPairAppException() : base() {}

        public HealthPairAppException(string message) : base(message) { }

        public HealthPairAppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}