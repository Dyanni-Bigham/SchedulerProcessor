using System;

namespace Log
{
    public class Logger
    {
        private static readonly object _lock = new object();
        private static StreamWriter _writer;
        
        static Logger()
        {
            string filePath = $"output_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            _writer = new StreamWriter(filePath, true) { AutoFlush = true};
        }

        public static void Log(string message)
        {
            lock(_lock)
            {
                _writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }

        public static void Close()
        {
            lock(_lock)
            {
                _writer.Close();
            }
        }
    }
}