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
                    InitializeSchedule();
                }

                // Get the current time
                currentTime = TimeHelper.GetCurrentTime();

                // check if time has a valid interval 00, 15, 30, 45
                if(!TimeHelper.IsValidInterval(currentTime))
                {
                    DateTime adjustedInterval = TimeHelper.AdjustTime(currentTime);
                    Console.WriteLine("Adjusted time. sleeping until correct 15 minute interval\n");
                    TimeHelper.SleepUntilNextInterval(adjustedInterval, currentTime);
                }
                else
                {
                    Console.WriteLine("Correct 15 minute interval. Application starting soon...\n");
                    timeToSearch = TimeHelper.Conver12HoursTo24Hours(currentTime);
                    Processor.RunSchedule(timeToSearch);
                    TimeHelper.SleepUntilNextInterval();
                }
            }
        }

        private static void InitializeSchedule()
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
                Console.WriteLine("Schedule loaded successfully...\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading schedule: {ex.Message}");
                isRunning = false; // will stop the loop
            }
        }

        public static void TestMethod()
        {
            DateTime currentTime = TimeHelper.GetCurrentTime();
            if(!TimeHelper.IsValidInterval(currentTime))
            {
                DateTime adjustedInterval = TimeHelper.AdjustTime(currentTime);
                TimeHelper.SleepUntilNextInterval(adjustedInterval, currentTime);
            }
            else
            {
                Console.WriteLine("Time is a valid interal");
            }
        }
    }
}
