using System;
using System.Collections.Generic;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> folks = new List<string>();
            List<int> nums = new List<int>();

            folks.Add("Mary");
            folks.Add("Jack");
            folks.Add("Mike");
            folks.Add("Wilbur");
            folks.Add("Henry");

            Console.WriteLine(folks.Count);

            for (int i = 0; i < folks.Count; i++)
            {
                Console.WriteLine(folks[i]);
            }

            foreach (string person in folks)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("Removing jack from folks...");
            folks.Remove("Jack");
            foreach (string person in folks)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("Sorting folks...");
            folks.Sort();

            foreach (string person in folks)
            {
                Console.WriteLine(person);
            }

            folks[2] = folks[2].ToUpper();

            foreach (string person in folks)
            {
                Console.WriteLine(person);
            }

            folks.Add("Jack");
            Console.WriteLine("Finding jack...");

            Console.WriteLine(folks.Find(n => n.Contains("Jack")).ToUpper());

            foreach (string person in folks)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("Removing the uppercase JACK...");

            folks.Remove(folks.Find(n => n.Contains("JACK")));
            foreach (string person in folks)
            {
                Console.WriteLine(person);
            }
        }
    }
}
