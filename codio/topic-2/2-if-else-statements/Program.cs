using System;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            bool test = true;

            if (test)
            {
                Console.WriteLine("The bool is true");
            }
            else
            {
                Console.WriteLine("The bool is false");
            }

            int grade = 62;
            if (grade < 60)
                Console.WriteLine("F");
            else if (grade < 70)
                Console.WriteLine("D");
            else if (grade < 80)
                Console.WriteLine("C");
            else if (grade < 90)
                Console.WriteLine("B");
            else if (grade <= 100)
                Console.WriteLine("A");

        }

        static void Rain(string[] args)
        {
            bool windy = true;
            bool rainy = true;
            bool cold = true;

            if (rainy)
            {
                if (windy)
                {
                    // wear a rain jacket
                }
                else
                {
                    // bring an umbrella
                }
            }
            else
            {
                if (cold)
                {
                    // you might need a coat
                }
                else
                {
                    // enjoy your day
                }
            }
        }
    }
}
