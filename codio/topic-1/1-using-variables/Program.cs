using System;

namespace p4cs
{
    public class Program
    {
        static void Main(string[] args)
        {
            // bool a = True;
            bool b = true;

            String a = "hello";

            // Console.WriteLine(a);
            Console.WriteLine(b);

            // Fails because of wrong type, using single quotes indicates a char not string
            // String c = 'hello';
            String d = "hello";

            // Console.WriteLine(c);
            Console.WriteLine(d);

            String quotes = "This is a \"quote mark \nin a \tstring";
            Console.WriteLine(quotes);

            // throws runtime errorye
            // Console.WriteLine(int.Parse("32.45"));
        }
    }
}
