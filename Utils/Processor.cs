using System;

namespace Utils
{
    
    public class Processor
    {
        // Have global dictionary for the dictionary
        private static Dictionary<string, Dictionary<string, List<string>>> scheduleTemplate =
           new Dictionary<string, Dictionary<string, List<string>>>();
    
        public static void CreateDaySchedule()
        {
            // Call create dictionary method
            Dictionary<string, Dictionary<string, List<string>>> test = CreateDayDictionary();
            printSchedule();
            

            // call add to dictionary method
        }

        private static Dictionary<string, Dictionary<string, List<string>>> CreateDayDictionary()
        {
            // get the current day of the week
            string currentDay = DateTime.Now.DayOfWeek.ToString();

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

        private static void AddEntryToDictionary()
        {
            //TODO Add Implementation
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