using System;

namespace Delegate_Logger
{
    public class Logger
    {
        public void Info(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }

        public void Warning(string message)
        {
            Console.WriteLine("[WARNING] " + message);
        }

        public void Error(string message)
        {
            Console.WriteLine("[ERROR] " + message);
        }
    }
}
