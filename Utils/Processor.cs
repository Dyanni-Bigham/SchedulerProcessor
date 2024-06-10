using System;
using Objects;

namespace Utils
{

    public class Processor
    {
        // Have global dictionary for the dictionary
        private static Dictionary<string, Dictionary<string, List<string>>> scheduleTemplate =
           new Dictionary<string, Dictionary<string, List<string>>>();
        private List<Entry> entries;
        private static string currentDay;

        public Processor(List<Entry> entries)
        {
            this.entries = entries;
            currentDay = DateTime.Now.DayOfWeek.ToString();
        }
        public void CreateDaySchedule()
        {
            // Call create dictionary method
            Dictionary<string, Dictionary<string, List<string>>> test = CreateDayDictionary();

            // call add to dictionary method
            AddEntryToDictionary(entries);
        }

        private static Dictionary<string, Dictionary<string, List<string>>> CreateDayDictionary()
        {

            string[] timeSlots = GenerateTimeSlots();

            scheduleTemplate[currentDay] = new Dictionary<string, List<string>>();

            foreach (string slot in timeSlots)
            {
                scheduleTemplate[currentDay][slot] = new List<string>();
            }

            return scheduleTemplate;

        }

        // test method delete later
        public static void printSchedule()
        {
            foreach (var dayEntry in scheduleTemplate)
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
                if (entries[i].days.Contains(currentDay))
                {
                    // check that current day is in the schedule
                    if(scheduleTemplate.ContainsKey(currentDay))
                    {
                        // gets all of the time slots for the current day
                        var timeSlots = scheduleTemplate[currentDay];

                        // find the time slot in the time slots dictionary
                        if(timeSlots.ContainsKey(entries[i].interval))
                        {
                           Console.WriteLine(entries[i].interval);
                        }
                    }
                }
            }
            // check that entry day is the current day

            // search for entry interval in template

            // assign that interval the entry app string
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
