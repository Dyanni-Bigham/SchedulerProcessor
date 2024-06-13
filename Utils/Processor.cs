using System;
using System.Diagnostics;
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
            _entries = entries;
        }

        public static Dictionary<string, Dictionary<string, List<string>>> GetSchedule()
        {
            if (_scheduleTemplate == null)
            {
                 return new Dictionary<string, Dictionary<string, List<string>>>();
            }

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

            return _scheduleTemplate;

        }

        public static void PrintSchedule()
        {
            foreach (var dayEntry in _scheduleTemplate)
            {
                Console.WriteLine(dayEntry.Key);
                foreach (var periodKey in dayEntry.Value)
                {
                    Console.WriteLine($"  {periodKey.Key}");
                    foreach (var task in periodKey.Value)
                    {
                        Console.WriteLine($"    {task}");
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
                Process.Start(appName[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing file: " + ex.Message);
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
                ExecuteApp(listOfTimes[time]);
            }

        }
    }

}
