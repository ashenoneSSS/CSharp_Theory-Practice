using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_12_31_const_variables
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //the way to create const var same as in C++
            const double PI = 3.14;
            double radius = 6.67;

            Console.WriteLine("Area of circle: " + (radius * radius * PI));

            //in C# i can do this
            int num1 = 17;
            int num2 = num1;

            Console.WriteLine("\nNumber2 = " + num2);

            //but cannot do this:
            //const int const_num = num1;
            
            //this because const value is determined at compile time, meaning the value must be fixed and known when the code is compiled

            //A const variable must always be assigned a literal or another const
            const int const_num1 = 27;
            const int const_num2 = const_num1;

            Console.WriteLine("\nConstant number2 = " + const_num2);


        }
    }
}
