using System;
using System.Collections;

namespace p4cs
{
    class GreetingMachine
    {
        // add code below this line
        public static void SayHello(ArrayList people)
        {
            foreach (string person in people)
            {
                Console.WriteLine("Hello " + person);
            }
        }
        // add code above this line

        public static void Main(String[] args)
        {
            ArrayList people = new ArrayList();
            people.Add("Tom");
            people.Add("Richard");
            people.Add("Harriet");

            SayHello(people);
        }
    }
}
