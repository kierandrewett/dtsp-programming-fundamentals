using System;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] family = { "Mike", "Paul", "Suzy", "Steve", "John" };

            Console.WriteLine(family[2]);

            string boss = family[2];
            Console.WriteLine(boss);

            family[0] = "Kieran";

            int[] numbers = { 1, 2, 3, 4, 5 };

            Console.WriteLine(numbers);

            // create bool[] of size 5
            bool[] truth = new bool[5];
            Console.WriteLine(truth[2]);

            int[] grades = { 85, 42, 100, 12, 77, 92 };
            Console.WriteLine(grades[2]);

            string[] family2 = { "Dad", "Mum", "Brother", "Sister" };
            int[] age = new int[4];

            age[0] = 43;
            age[1] = 40;
            age[2] = 20;
            age[3] = 17;

            Console.WriteLine("My " + family2[0] + " is " + age[0] + " years old");
            Console.WriteLine("My " + family2[1] + " is " + age[1] + " years old");
            Console.WriteLine("My " + family2[2] + " is " + age[2] + " years old");
            Console.WriteLine("My " + family2[3] + " is " + age[3] + " years old");

            string[] friends = { "Ejaz", "Daniel", "Thomas", "Ben", "Jack" };

            for (int i = 0; i < friends.Length; i++)
            {
                Console.WriteLine(friends[i]);
            }

            foreach (string friend in friends)
            {
                Console.WriteLine(friend);
            }

            string[] cars = { "Corsa", "Fiesta", "Octavia", "Passat" };

            string hasCorsa = "A Corsa is not available.";

            foreach (string car in cars)
            {
                if (car.Equals("Corsa"))
                {
                    hasCorsa = "A Corsa is now available.";
                }
            }

            Console.WriteLine(hasCorsa);

            int min = grades[0];

            foreach (int grade in grades)
            {
                if (grade < min)
                {
                    min = grade;
                }
            }

            Console.WriteLine($"The lowest grade in the array is {min}");

            int max = grades[0];

            foreach (int grade in grades)
            {
                if (grade > max)
                {
                    max = grade;
                }
            }

            Console.WriteLine($"The highest grade in the array is {max}");

            string[] letters = { "A", "B", "C", "D", "E" };

            for (int i = letters.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(letters[i]);
            }
        }
    }
}
