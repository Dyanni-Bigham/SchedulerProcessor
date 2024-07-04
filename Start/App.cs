using System;
using Log;
using Utils;

namespace Start
{
    public class App
    {
        public static bool isRunning;
        public static string filePath = "config_v2.json"; //TODO: change this to a dynamic value
        public static bool haveSchedule = false;
        public static Dictionary<string, Dictionary<string, List<string>>> schedule;

    public static void Start(bool isRunning, CancellationToken token)
    {
        Logger.Log("Application is starting");

        DateTime currentTime;
        string timeToSearch;

        while (isRunning && !token.IsCancellationRequested)
        {
            if (!haveSchedule)
            {
                InitializeSchedule();
            }

            currentTime = TimeHelper.GetCurrentTime();

            if (!TimeHelper.IsValidInterval(currentTime))
            {
                DateTime adjustedInterval = TimeHelper.AdjustTime(currentTime);
                Logger.Log("Adjusted time. sleeping until correct 15 minute interval\n");
                TimeHelper.SleepUntilNextInterval(adjustedInterval, currentTime);
            }
            else
            {
                Logger.Log("Correct 15 minute interval. Application starting soon...\n");
                timeToSearch = TimeHelper.Conver12HoursTo24Hours(currentTime);
                Processor.RunSchedule(timeToSearch);
                TimeHelper.SleepUntilNextInterval();
            }
        }

        Logger.Close();
        System.Environment.Exit(0);
    }

        private static void InitializeSchedule()
        {
            try
            {
                //Console.WriteLine("Loading schedule from config file...");
                Logger.Log("Loading schedule from config file...");
                FileReader.SetFilePath(filePath);
                FileReader.ReadFile();

                Processor.SetEntries(FileReader.GetEntries());
                Processor.CreateDaySchedule();
                schedule = Processor.GetSchedule();
                haveSchedule = true;
                //Console.WriteLine("Schedule loaded successfully...\n");
                Logger.Log("Schedule loaded successfully...\n");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error loading schedule: {ex.Message}");
                Logger.Log($"Error loading schedule: {ex.Message}");
                isRunning = false; // will stop the loop
            }
        }
    }
}
