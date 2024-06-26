using System;
using System.Diagnostics;
using Log;
using Objects;

namespace Utils
{

    public class Processor
    {
        private static Dictionary<string, Dictionary<string, List<string>>> _scheduleTemplate =
           new Dictionary<string, Dictionary<string, List<string>>>();
        private static List<Entry> _entries;

        private static string _currentDay = DateTime.Now.DayOfWeek.ToString();

        public static List<Entry> GetEntries()
        {
            return _entries;
        }

        public static void SetEntries(List<Entry> entries)
        {
           //Console.WriteLine("Processor is now picking up entries");
           Logger.Log("Processor is now picking up entries");
            _entries = entries;
        }

        public static Dictionary<string, Dictionary<string, List<string>>> GetSchedule()
        {
            if (_scheduleTemplate == null)
            {
                //Console.WriteLine("Schedule was not created successfully");
                Logger.Log("Schedule was not created successfully");
                 return new Dictionary<string, Dictionary<string, List<string>>>();
            }

           // Console.WriteLine("Returning the schedule for processing");
            Logger.Log("Returning the schedule for processing");
            return _scheduleTemplate;
        }
        
        public static void CreateDaySchedule()
        {
            Dictionary<string, Dictionary<string, List<string>>> test = CreateDayDictionary();

            AddEntryToDictionary(_entries);
        }

        private static Dictionary<string, Dictionary<string, List<string>>> CreateDayDictionary()
        {

            string[] timeSlots = GenerateTimeSlots();

            _scheduleTemplate[_currentDay] = new Dictionary<string, List<string>>();

            foreach (string slot in timeSlots)
            {
                _scheduleTemplate[_currentDay][slot] = new List<string>();
            }
            //Console.WriteLine("Schedule has been created with the entries");
            Logger.Log("Schedule has been created with the entries");

            return _scheduleTemplate;

        }

        public static void PrintSchedule()
        {
            foreach (var dayEntry in _scheduleTemplate)
            {
                //Console.WriteLine(dayEntry.Key);
                Logger.Log(dayEntry.Key);
                foreach (var periodKey in dayEntry.Value)
                {
                    //Console.WriteLine($"  {periodKey.Key}");
                    Logger.Log($"  {periodKey.Key}");
                    foreach (var task in periodKey.Value)
                    {
                        //Console.WriteLine($"    {task}");
                        Logger.Log($"    {task}");
                    }
                }
            }
        }

        private static void AddEntryToDictionary(List<Entry> entries)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].days.Contains(_currentDay))
                {
                    if(_scheduleTemplate.ContainsKey(_currentDay))
                    {
                        var timeSlots = _scheduleTemplate[_currentDay];

                        if(timeSlots.ContainsKey(entries[i].interval))
                        {
                           _scheduleTemplate[_currentDay][entries[i].interval].AddRange(entries[i].apps);
                        }
                    }
                }
            }

        }

        private static string[] GenerateTimeSlots()
        {
            List<string> timeSlots = new List<string>();

            for (int hour = 0; hour < 24; hour++)
            {
                for (int minute = 0; minute < 60; minute += 15)
                {
                    timeSlots.Add(string.Format("{0:D2}:{1:D2}", hour, minute));
                }
            }

            return timeSlots.ToArray();
        }

        private static void ExecuteApp(List<string> appName)
        {
            try
            {
                //Console.WriteLine($"Executing the application {appName[0]}");
                Logger.Log($"Executing the application {appName[0]}");
                Process.Start(appName[0]);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Application doesn't exist for interval");
                Logger.Log("Application doesn't exist for interval");
            }
        }

        public static void RunSchedule(string time)
        {
            // search the nested dictionary for the time
            var listOfTimes = _scheduleTemplate[_currentDay];

            // search for time in list of times
            if (listOfTimes.ContainsKey(time))
            {
                // execute the app
                //Console.WriteLine("Executing the application");
                Logger.Log("Executing the application");
                ExecuteApp(listOfTimes[time]);
            }

        }
    }

}
