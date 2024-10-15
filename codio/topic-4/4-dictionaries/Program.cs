using System;
using System.Collections.Generic;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> ages = new Dictionary<string, int>();
            ages.Add("Chris", 46);
            ages.Add("Bing", 22);

            int chrisAge = ages["Chris"];
            Console.WriteLine($"Chris is {chrisAge} years old.");
            Console.WriteLine("Bing is {0}", ages["Bing"]);

            int count = 0;
            foreach (KeyValuePair<string, int> _ in ages)
            {
                count += 1;
            }

            Console.WriteLine($"There are {count} elements.");

            foreach (KeyValuePair<string, int> kv in ages)
            {
                Console.WriteLine($"{kv.Key} = {kv.Value}");
            }

            Dictionary<string, int>.KeyCollection keys = ages.Keys;

            foreach (var k in keys)
            {
                Console.WriteLine($"Key = {k}");
            }

            Dictionary<string, int>.ValueCollection values = ages.Values;

            foreach (var v in values)
            {
                Console.WriteLine($"Value = {v}");
            }

            ages["Bing"] = 23;

            if (ages.ContainsKey("Bing"))
            {
                Console.WriteLine("Got bingus");
            }
            else
            {
                Console.WriteLine("Haven't got bingus :(");
            }

            bool validInput = false;
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Dictionaries");
                Console.WriteLine("--------------------------------");

                Console.WriteLine("1) Add new item");
                Console.WriteLine("2) Search by key");
                Console.WriteLine("3) Edit by key");
                Console.WriteLine("0) Exit");

                int choice = -1;

                validInput = int.TryParse(Console.ReadLine(), out choice);

                if (validInput)
                {
                    switch (choice)
                    {
                        case 0:
                            exit = true;
                            break;
                        case 1:
                            Console.WriteLine("Add new item");
                            Console.WriteLine("--------------------------------");

                            string key = "";
                            int val = 0;

                            bool isValid = false;

                            while (!isValid)
                            {
                                Console.Write("Enter key: ");
                                key = Console.ReadLine().Trim();

                                isValid = key.Length > 0;

                                if (!isValid)
                                {
                                    Console.WriteLine("Invalid string please try again.");
                                }
                            }

                            isValid = false;

                            while (!isValid)
                            {
                                Console.Write("Enter value: ");

                                string strVal = Console.ReadLine().Trim();
                                isValid = int.TryParse(strVal, out val);

                                if (!isValid)
                                {
                                    Console.WriteLine("Invalid integer please try again.");
                                }
                            }

                            ages.Add(key, val);

                            Console.WriteLine($"Added {key} = {val}.");

                            break;
                        case 2:
                            Console.WriteLine("Search by key");
                            Console.WriteLine("--------------------------------");

                            string searchKey = "";

                            while (true)
                            {
                                Console.Write("Enter key to search: ");
                                searchKey = Console.ReadLine().Trim();

                                if (searchKey.Length <= 0)
                                {
                                    Console.WriteLine("Invalid string please try again.");
                                }
                                else if (!ages.ContainsKey(searchKey))
                                {
                                    Console.WriteLine($"Ages does not contain {searchKey}.");
                                }
                                else
                                {
                                    break;
                                }
                            }

                            int searchVal = ages[searchKey];

                            Console.WriteLine($"{searchKey} = {searchVal}");

                            break;
                        case 3:
                            Console.WriteLine("Edit by key");
                            Console.WriteLine("--------------------------------");

                            string editKey = "";
                            int editVal = 0;

                            while (true)
                            {
                                Console.Write("Enter key to edit: ");
                                editKey = Console.ReadLine().Trim();

                                if (editKey.Length <= 0)
                                {
                                    Console.WriteLine("Invalid string please try again.");
                                }
                                else if (!ages.ContainsKey(editKey))
                                {
                                    Console.WriteLine($"Ages does not contain {editKey}.");
                                }
                                else
                                {
                                    break;
                                }
                            }

                            bool isEditValid = false;

                            while (!isEditValid)
                            {
                                Console.Write("Enter new value: ");

                                string strVal = Console.ReadLine().Trim();
                                isEditValid = int.TryParse(strVal, out editVal);

                                if (!isEditValid)
                                {
                                    Console.WriteLine("Invalid integer please try again.");
                                }
                            }

                            ages[editKey] = editVal;
                            Console.WriteLine($"{editKey} = {editVal}");

                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Input is not valid, try again.");
                }
            }

        }
    }
}
