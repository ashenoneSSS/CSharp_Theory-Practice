using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_01_27_operator_precedence_associativity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Precedence (telegram)

            //In same solution file there is a table lists  operators starting
            //with the highest precedence to the lowest. The operators within
            //each row have the same precedence.
            //or here:
            //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/

            int value = (10 + 8 * 3) / 4; //                                ==> parentheses operator (...)
            //value = (10 + 8 * 3) / 4;                                     ==> multiplicative operator *
            //value = (10 + 24) / 4;                                        ==> addictive operator +
            //value = 34 / 4;                                               ==> division /
            //value = 8;                                                    ==> assignment operator =
            System.Console.WriteLine(value + "\n");



            //Associativity

            //Left-associative operators are evaluated in order from
            //left to right. Except for the assignment operators and
            //the null-coalescing operators, all binary operators are left-associative.
            //
            //For example, a + b - c is evaluated as (a + b) - c            ==> left-associative
            //
            //And x = y = z is evaluated as x = (y = z)                     ==> right-associative


            int a,b;

            a = 10;
            b = 20;

            int c = 5 + a + (a = 20) + b + (b = 2);
            //same as c = 5 + 10 + 20 + 20 + 2;
            //because in a * (a = 20) is evaluated first (left-to-right),
            //so its current value 10 is used before the assignment a = 20

            a = 10;
            b = 20;

            int d = 5 + (a = 20) + (b = 2) + a + b;
            //same as d = 5 + 20 + 2 + 20 + 2;

            //and after all a = 20, b = 2

            Console.WriteLine(c + "\n" + d);

        }
    }
}
