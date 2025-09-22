using System;
using System.Collections.Generic;

namespace _25_02_04_foreach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ===========================
            // FOREACH LOOP BASICS
            // ===========================
            // foreach is used to iterate through collections (arrays, lists, etc.)

            // foreach id READONLY

            // Syntax:
            // foreach (Type element in collection)
            // {
            //     // code using element
            // }

            // --- Example 1: simple array ---
            int[] numbers = { 7, 2, 3, 4, 5 };
            Console.WriteLine("Array elements:");
            foreach (int n in numbers)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine("\n");

            // --- Example 2: string characters ---
            string word = "Hello";
            Console.WriteLine("Characters in string:");
            foreach (char c in word)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine("\n");

            // --- Example 3: List<T> ---
            List<string> fruits = new List<string> { "Apple", "Banana", "Cherry" };
            Console.WriteLine("Fruits:");
            foreach (string fruit in fruits)
            {
                Console.WriteLine(fruit);
            }
            Console.WriteLine();

            // --- Example 5: nested foreach ---
            int[,] matrix = { { 1, 2 }, { 3, 4 } };
            Console.WriteLine("Matrix with nested foreach:");
            foreach (int x in matrix)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine("\n");

            // --- Example 6: modifying collection elements inside foreach (not allowed directly) ---
            // foreach gives read-only access to 'element' variable (you cannot reassign it).
            // If you need modification, use for-loop or separate indexing.
            Console.WriteLine("Trying modification:");
            foreach (int n in numbers)
            {
                Console.WriteLine("Read element: " + n);
                // n = n + 1; // ERROR: cannot assign to iteration variable
            }
            Console.WriteLine();

            // --- Example 7: break and continue ---
            Console.WriteLine("Break and Continue in foreach:");
            foreach (int n in numbers)
            {
                if (n == 2) continue; // skip this iteration
                if (n == 4) break;    // exit the loop
                Console.WriteLine("n = " + n);
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-foreach-statement
