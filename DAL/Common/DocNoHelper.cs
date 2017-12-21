using System;
namespace DgWebAPI.DAL
{
    public static class DocNoHelper
    {
        /// <summary>
        /// 生成日期字符串
        /// </summary>
        /// <returns>The document no.</returns>
        /// <param name="prefix">单号前缀，可选</param>
        public static string InitDateString(string prefix = "")
        {
            string dateString = prefix;
            DateTime now = DateTime.Now;
            string month = now.Month > 9 ? now.Month.ToString() : "0" + now.Month.ToString();
            string day = now.Day > 9 ? now.Day.ToString() : "0" + now.Day.ToString();
            dateString = string.Format(@"{0}{1}{2}{3}", dateString, now.Year, month, day);
            return dateString;
        }

        /// <summary>
        /// 生成单号
        /// </summary>
        /// <returns>The document no.</returns>
        /// <param name="dateString">日期字符串</param>
        /// <param name="number">单号值</param>
        public static string InitDocNo(string dateString, int number)
        {
            string docNo = "";
            number += 1;
            if (number < 10)
            {
                docNo = string.Format(@"{0}00{1}", dateString, number);
            }
            else if (number >= 10 && number < 100)
            {
                docNo = string.Format(@"{0}0{1}", dateString, number);
            }
            else
            {
                docNo = string.Format(@"{0}{1}", dateString, number);
            }
            return docNo;
        }
    }
}
