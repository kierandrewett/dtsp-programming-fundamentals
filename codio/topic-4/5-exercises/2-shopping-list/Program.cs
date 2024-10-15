using System;
using System.Collections.Generic;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            list.Add("Apple");
            list.Add("Banana");
            list.Add("Banana");
            list.Add("Dumpling");

            foreach (string item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------------------------------");

            int bananaIndex = list.FindIndex(c => c.Equals("Banana"));

            list.RemoveAt(bananaIndex);
            list.Insert(bananaIndex + 1, "Carrot");

            foreach (string item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
