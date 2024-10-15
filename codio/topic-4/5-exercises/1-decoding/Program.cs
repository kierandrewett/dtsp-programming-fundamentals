using System;
using System.Collections.Generic;

namespace p4cs
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> message = new List<string>();
            message.Add("a");
            message.Add("bad");
            message.Insert(0, "today");
            message.Insert(1, "is");
            message.RemoveAt(2);
            message.Add("good");
            message.Insert(2, "a");
            message.Add("day");

            foreach (string s in message)
            {
                Console.WriteLine(s);
            }

        }
    }
}
