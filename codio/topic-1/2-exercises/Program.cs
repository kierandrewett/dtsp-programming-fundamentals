using System;

namespace bill
{
    class Program
    {
        static void Main(string[] args)
        {
            // Program.bill();
            // Program.triangles();
            Program.temperature();
        }

        static void bill()
        {
            Console.Write("Enter the amount of the bill in pounds: ");
            double.TryParse(Console.ReadLine(), out double bill);

            Console.Write("Enter the number of guests: ");
            int.TryParse(Console.ReadLine(), out int guests);

            double tip = bill * 0.12;
            double total = bill + tip;
            double perPerson = total / guests;

            Console.WriteLine("Tip: £{0:F2} GBP", tip);
            Console.WriteLine("Total: £{0:F2} GBP", total);
            Console.WriteLine("Per person: £{0:F2} GBP", perPerson);
        }

        static void triangles()
        {
            Console.Write("Enter length a: ");
            int.TryParse(Console.ReadLine(), out int lengthA);

            Console.Write("Enter length b: ");
            int.TryParse(Console.ReadLine(), out int lengthB);

            double lengthC = Math.Sqrt(Math.Pow(lengthA, 2) + Math.Pow(lengthB, 2));

            Console.WriteLine("Length of c is {0:F3}", lengthC);

            double area = 0.5 * lengthA * lengthB;

            Console.WriteLine("Area of triangle is {0:F2}", area);
        }

        static void temperature()
        {
            Console.WriteLine("Temperature Conversion Tool");
            Console.WriteLine("-------------------------------");
            Console.Write("Please enter a temperature in Celcius: ");
            float.TryParse(Console.ReadLine(), out float temp);

            float tempInFahrenheit = 32 + ((temp * 9) / 5);
            float tempInKelvin = temp + 273;

            Console.WriteLine("{0:F1}°C is equivalent to:", temp);
            Console.WriteLine("{0:F1}°F in Fahrenheit", tempInFahrenheit);
            Console.WriteLine("{0:F0}K in Kelvin", tempInKelvin);


        }
    }
}
