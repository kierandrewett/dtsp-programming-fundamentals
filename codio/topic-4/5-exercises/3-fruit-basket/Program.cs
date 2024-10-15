// Write a program that accepts names of fruits, typed by a user at the keyboard, and then stores them in a collection. 
// Your program should only accept the following fruit: mango, papaya, guava, orange, apple.
// Your program should do the following:

//     Print all of the items in the list.
//     Count the number of fruit of each of the five types and print these totals.
//     Allow the user to select a fruit and then display the total for that type.
//     Allow the user to enter a fruit and remove all items of that type from the list.
//     Display the list sorted alphabetically.
//     Display the list sorted in reverse alphabetical order.
//     Replace all apples with another fruit not in the list of five types.

using System;

namespace exercises
{
    enum TUIOption
    {
        PrintOptions,
        AddFruit,
        TotalFruit,
        Quit,
    }

    class Program
    {
        static bool exitLock = false;
        static bool firstStart = true;

        static List<string> store = new List<string>();

        const string DIVIDER_STR = "----------------------------------";

        static string[] ALLOWED_FRUITS = new string[] {
            "mango",
            "papaya",
            "guava",
            "orange",
            "apple"
        };

        static string GetOptionName(TUIOption option)
        {
            switch (option)
            {
                case TUIOption.PrintOptions:
                    return "Show stored fruits (alphabetically)";
                case TUIOption.AddFruit:
                    return "Add a fruit...";
                case TUIOption.TotalFruit:
                    return "Get total number of fruits...";
                case TUIOption.Quit:
                    return "Quit";
                default:
                    return "Unknown option";
            }
        }

        static T Prompt<T>(string question, Func<string, bool> validator, Func<string, T> parser)
        {
            while (true)
            {
                Console.Write($"{question}: ");

                string? input = Console.ReadLine();

                if (input != null && validator(input.Trim()))
                {
                    return parser(input.Trim());
                }
                else
                {
                    Console.WriteLine("Error: Invalid input, please try again.");
                }
            }
        }

        static void PrintBasicFruitsStored()
        {
            string fruitsJoined = "No fruits just yet.";

            if (Program.store.Count > 0)
            {
                fruitsJoined = string.Join(", ", Program.store);
            }

            Console.WriteLine("Fruits: {0}", fruitsJoined);
        }

        static string GetFruitHumanName(string fruit)
        {

            switch (fruit.ToLower())
            {
                case "mango":
                    return "Mango";
                case "papaya":
                    return "Papaya";
                case "guava":
                    return "Guava";
                case "orange":
                    return "Orange";
                case "apple":
                    return "Apple";
                default:
                    return "Unknown fruit";
            }
        }

        static void HandleOption(TUIOption option)
        {
            switch (option)
            {
                case TUIOption.PrintOptions:
                    Program.PrintBasicFruitsStored();
                    break;
                case TUIOption.AddFruit:
                    Console.WriteLine("Allowed fruit options: {0}", string.Join(", ", Program.ALLOWED_FRUITS));

                    string chosenFruit = Program.Prompt<string>(
                        "Choose a fruit to add",
                        x => x.Length > 0 && Program.ALLOWED_FRUITS.Contains(x.ToLower()),
                        x => x.ToLower()
                    );

                    Console.WriteLine("Adding {0}...", chosenFruit);
                    Program.store.Add(chosenFruit);

                    break;
                case TUIOption.TotalFruit:
                    Console.WriteLine("Allowed fruit options: {0}", string.Join(", ", Program.ALLOWED_FRUITS));

                    string chosenFruitTotal = Program.Prompt<string>(
                        "Choose a specific fruit or press enter to see all fruits",
                        x => Program.ALLOWED_FRUITS.Contains(x.ToLower()) || x.Equals(""),
                        x => x.ToLower()
                    );

                    Console.WriteLine("The total count of:");

                    foreach (string fruit in Program.ALLOWED_FRUITS)
                    {
                        if (chosenFruitTotal.Length > 0 && fruit != chosenFruitTotal)
                        {
                            continue;
                        }

                        int count = Program.store.Count(x => x.ToLower().Equals(fruit));

                        Console.WriteLine("{0}: {1}", Program.GetFruitHumanName(fruit), count);
                    }

                    break;
                case TUIOption.Quit:
                    Console.WriteLine("Quitting, thank you and goodbye.");
                    exitLock = true;
                    break;
                default:
                    Console.WriteLine("Error: Not a valid option, please try again.");
                    break;
            }
        }

        static void Main(string[] args)
        {
            while (!Program.exitLock)
            {
                if (!Program.firstStart)
                {
                    Console.WriteLine(Program.DIVIDER_STR);
                }
                Console.WriteLine("Fruit Basket");

                string fruitsJoined = "No fruits just yet.";

                if (Program.store.Count > 0)
                {
                    fruitsJoined = string.Join(", ", Program.store);
                }

                Console.WriteLine("Fruits: {0}", fruitsJoined);

                Console.WriteLine(Program.DIVIDER_STR);

                Program.firstStart = false;

                TUIOption[] options = new TUIOption[] {
                    TUIOption.PrintOptions,
                    TUIOption.AddFruit,
                    TUIOption.TotalFruit,
                    TUIOption.Quit
                };

                for (int i = 0; i < options.Length; i++)
                {
                    string optName = Program.GetOptionName(options[i]);

                    Console.WriteLine($"{i + 1}) {optName}");
                }

                Console.WriteLine(Program.DIVIDER_STR);
                Console.Write("> ");

                int chosenOption = -1;

                string? input = Console.ReadLine();

                if (input == null)
                {
                    continue;
                }

                input = input.Trim();

                if (input.Length <= 0)
                {
                    continue;
                }

                bool isValid = int.TryParse(input, out chosenOption);

                if (isValid)
                {
                    if (options.Length > chosenOption - 1)
                    {
                        TUIOption option = options[chosenOption - 1];

                        Console.WriteLine(Program.DIVIDER_STR);
                        Program.HandleOption(option);
                    }
                    else
                    {
                        Console.WriteLine("Error: Input is not a valid option.");

                    }
                }
                else
                {
                    Console.WriteLine("Error: Input is not a valid integer.");
                }
            }
        }
    }
}