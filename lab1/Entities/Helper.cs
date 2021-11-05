using System;
using System.Collections.Generic;
using System.Linq;

namespace NTR.Entities
{
    public static class Debugger
    {
        public static void DebugLog(List<string> data)
        {
            string output = "";
            foreach(string s in data)
            {
                output = output + "|";
            }
            System.IO.File.AppendAllText("debug.txt", output + '\n');
        }
    }
}
