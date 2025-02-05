using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_01_30_increment_decrement_logical_operators
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Postfix increment operator

            int i = 3;
            Console.WriteLine(i);   // output: 3
            Console.WriteLine(i++); // output: 3
            Console.WriteLine(i);   // output: 4
            Console.WriteLine();

            //Prefix increment operator

            double a = 1.5;
            Console.WriteLine(a);   // output: 1.5
            Console.WriteLine(++a); // output: 2.5
            Console.WriteLine(a);   // output: 2.5
            Console.WriteLine();

            //Same with Postfix and Prefix decrement



            //Logcail Operators

            bool SecondOperand()
            {
                Console.WriteLine("Second operand is evaluated.");
                return true;
            }

            // condition1 && condition2 (conditional logical "AND")
            // true  && true  -> true
            // false && true  -> false
            // true  && false -> false
            // false && false -> false

            bool b = false && SecondOperand();
            Console.WriteLine(b + "\n");
            // right-hand operand of the && operator is a method call, which
            // isn't performed if the left-hand operand evaluates to false


            // condition1 & condition2 (logical "AND")
            // true  & true  -> true
            // false & true  -> false
            // true  & false -> false
            // false & false -> false

            bool c = false & SecondOperand();
            Console.WriteLine(c + "\n");
            // The logical AND operator & also computes the
            // logical AND of its operands, but always evaluates both operands


            // condition1 || condition2 (conditional logical "OR")
            // true  || true  -> true
            // false || true  -> true
            // true  || false -> true
            // false || false -> false
            bool d = true || SecondOperand();
            Console.WriteLine(d + "\n");

            // condition1 | condition2 (logical "OR")
            // true  | true  -> true
            // false | true  -> true
            // true  | false -> true
            // false | false -> false

            bool e = true | SecondOperand();
            Console.WriteLine(e + "\n");
            // The logical OR operator & also computes the
            // logical OR of its operands, but always evaluates both operands


            // condition1 ^ condition2 (logical exclusive "OR" / "XOR")
            // true  ^ true  -> false
            // false ^ true  -> true
            // true  ^ false -> true
            // false ^ false -> false


            Console.WriteLine("Precedence:");
            Console.WriteLine(10 + 2 > 3 * 5 && 500 == 100 - 2);
            //Console.WriteLine(12 > 15 && 500 == 98);      Arithmetic operators have more precedence
            //Console.WriteLine(False && 500 == 98);        Comparison operators have more precedence
            //Console.WriteLine(False && 500 == 98);        Equality operator have more precedence
            //Console.WriteLine(False && False);            conditional logical "AND"
            //Console.WriteLine(False)                      output: false

        }
    }
}
