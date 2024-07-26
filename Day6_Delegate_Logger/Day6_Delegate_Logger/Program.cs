using System;

namespace Delegate_Logger
{
    // Define the delegate type
    public delegate void LoggingOperation(string message);

    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the Logger class
            Logger logger = new Logger();

            // Create instances of the delegate for each logging method
            LoggingOperation infoOp = new LoggingOperation(logger.Info);
            LoggingOperation warningOp = new LoggingOperation(logger.Warning);
            LoggingOperation errorOp = new LoggingOperation(logger.Error);

            // Test the Info method
            infoOp("This is an informational message.");

            // Test the Warning method
            warningOp("This is a warning message.");

            // Test the Error method
            errorOp("This is an error message.");

            // Create a LoggingOperation delegate that references an anonymous method
            LoggingOperation alertOp = delegate (string message)
            {
                Console.WriteLine("[ALERT] " + message);
            };

            // Test the anonymous method
            alertOp("This is an alert message.");

            // Alternatively, you can use a lambda expression
            LoggingOperation alertOpLambda = (message) =>
            {
                Console.WriteLine("[ALERT] " + message);
            };

            // Test the lambda expression
            alertOpLambda("This is another alert message.");
        }
    }
}
