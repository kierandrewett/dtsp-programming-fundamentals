using System;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            if (5 > 4)
            {
                Console.WriteLine("hello world");
            }

            Console.WriteLine("I alwyas run");

            if (7 == 4)
            {
                Console.WriteLine("I will never run");
            }

            if (7 != 10)
            {
                Console.WriteLine("The above statement is true");
                Console.WriteLine("The above statement is still true");
            }

            int grade = 40;

            if (grade > 40)
            {
                Console.WriteLine("You got more than 40");
            }

            if (grade < 40)
            {
                Console.WriteLine("You failed");
            }
        }
    }
}
