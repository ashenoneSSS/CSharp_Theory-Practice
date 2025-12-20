using System;
using System.IO;
using System.Collections.Generic;

namespace _25_02_04_for_features
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            ===============================
            STRUCTURE OF "for" STATEMENT
            ===============================

            General form:
            for ( initialization ; condition ; iteration )
            {
                // loop body
            }

            -------------------------------
            1) INITIALIZATION
            -------------------------------
            - Executed once at the start of the loop.
            - Can declare variables (int i = 0) 
              or assign values to existing ones (i = 5, j = 10).
            - Only declarations/assignments are allowed.
            - Expression statements like "sum += i" are NOT allowed here.

            Examples of allowed forms:
            for (int i = 0, j = 10; ... ; ... )   // declaration
            for (i = 5, j = 20; ... ; ... )       // assignment

            Invalid form:
            for (int i = 0, sum += 5; ... ; ... ) // error, not initialization

            -------------------------------
            2) CONDITION
            -------------------------------
            - Evaluated before each iteration.
            - Must be an expression that returns bool (true/false).
            - If true → loop continues; if false → loop ends.
            - If omitted → treated as "true" (infinite loop).

            Examples of valid conditions:
            i < 10
            x != y
            flag == true
            true   // infinite loop

            Invalid form:
            i + 5  // error, does not return bool

            -------------------------------
            3) ITERATION
            -------------------------------
            - Executed after each loop body.
            - Can be any expression or multiple expressions separated by commas.
            - Common use: increments/decrements (i++, j--).
            - But can also contain method calls, assignments, printing, etc.

            Examples:
            i++
            i--, sum += i
            Console.WriteLine(i--)
            Foo(), Bar(i)

            -------------------------------
            SUMMARY
            -------------------------------
            - Initialization → only declarations or assignments
            - Condition → must evaluate to bool
            - Iteration → any expressions (one or many)
            */



            // 1) Classic counter
            for (int i = 0; i < 5; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine("\n");

            // 2) Omit initializer (i declared before)
            int j = 0;
            for (; j < 3; j++)
            {
                Console.Write(j + " ");
            }
            Console.WriteLine("\n");

            // 3) Omit iterator (do update inside the body)
            for (int k = 0; k < 3;)
            {
                Console.Write(k + " ");
                k++; // manual increment
            }
            Console.WriteLine("\n");

            // 4) Omit condition → infinite loop (break required)
            int hits = 0;
            for (int t = 0; ; t++)
            {
                hits++;
                if (t >= 2) break; // must break manually
            }
            Console.WriteLine("hits = " + hits + "\n");

            // 5) Everything omitted → infinite loop
            int guard = 0;
            for (; ; )
            {
                guard++;
                if (guard == 2) break;
            }
            Console.WriteLine("guard = " + guard + "\n");

            // 6) Empty body (semicolon at the end) → all work in header/iterator
            int sum = 0;
            int f = 0;
            for (int x = 1; x <= 5; sum += x, f -= x, x++) ; // empty body on purpose
            Console.WriteLine("sum = " + sum + "\n");

            // 7) Two counters moving inward
            int[] arr = { 1, 2, 3, 4, 5, 6 };
            for (int left = 0, right = arr.Length - 1; left < right; left++, right--)
            {
                // swap ends
                int tmp = arr[left];
                arr[left] = arr[right];
                arr[right] = tmp;
            }
            Console.WriteLine("reversed: " + string.Join(",", arr) + "\n");

            // 8) Mixed iterator actions (math + method call)
            for (int a = 0, b = 10; a < b; a += 2, b--, LogTick(a, b)) { }
            Console.WriteLine();

            // 9) Multiple declarations
            for (int a1 = 0, a2 = 5; a1 < a2; a1++, a2--) { /* work */ }
            // a1, a2 are not accessible here            

            // 10) Predicate call in condition
            for (int n = 0; is_equal_to_five(n) == false; n++)
            {
                Console.Write(n + "  ");
            }
            Console.WriteLine("\n");

            // 11) Using short-circuit logic
            int p = 0;
            for (int q = 5; q > 0 && p < 3; q--, p++)
            {
                Console.WriteLine($"q={q}, p={p}");
            }
            Console.WriteLine();

            // 12) Step > 1
            for (int z = 0; z <= 10; z += 2)
            {
                Console.Write(z + " ");
            }
            Console.WriteLine("\n");

            // 13) Post-check trick (uses i++ in condition)
            for (int i2 = 0; i2++ < 3;)
            {
                Console.WriteLine("i2 now = " + i2); // prints 1, 2, 3
            }
            Console.WriteLine();

            // 14) Iterator with method calls (side effects)
            int counter = 0;
            for (int u = 0; u < 3; u++, counter += Bump())
            {
                Console.WriteLine("counter = " + counter);
            }
            Console.WriteLine();


            Console.ReadLine();

        }

        //used in 8-th
        static void LogTick(int a, int b)
        {
            Console.WriteLine($"tick: a={a}, b={b}");
        }

        //used in 14-th
        static int Bump()
        {
            // any side-effectful work for iterator section
            return 1;
        }

        //used in 10-th
        static bool is_equal_to_five(int x)
        {
            if (x == 5)
                return true;
            else
                return false;
        }

    }
}

// Docs:
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-for-statement
