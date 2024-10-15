using System;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] names = new string[3, 5];

            Console.WriteLine(names.GetLength(0) + " rows");
            Console.WriteLine(names.GetLength(1) + " columns");

            int[,] numbers = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };

            Console.WriteLine(numbers.GetLength(0) + " rows");
            Console.WriteLine(numbers.GetLength(1) + " columns");

            Console.WriteLine(numbers[0, 1]); // displays 2
            Console.WriteLine(numbers[2, 0]); // displays 5

            for (int row = 0; row < numbers.GetLength(0); row++)
            {
                for (int col = 0; col < numbers.GetLength(1); col++)
                {
                    Console.Write("The value is " + numbers[row, col] + " ");
                }

                Console.WriteLine();
            }

            for (int row = 0; row < numbers.GetLength(0); row++)
            {
                Console.WriteLine("The values are {0} and {1}", numbers[row, 0], numbers[row, 1]);
            }
        }
    }
}
