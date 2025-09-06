using System;

namespace _25_08_11_MethodParameters_4types
{
    internal class Program
    {
        // ===========================
        // 1) BY VALUE (default)
        // ===========================
        // A copy of the value is passed into the method.
        // Changes inside the method do NOT affect the caller's variable.
        // Used when you don't want the method to modify the original value.
        static void IncreaseByValue(int number)
        {
            number += 10;
            Console.WriteLine("Inside method (by value): number = " + number);
        }

        static void ChangeStringByValue(string text)
        {
            // Strings are reference types but immutable → assigning a new string creates a new object.
            text = "New string";
            Console.WriteLine("Inside method (by value): text = " + text);
        }

        // ===========================
        // 2) REF
        // ===========================
        // Passes a variable by reference → method works with the original variable.
        // Must be initialized before passing.
        // Used when the method needs to modify the caller's variable.
        static void IncreaseByRef(ref int number)
        {
            number += 10;
            Console.WriteLine("Inside method (ref): number = " + number);
        }

        static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        // ===========================
        // 3) OUT
        // ===========================
        // Passes a variable by reference but does NOT need to be initialized before passing.
        // Method MUST assign a value before returning.
        // Used to return multiple values from a method.
        static void GetPoint(out int x, out int y)
        {
            x = 5;
            y = 8;
        }

        static void Divide(int dividend, int divisor, out int quotient, out int remainder)
        {
            quotient = dividend / divisor;
            remainder = dividend % divisor;
        }

        // ===========================
        // 4) IN
        // ===========================
        // Passes by reference but as READ-ONLY inside the method.
        // Cannot assign a new value to the parameter inside the method.
        // Used to avoid copying large structs while ensuring no modification.
        static void PrintInValue(in int number)
        {
            Console.WriteLine("Inside method (in): " + number);
            // number = 10; // ERROR: cannot modify a parameter passed with 'in'
        }

        struct LargeStruct
        {
            public int A;
            public int B;
            public int C;
            public int D;
            public int E;
        }

        static void ProcessLargeStruct(in LargeStruct data)
        {
            // No copy → better performance for large structs
            Console.WriteLine("Processing struct: " + data.A + ", " + data.B);
        }





        static void Main(string[] args)
        {
            // --- BY VALUE ---
            int a = 5;
            Console.WriteLine("Before (by value): a = " + a);
            IncreaseByValue(a);
            Console.WriteLine("After (by value): a = " + a + "\n");

            string str = "Original";
            Console.WriteLine("Before (by value, string): " + str);
            ChangeStringByValue(str);
            Console.WriteLine("After (by value, string): " + str + "\n");

            // --- REF ---
            int b = 5;
            Console.WriteLine("Before (ref): b = " + b);
            IncreaseByRef(ref b);
            Console.WriteLine("After (ref): b = " + b + "\n");

            int x1 = 1, y1 = 2;
            Console.WriteLine($"Before swap: x1 = {x1}, y1 = {y1}");
            Swap(ref x1, ref y1);
            Console.WriteLine($"After swap: x1 = {x1}, y1 = {y1}\n");

            // --- OUT ---
            int px, py;
            GetPoint(out px, out py);
            Console.WriteLine($"Point from GetPoint(): ({px}, {py})");

            Divide(17, 5, out int q, out int r);
            Console.WriteLine($"Divide 17 / 5 → Quotient = {q}, Remainder = {r}\n");

            // --- IN ---
            int value = 42;
            PrintInValue(in value);
            Console.WriteLine();

            LargeStruct ls = new LargeStruct { A = 10, B = 20, C = 30, D = 40, E = 50 };
            ProcessLargeStruct(in ls);

            Console.ReadLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/passing-parameters
