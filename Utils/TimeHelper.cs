namespace Utils
{
    public class TimeHelper
    {
        public static string Conver12HoursTo24Hours(DateTime time)
        {

            return time.ToString("HH:mm");
        }

        public static DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}

/*
DateTime d = new DateTime(1, 1, 1, 23, 12, 0);
var res = d.ToString("hh:mm tt"); // this shows 11:12 PM
var res2 = d.ToString("HH:mm");   // this shows 23:12
*/