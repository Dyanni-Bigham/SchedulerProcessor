using System;
using Utils;

namespace Start
{
    class App
    {

        public static void Start()
        {   
            // Read the config file (maybe make this a class that can do that) (FileReader class)

            // Extract the day, time, and application (file path) ignore the timeUnit field for now (FileReader class)

            // have a flag called IsRunning this will be triggered by the context memu in the UI

                // This will be a while loop that will constantly run the scheduling logic

                // Create a class that will handle the processing (Processor class)

            // Create an exeception class (Error)
            //Console.WriteLine("Application is starting");
            string filePath = "./config.json";
            FileReader reader = new FileReader(filePath);
            reader.ReadFile();
            reader.displayEntries();
        }
    }
}