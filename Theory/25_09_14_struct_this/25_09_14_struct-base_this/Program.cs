using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_09_14_struct_base
{
    // ===========================
    // STRUCT BASICS
    // ===========================
    // - struct is a value type (stored on the stack)
    // - can contain fields, properties, methods, constructors
    // - supports "this" keyword (refers to the current struct instance)
    // - useful for lightweight objects (small data containers)
    // - constructors must assign ALL fields

    struct Clock
    {
        // ===========================
        // Fields
        // ===========================
        public int hours;
        public int minutes;

        // ===========================
        // Constructor  (is a method which is automatically executed when a new object is created.)
        // ===========================
        public Clock(int hours, int minutes)
        {
            if ((hours >= 24 || hours < 0) || (minutes >= 60 || minutes < 0))
            {
                Console.WriteLine("Invalid time, set to 00:00");
                this.hours = 0;     // "this" points to the instance field, not the constructor parameter.
                this.minutes = 0;  
            }
            else
            {
                this.hours = hours;
                this.minutes = minutes;
            }
        }

        // ===========================
        // Method with parameter
        // ===========================
        public void AddMinutes(int minutes)
        {
            int x;
            if ((this.minutes + minutes) < 60)
            {
                this.minutes = this.minutes + minutes;
            }
            else
            {
                x = (this.minutes + minutes) / 60;
                this.minutes = (this.minutes + minutes) % 60;

                if ((this.hours + x) < 24)
                {
                    this.hours = this.hours + x;
                }
                else
                {
                    this.hours = (this.hours + x) % 24;
                }
            }
        }

        // ===========================
        // Method without parameter
        // ===========================
        public void Print()
        {
            Console.WriteLine($"Time is {this.hours}:{this.minutes}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Create struct with invalid values (constructor will fix them)
            Clock myClock = new Clock(33, 95);
            myClock.Print();

            // Call method that modifies struct fields
            myClock.AddMinutes(5);
            myClock.Print();

            Console.ReadLine();
        }
    }
}


/*
Summary:
- struct = value type, lightweight, often used for small objects (like coordinates, time, etc.).
- Can contain: fields, methods, constructors, properties.
- "this" refers to the current struct instance.
- Constructors must assign all fields.
- Good for objects that don’t need inheritance.
*/

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct




