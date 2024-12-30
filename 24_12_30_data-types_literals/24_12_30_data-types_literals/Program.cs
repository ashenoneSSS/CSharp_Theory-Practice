using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_12_30_data_types_literals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //difference between difference between

            //Literals: Fixed values written directly in the code. They don’t change during execution
            //Variables: Named storage for values that can change during execution

            //name of variables cannot start with digits or any of this symbols: "!#$%^&*()+"
            int age = 10;
            double distance = 123.45;
            char myFavoriteCharacter = '$';
            string name = "Penis";
            

            System.Console.WriteLine("My name is " + name);
            System.Console.WriteLine("My age is " + age);
            System.Console.WriteLine("Distance to my home = " + distance + "m.");
            System.Console.WriteLine("My favorite character: " + myFavoriteCharacter + "\n\n");


            int number = 100;

            //difference between using String Concatenation Operator (+)
            System.Console.WriteLine(number + 20);
            System.Console.WriteLine("Number = "+ number + 20);
            System.Console.WriteLine("Number = " + (number + 20));
            //but
            System.Console.WriteLine(number + 20 + " is number");

        }
    }
}
