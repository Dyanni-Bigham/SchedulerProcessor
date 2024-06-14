using System;
using Utils;

namespace Start
{
    public class App
    {
        public static bool isRunning;
        public static string filePath = "./config_v2.json"; //TODO: change this to a dynamic value
        public static bool haveSchedule = false;
        public static Dictionary<string, Dictionary<string, List<string>>> schedule;

        public static void Start()
        {
            // Read the config file (maybe make this a class that can do that) (FileReader class)

            // Extract the day, time, and application (file path) ignore the timeUnit field for now (FileReader class)

            // have a flag called IsRunning this will be triggered by the context memu in the UI

                // This will be a while loop that will constantly run the scheduling logic

                // Create a class that will handle the processing (Processor class)

            // Create an exeception class (Error)
            Console.WriteLine("Application is starting");
            isRunning = true; // this will be an argument passed from the client

            DateTime currentTime;
            string timeToSearch;

            while (isRunning)
            {
                if (!haveSchedule)
                {
                    LoadSchedule();
                }

                // Get the current time
                currentTime = TimeHelper.GetCurrentTime();

                // call convert to 24 hours method
                timeToSearch = TimeHelper.Conver12HoursTo24Hours(currentTime);
                Console.WriteLine(timeToSearch);

                // pass the time into the execute file method
                Processor.RunSchedule(timeToSearch);

                // sleep until next interval
                TimeHelper.SleepUntilNextInterval();
            }
        }

        private static void LoadSchedule()
        {
            try
            {
                Console.WriteLine("Loading schedule from config file...");
                FileReader.SetFilePath(filePath);
                FileReader.ReadFile();

                Processor.SetEntries(FileReader.GetEntries());
                Processor.CreateDaySchedule();
                schedule = Processor.GetSchedule();
                haveSchedule = true;
                Console.WriteLine("Schedule loaded successfully...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading schedule: {ex.Message}");
                isRunning = false; // will stop the loop
            }
        }
    }
}
