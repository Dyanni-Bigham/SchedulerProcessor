namespace Utils
{
    public class TimeHelper
    {
        public static string Conver12HoursTo24Hours(DateTime time)
        {
            Console.WriteLine("Converting time from 12 clock to 24 clock");

            return time.ToString("HH:mm");
        }

        public static DateTime GetCurrentTime()
        {
            Console.WriteLine("Getting current time");
            return DateTime.Now;
        }

        public static bool IsValidInterval(DateTime time)
        {
            //DateTime date = DateTime.Parse(time, System.Globalization.CultureInfo.InvariantCulture);
            int minutes = time.Minute;
            
            return minutes == 0 || minutes == 15 || minutes == 30 || minutes == 45;
        }

        public static DateTime AdjustTime(DateTime time)
        {
            int minutes = time.Minute;
            int nextInterval  = ((minutes / 15) + 1) * 15;
            //int minutAdjustment = nextInterval - minutes;

            if (nextInterval == 60)
            {
                return new DateTime(time.Year, time.Month, time.Day,time.Hour, 0, 0).AddHours(1);
            }
            else
            {
                return new DateTime(time.Year, time.Month, time.Day, time.Hour, nextInterval, 0);

            }
        }

        public static void SleepUntilNextInterval()
        {
            DateTime now = DateTime.Now.Date;

            int nextIntervalMinute = (now.Minute / 15 + 1) * 15 % 60;
            int nextIntervalHour = now.Hour;

            if (nextIntervalMinute == 0)
            {
                nextIntervalHour = (now.Hour + 1) % 24;
            }

            DateTime nextIntervalTime = 
                new DateTime(now.Year, now.Month, now.Day, nextIntervalHour, nextIntervalMinute, 0);

            if (nextIntervalTime <= now)
            {
                nextIntervalTime = nextIntervalTime.AddHours(1);
                nextIntervalTime = nextIntervalTime.AddMinutes(-nextIntervalTime.Minute);
            }

            TimeSpan sleepDuration = nextIntervalTime - now;
            
            Console.WriteLine(sleepDuration);
            Console.WriteLine("Sleeping for 15 minutes");
            Thread.Sleep(sleepDuration);
        }
    }
}

/*
DateTime d = new DateTime(1, 1, 1, 23, 12, 0);
var res = d.ToString("hh:mm tt"); // this shows 11:12 PM
var res2 = d.ToString("HH:mm");   // this shows 23:12
*/