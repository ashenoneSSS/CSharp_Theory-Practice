using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_01_02_composite_formatting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Syntax of a formatting element:
            // {ElementIndex,Alignment:FormatSpecifier}
            // {ElementIndex,Alignment}
            // {ElementIndex:FormatSpecifier}
            // {ElementIndex}

            //First example (ElementIndex)
            int age = 18;
            Console.WriteLine("My name is {0} and i {1} years old \n\n", "Vova", age);

            //Second example (ElementIndex,Alignment)
            Console.WriteLine("Right-aligned table:");
            Console.WriteLine("|{0,8}|{1,10}|{2,16}|{3,10}|", "value 1", "value 2", "value 3", "value 4");
            Console.WriteLine("+--------+----------+----------------+----------+");
            Console.WriteLine("|{0,8}|{1,10}|{2,16}|{3,10}|", "value 5", "value 6", "value 7", "value 8");
            Console.WriteLine("+--------+----------+----------------+----------+");
            Console.WriteLine("|{0,8}|{1,10}|{2,16}|{3,10}|\n\n", "value 9", "value 10", "value 11", "value 12");

            Console.WriteLine("Left-aligned table:");
            Console.WriteLine("|{0,-8}|{1,-10}|{2,-16}|{3,-10}|", "value 1", "value 2", "value 3", "value 4");
            Console.WriteLine("+--------+----------+----------------+----------+");
            Console.WriteLine("|{0,-8}|{1,-10}|{2,-16}|{3,-10}|", "value 5", "value 6", "value 7", "value 8");
            Console.WriteLine("+--------+----------+----------------+----------+");
            Console.WriteLine("|{0,-8}|{1,-10}|{2,-16}|{3,-10}|\n\n", "value 9", "value 10", "value 11", "value 12");

            //Third exmple (ElementIndex:FormatSpecifier)
            int hex_value = 255;
            Console.WriteLine("{0:X}", hex_value);
            Console.WriteLine("{0:x}\n", hex_value);

            double scientific_value = 1234.5678;
            Console.WriteLine("{0:E2}\n", scientific_value);

            int padded_value = 5;
            Console.WriteLine("{0:D3}\n", padded_value);

            double fixed_point_value = 1234.5678;
            Console.WriteLine("{0:F2}", fixed_point_value);
            Console.WriteLine("{0:G}\n", fixed_point_value);

            double percentage_value = 0.12345;
            Console.WriteLine("{0:P2}", percentage_value);
        }
    }
}
