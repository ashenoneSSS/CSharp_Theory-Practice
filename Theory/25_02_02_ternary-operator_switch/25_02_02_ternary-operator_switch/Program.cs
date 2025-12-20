using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _25_02_02_ternary_operator_switch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ternary operator syntax
            //
            //condition ? consequent : alternative

            //If condition evaluates to true, the consequent expression is evaluated.
            //If condition evaluates to false, the alternative expression is evaluated.
            //and its result becomes the result of the operation

            //is this condition true ? yes : no


            //example 1
            int tempInCelsius = 27;
            Console.WriteLine(tempInCelsius < 20.0 ? "Cold" : "Warm");

            tempInCelsius = 15;
            Console.WriteLine(tempInCelsius < 20.0 ? "Cold" : "Warm");
            Console.WriteLine();


            //example 2
            double money_vasted_1 = 10000.1;
            double discount;
            discount = money_vasted_1 > 10000.0 ? 10.0 : 5.0;

            double final_cost = money_vasted_1 - ((discount / 100.0) * money_vasted_1);
            Console.WriteLine(final_cost);
            Console.WriteLine();


            //Switch syntax
            /*
            switch (expression)
            {
                case value1:
                    // Code to execute if expression == value1
                    break;

                case value2:
                    // Code to execute if expression == value2
                    break;

                case value3:
                case value4:
                    // Code for multiple cases executing the same block
                    break;

                default:
                    // Code to execute if no case matches
                    break;
            }
            */


            //example of multiple cases executing the same block
            int f = 2;

            switch (f)
            {
                case 1:
                    Console.WriteLine("case ");
                    break;
                case 2: //here
                case 3: 
                    Console.WriteLine("case tree"); 
                    break;
                default:
                    Console.WriteLine("defaul case");
                    break;
            }


            //switch with when
            //in this version of C# we cannot implement comparing operators without "when" keyword
            int age = 20;

            switch (age)
            {
                case int n when n < 18:
                    Console.WriteLine("Minor");
                    break;
                case int n when n >= 18 && n < 65:
                    Console.WriteLine("Adult");
                    break;
                default:
                    Console.WriteLine("Senior");
                    break;
            }

        }
    }
}
