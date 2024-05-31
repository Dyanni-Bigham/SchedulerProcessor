using System;

namespace Objects
{
    public class Entry
    {
        public List<string>? days
        {get; set;}

        public List<string>? apps
        {get; set;}

         public string? interval
         {get; set;}

         public Entry(List<string> days, List<string> apps, string interval)
         {
            this.days = days;
            this.apps = apps;
            this.interval = interval;
         }

        public override string ToString()
        {
            string daysString = days != null ? string.Join(", ", days) : "None";
            string appsString = apps != null ? string.Join(", ", apps) : "None";
            return $"Days: {daysString}, Apps: {appsString}, Interval: {interval}";
        }

    }
}