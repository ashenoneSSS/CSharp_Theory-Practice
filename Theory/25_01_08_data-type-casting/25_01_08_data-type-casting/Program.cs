using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_01_08_data_type_casting_hierarchy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IMPLICIT CONVERSION

            int num1 = 2147483647;
            long bigNum = num1;
            //we can do this because 'long' can hold any value an 'int' can hold


            int num2 = 45;
            double d1 = num2;
            //same with 'double' and 'int'
            Console.WriteLine(d1 + "\n");


            float num3 = 45.97f;
            double d2 = num3;
            //same with 'double' and 'float'
            Console.WriteLine("{0:F4} \n", d2);



            //EXPLICIT CONVERSION

            //example 1
            double x = 1234.567;
            int a;

            //Syntax of casting double to int: (type)value || (type)(expression)
            a = (int)x;
            //a = x; is error because 'int' smaller than 'double'
            System.Console.WriteLine(a);


            //example 2
            int int_var = 30;
            //we can do this
            byte var1 = 30;
            byte var2 = 255;

            //byte var3 = a;
            //but this is error

            //Compiler doesn't allow implicit casting from an int to a byte,
            //this is because 'int' is a larger type and can hold values outside the range of a 'byte'

            //instead we can use explicit cast:
            byte var3 = (byte)(int_var); //we explicitly cast 'a' to a byte
            Console.WriteLine(var3 + "\n");


            //method ToString()
            double d3 = 45.67;
            string str_value2 = d3.ToString(); // Convert double to string
            Console.WriteLine(str_value2);
        }
    }
}
