using System;
using Log;

namespace Start
{
    public class Base
    {
        public static void Main(string[] args)
        {
            // Call the App class that will start the program

            // TODO Get run/pause command arguments from the client
            Console.WriteLine("Starting main application...");
            //Console.WriteLine(args[0]);
            App.Start(bool.Parse("true"));
            //App.TestMethod();
            
        }
    }
}