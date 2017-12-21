using System;
namespace DgWebAPI.DAL
{
    public static class TimeParser
    {
        public static string GetTimeRandom()
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTimeToUtc(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds; // 相差毫秒数
            string random = "";
            for (var i = 0; i < 5; i++)
            {
                Random r = new Random();
                random += r.Next(0, 9).ToString();
            }
            return timeStamp.ToString() + random;
        }
    }
}
