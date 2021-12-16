using System;
using System.Collections.Generic;
using System.Linq;

namespace NTR.Entities
{
    public static class Debugger
    {
        public static void Log(IEnumerable<string> data)
        {
            string output = "";
            foreach(string s in data)
            {
                output = output + "|" + s;
            }
            System.IO.File.AppendAllText("debug.txt", output + '\n');
        }
    }
}
