using System;

namespace Utils
{
    
    public class Processor
    {
        // Have global dictionary for the dictionary
        private Dictionary<string, Dictionary<string, List<string>>> scheduleTemplate =
           new Dictionary<string, Dictionary<string, List<string>>>();
    
        public static void CreateDaySchedule()
        {
            // Call create dictionary method

            // call add to dictionary method
        }

        private Dictionary<string, Dictionary<string, List<string>>> CreateDayDictionary()
        {
            // get the current day of the week
            string currentDay = DateTime.Now.ToString();

            string[] timeSlots = GenerateTimeSlots();

        }

        private void AddEntryToDictionary()
        {
            //TODO
        }

        private string[] GenerateTimeSlots()
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