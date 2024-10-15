using System;
using System.Collections.Generic;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            // Program.TimesTables();
            // Program.Exponentiation();
            // Program.GuessingGame();
            Program.PriceOfBread();
            // Program.SubtractionQuiz();
        }

        static void TimesTables()
        {
            Console.Write("Enter any positive or negative number: ");
            int num = int.Parse(Console.ReadLine());

            Console.WriteLine("Multiplication table of {0}", num);

            for (int i = 1; i <= 10; i++)
            {
                int result = num * i;

                Console.WriteLine($"{num} x {i} = {result}");
            }
        }

        static void Exponentiation()
        {
            Console.Write("Enter the base number: ");
            double baseNum = int.Parse(Console.ReadLine());

            Console.Write("Enter the power: ");
            double pow = int.Parse(Console.ReadLine());

            long res = Convert.ToInt64(Math.Pow(baseNum, pow));

            Console.WriteLine($"Result: {res}");
        }

        static void GuessingGame()
        {
            Random rng = new Random();

            Console.WriteLine("Guess a magic number between 1 and 100");
            Console.WriteLine("----------------------------------------");

            int guess = -1;
            int answer = rng.Next(1, 100);

            do
            {
                Console.WriteLine("Enter your guess: ");
                Console.Write("> ");

                bool parsed = int.TryParse(Console.ReadLine(), out guess);

                if (!parsed)
                {
                    Console.WriteLine("Invalid integer, please guess again.");
                }
                else
                {
                    if (guess > 100 || guess < 0)
                    {
                        Console.WriteLine("Your guess is outside the allowed range.");
                    }
                    else if (guess < answer)
                    {
                        Console.WriteLine("Your guess is too low.");
                    }
                    else if (guess > answer)
                    {
                        Console.WriteLine("Your guess is too high.");
                    }
                }
            } while (guess != answer);

            Console.WriteLine("Congratulations, the magic number I was looking for was: {0}", answer);
        }

        static void PriceOfBread()
        {
            const double originalPrice = 1.50;
            double price = originalPrice;

            Console.WriteLine("Currently, the price of bread is: £{0:f2}", price);

            for (int year = 1; year <= 10; year++)
            {
                price = 1.50 * (1.08 * year);

                Console.WriteLine("The price of bread after {0} years is: £{1:f2}", year, price);
            }

            Console.Write("In 10 years, the price of bread will have ");

            if (price < originalPrice)
            {
                Console.Write("decreased ");
            }
            else
            {
                Console.Write("increased ");
            }

            double percentDiff = Math.Abs(((price - originalPrice) / originalPrice) * 100);

            Console.Write("by {0:f0}%.", percentDiff);
            Console.WriteLine();
        }

        static void SubtractionQuiz()
        {
            Console.WriteLine("Subtraction Quiz");
            Console.WriteLine("-------------------------------------");

            const int numQuestions = 5;

            List<int[]> data = new List<int[]> { };

            Random rng = new Random();

            for (int i = 1; i <= numQuestions; i++)
            {
                int x = rng.Next(0, 34);
                int y = rng.Next(0, 34);
                int computedAnswer = x - y;

                data.Add(new int[] { x, y, -1 });

                bool isAnswerValid = false;

                while (!isAnswerValid)
                {
                    Console.Write("Question {0}: {1} - {2} = ", i, data[i - 1][0], data[i - 1][1]);

                    if (isAnswerValid = int.TryParse(Console.ReadLine(), out data[i - 1][2]))
                    {
                        if (data[i - 1][2] == computedAnswer)
                        {
                            Console.WriteLine("Correct.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect, the answer was {0}", computedAnswer);
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid integer, please try again.");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Your score");
            Console.WriteLine("------------------------");

            int correct = 0;
            int wrong = 0;
            for (int i = 0; i < data.Count; i++)
            {
                int[] x = data[i];

                if (x[0] - x[1] == x[2])
                {
                    correct++;
                }
                else
                {
                    wrong++;
                }
            }

            double percentRight = ((float)correct / (float)numQuestions) * 100.0;

            Console.WriteLine("You got {0} correct answers, and {1} wrong answers.", correct, wrong);
            Console.WriteLine("Percent correct: {0:f2}%", percentRight);

            if (correct >= numQuestions)
            {
                Console.WriteLine("Congratulations! You got all the answers right.");
            }
            else if (correct <= 0)
            {
                Console.WriteLine("Better luck next time.");
            }
            else
            {
                Console.WriteLine("Well done! You got most of the questions right.");
            }
        }
    }
}
