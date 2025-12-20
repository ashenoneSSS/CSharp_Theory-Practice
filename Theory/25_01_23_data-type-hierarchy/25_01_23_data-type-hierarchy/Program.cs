using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_01_23_data_type_hierarchy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // byte b = 130;
            // byte r = b + 45;
            //This will be error "cannot implicitly convert type 'int' to 'byte' "
            //because arithmetic operations automatically promote ushort, short, sbyte, byte and char  to int during the calculation

            //instesd we can use explicit conversion
            byte b = 130;
            byte r = (byte)(b + 45);

            Console.WriteLine(r + "\n");



            // Data type hierarchy (Starting with the highest type):
            // double
            // float
            // ulong
            // long
            // uint
            // int
            // char

            Console.WriteLine(10 + 'G' + 10L + 23.5 + 32U);

            //it will goes from left to right
            //Console.WriteLine(10 + 'G' + 10L + 23.5 + 32U);       int > char
            //Console.WriteLine(81 + 10L + 23.5 + 32U);             long > int
            //Console.WriteLine(91 + 23.5 + 32U);                   double > long 
            //Console.WriteLine(114,5 + 32U);                       double > uint
            //Console.WriteLine(146,5);                             The final result will be calculated and displayed as a double


            // Data types that do not belong to the hierarchy:
            // decimal
            // bool

            //Console.WriteLine(10 + 2345.34m + 'G' + 10L + true + 23.5 + 32U); is ERROR

        }
    }
}
