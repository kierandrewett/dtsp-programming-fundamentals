using System;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Add(5, 7);
            // Add(5, "seven"); // error
            // Add(); // error
            Add(5, 10, 15);

            AddSub(5, 10, 15);
            AddSub(10, 15, 5);
            AddSub(15, 5, 10);
            AddSub(10 + 5, 20 / 4, 5 * 2);

            Divide(5, 2);
            Divide(10, 2);
            Divide(0, 2);
            // Divide(14.5, 2); // error, 14.5 is a double

            String[] names = { "Mike", "Steve", "John" };
            PrintArray(names);
        }

        /**
         * This method adds two integers together
         * 
         * @param num1 The first integer
         * @param num2 The second integer
         */
        public static void Add(int num1, int num2)
        {
            Console.WriteLine(num1 + num2);
        }

        // Overloaded function
        public static void Add(int num1, int num2, int num3)
        {
            Console.WriteLine(num1 + num2 + num3);
        }

        /**
         * This method adds the first two integers together,
         * then subtracts the third integer
         * 
         * @param num1 The first integer
         * @param num2 The second integer
         * @param num3 The third integer
         */
        public static void AddSub(int num1, int num2, int num3)
        {
            Console.WriteLine(num1 + num2 - num3);
        }

        /**
         * This method divides one integer by the other
         * 
         * @param num1 The first integer
         * @param num2 The second integer
         */
        public static void Divide(int num1, int num2)
        {
            try
            {
                Console.WriteLine(num1 / num2);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot divide by zero");
            }
        }

        /**
         * This method prints all values of an array
         * 
         * @param arr is an array of strings
         */
        public static void PrintArray(String[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }
    }
}
