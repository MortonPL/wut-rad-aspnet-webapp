using System;
using System.Collections.Generic;

namespace NTR.Helpers
{
    public static class Helper
    {
        public static void Log(string data)
        {
            System.IO.File.AppendAllText("debug.txt", data + '\n');
        }

        public static void ListLog(IEnumerable<string> data)
        {
            string output = "";
            foreach(string s in data)
            {
                output = output + "|" + s;
            }
            System.IO.File.AppendAllText("debug.txt", output + '\n');
        }

        public static DateTime GetYM(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 1, 1, 1);
        }

        public static DateTime GetYMD(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 1, 1, 1);
        }
    }
}
