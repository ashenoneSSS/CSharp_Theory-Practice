using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace _25_08_30_static_base
{
    internal class Program
    {
        // =============================
        // Instance method (non-static)
        // =============================
        // Belongs to an object (instance of the class).
        // You must create an object before calling it.
        bool IsPrimeInstance(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i < n; i++)
                if (n % i == 0) return false;
            return true;
        }

        // =============================
        // Static method
        // =============================
        // Belongs to the class itself, not to any object.
        // You call it using ClassName.MethodName without creating an object.
        static bool IsPrimeStatic(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i < n; i++)
                if (n % i == 0) return false;
            return true;
        }

        static void Main(string[] args)
        {
            // --- Using instance method ---
            Program obj = new Program(); // create object
            for (int i = 2; i < 20; i++)
            {
                if (obj.IsPrimeInstance(i)) // call instance method on the object
                    Console.WriteLine("Instance prime: " + i);
            }

            // --- Using static method ---
            for (int i = 2; i < 20; i++)
            {
                if (IsPrimeStatic(i)) // call static method via class name
                    Console.WriteLine("Static prime: " + i);
            }

            Console.ReadLine();
        }
    }
}

/*
Summary:
- Instance method (non-static):
  * requires an object
  * can access both instance fields and static fields
  
- Static method:
  * does not require an object
  * can only access static fields or receive data via parameters
  * belongs to the class, not to any specific object
*/

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members

