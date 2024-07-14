using Log;

namespace Utils
{
    public class TimeHelper
    {
        public static string Conver12HoursTo24Hours(DateTime time)
        {
            Logger.Log("Converting time from 12 clock to 24 clock");

            return time.ToString("HH:mm");
        }

        public static DateTime GetCurrentTime()
        {
            Logger.Log($"Getting current time {DateTime.Now}");
            return DateTime.Now;
        }

        public static bool IsValidInterval(DateTime time)
        {
            int minutes = time.Minute;
            
            return minutes == 0 || minutes == 15 || minutes == 30 || minutes == 45;
        }

        public static DateTime AdjustTime(DateTime time)
        {
            int minutes = time.Minute;
            int nextInterval  = ((minutes / 15) + 1) * 15;

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
            Logger.Log("Sleeping for 15 minutes\n");
            Thread.Sleep(sleepDuration);
        }

        public static void SleepUntilNextInterval(DateTime adjustedInterval, DateTime currentTime)
        {
            TimeSpan sleepDuration = adjustedInterval - currentTime;
            Thread.Sleep(sleepDuration);
        }
    }
}