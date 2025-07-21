using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_07_07_enumaration
{
    //An enumeration type (or enum) is a special value type that lets you use named values instead of numbers.
    //It’s like giving easy-to-read names to numbers inside your code.

    enum Day         
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday = 9,
        Sunday        // because of Satarday is 9 Sunday will be 10
    }

    //You can also explicitly specify the associated constant values, as the following example shows:
    enum ErrorCode : ushort
    {
        None = 0,
        Unknown = 1,
        ConnectionLost = 100,
        OutlierReading = 200
    }

    enum Race
    {
        Ork = 1,
        Elf = 2,
        Human = 3
    }


    internal class Program
    {
        
        static void Main(string[] args)
        {

            // Basic enum usage
            Day today = Day.Monday;
            Console.WriteLine(today);           // prints "Monday"
            Console.WriteLine((int)today);
            Console.WriteLine();// prints 0

            today = Day.Friday;
            Console.WriteLine(today);           // prints "Friday"
            Console.WriteLine((int)today);      // prints 4
            Console.WriteLine();

            // Example 2
            Race variable;

            variable = (Race)1;
            System.Console.WriteLine(variable);
            Console.WriteLine((int)variable);


            Console.ReadLine();
        }
    }
}
