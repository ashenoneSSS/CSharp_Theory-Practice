using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_01_03_string_interpolation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int age = 18;
            string name = "Vova";

            //Two ways that we already know to output text
            System.Console.WriteLine("My name is " + name + ", and my age is " + age);
            System.Console.WriteLine("My name is {0}, and my age is {1}", name, age);

            //Outputing text by using string interpolation (syntax is add $ before ' " ')
            System.Console.WriteLine($"My name is {name}, and my age is {age}\n\n");


            //Example of string interpolation with composite formating
            const int NameAlignment = -12;
            const int ValueAlignment = 8;
            double a = 3;
            double b = 4;
            Console.WriteLine($"Three classical Pythagorean means of {a} and {b}:");
            Console.WriteLine($"|{"Arithmetic",NameAlignment}|{0.5 * (a + b),ValueAlignment:F3}|");
            Console.WriteLine($"|{"Geometric",NameAlignment}|{Math.Sqrt(a * b),ValueAlignment:F3}|");
            Console.WriteLine($"|{"Harmonic",NameAlignment}|{2 / (1 / a + 1 / b),ValueAlignment:F3}|\n\n");


            //Example of string interpolation with verbatim string
            var userName = "ne_kazual";
            Console.WriteLine($@"C:\Users\{userName}\Documents");
            //You can use $ and @ in any order: both $@"..." and @$"..." are valid interpolated verbatim strings

        }
    }
}
