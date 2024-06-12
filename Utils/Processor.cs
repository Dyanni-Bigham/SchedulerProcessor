using System;
using Objects;

namespace Utils
{

    public class Processor
    {
        // Have global dictionary for the dictionary
        private static Dictionary<string, Dictionary<string, List<string>>> _scheduleTemplate =
           new Dictionary<string, Dictionary<string, List<string>>>();
        private static List<Entry> _entries;

        private static string _currentDay = DateTime.Now.DayOfWeek.ToString();

        /*
        public Processor(List<Entry> entries)
        {
            this.entries = entries;
            currentDay = DateTime.Now.DayOfWeek.ToString();
        }
        */

        public static List<Entry> GetEntries()
        {
            return _entries;
        }

        public static void SetEntries(List<Entry> entries)
        {
            _entries = entries;
        }
        
        public static void CreateDaySchedule()
        {
            // Call create dictionary method
            Dictionary<string, Dictionary<string, List<string>>> test = CreateDayDictionary();

            // call add to dictionary method
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
            // Assuming the entries will share the same schedule
            for (int i = 0; i < entries.Count; i++)
            {
                // check that entry day is the current day
                if (entries[i].days.Contains(_currentDay))
                {
                    // check that current day is in the schedule
                    if(_scheduleTemplate.ContainsKey(_currentDay))
                    {
                        // gets all of the time slots for the current day
                        var timeSlots = _scheduleTemplate[_currentDay];

                        // find the time slot in the time slots dictionary
                        if(timeSlots.ContainsKey(entries[i].interval))
                        {
                           // add the appplication to the schedule
                           _scheduleTemplate[_currentDay][entries[i].interval].AddRange(entries[i].apps);
                           //Console.WriteLine("Adding applications");
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

            // return time slots
            return timeSlots.ToArray();
        }
    }
}
