using System;

namespace exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            // int dayOfWeek = 5;

            // switch (dayOfWeek) {

            //     case 1: Console.WriteLine("Sunday"); //only prints if dayOfWeek == 1

            //     case 2: Console.WriteLine("Monday"); //only prints if dayOfWeek == 2

            //     case 3: Console.WriteLine("Tuesday"); //only prints if dayOfWeek == 3

            //     case 4: Console.WriteLine("Wednesday"); //only prints if dayOfWeek == 4

            //     case 5: Console.WriteLine("Thursday"); //only prints if dayOfWeek == 5

            //     case 6: Console.WriteLine("Friday"); //only prints if dayOfWeek == 6

            //     case 7: Console.WriteLine("Saturday"); //only prints if dayOfWeek == 7

            //     default: Console.WriteLine("Invalid"); //only prints if none of the above are true


            // }

            int grade = 62;
            int letterGrade = grade / 10;
            switch (letterGrade)
            {
                case 10: case 9: Console.WriteLine("A"); break;
                case 8: Console.WriteLine("B"); break;
                case 7: Console.WriteLine("C"); break;
                case 6: Console.WriteLine("D"); break;
                default: Console.WriteLine("F"); break;
            }

            int studentAnswer = 3;
            String feedback1 = "This answer is wrong because....";
            String feedback2 = "This answer is correct! You know this because...";
            String feedback3 = "This answer is wrong. While the first part is correct...";
            String feedback;

            int correctAnswer = 2;
            int points = 0;

            // switch (studentAnswer)
            // {
            //     case 1: feedback = feedback1; break;
            //     case 2: feedback = feedback2; break;
            //     case 3: feedback = feedback3; break;
            //     default: feedback = "Invalid answer choice"; break;
            // }

            // Console.WriteLine(feedback);

            if (studentAnswer == 1)
            {
                feedback = feedback1;
            }
            else if (studentAnswer == 2)
            {
                feedback = feedback2;
            }
            else if (studentAnswer == 3)
            {
                feedback = feedback3;
            }
            else
            {
                feedback = "Invalid answer choice";
            }

            Console.WriteLine(feedback);


        }
    }
}
