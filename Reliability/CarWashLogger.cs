
using System;

namespace Reliability
{
    public class CarWashLogger
    {
        public void LogInformation(string message, string parameterName, object value)
        {
            Console.WriteLine(
                "[INFORMATION] " + message.Replace("{" + parameterName + "}", Convert.ToString(value))
            );
        }

        public void LogWarning(string message, string parameterName, object value)
        {
            Console.WriteLine(
                "[WARNING] " + message.Replace("{" + parameterName + "}", Convert.ToString(value))
            );
        }

        public void LogError(string message, string parameterName, object value)
        {
            Console.WriteLine(
                "[ERROR] " + message.Replace("{" + parameterName + "}", Convert.ToString(value))
            );
        }
    }
}
