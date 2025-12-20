using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_04_22_data_input_ReadLine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Input string
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Hello, " + name + "\n");



            // Input int — first way (Convert)
            Console.WriteLine("Enter your age: ");
            string s_age = Console.ReadLine();
            int age = Convert.ToInt32(s_age);
            Console.WriteLine("I am " + age + " y.o" + "\n");

            // Input int — second way (Parse)
            Console.WriteLine("How much kg u weight:");
            int weight = int.Parse(Console.ReadLine());
            Console.WriteLine("U weight " + weight + " kg" + "\n");

            // Input int — third way (TryParse)
            // TryParse tries to convert the input string to an integer.
            // If the conversion succeeds, it returns true and sets the result in the 'height' variable.
            // If the input is not a valid integer, it returns false and skips the assignment.
            Console.WriteLine("Enter your height:");
            string height_input = Console.ReadLine();
            if (int.TryParse(height_input, out int height))
            {
                // This block executes if the input is a valid integer
                Console.WriteLine("Your height is " + height + " cm\n");
            }
            else
            {
                // This block executes if the input cannot be converted to an integer
                Console.WriteLine("Invalid height input.\n");
            }



            // Input double — first way (Convert)
            Console.WriteLine("Enter a decimal number (Convert):");
            double d1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("You entered: " + d1 + "\n");

            // Input double — second way (Parse)
            Console.WriteLine("Enter a decimal number (Parse):");
            double d2 = double.Parse(Console.ReadLine());
            Console.WriteLine("You entered: " + d2 + "\n");



            // Input boolean
            Console.WriteLine("Enter true or false:");
            bool flag = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("You entered: " + flag + "\n");



            // Input char
            Console.WriteLine("Enter a character:");
            char ch = Convert.ToChar(Console.ReadLine());
            Console.WriteLine("You entered: " + ch + "\n");

            Console.ReadLine();
            //https://learn.microsoft.com/en-us/dotnet/api/system.console.readline?view=net-9.0
        }
    }
}
