using System;

namespace _25_09_13_Class_Basics
{
    // ===========================
    // CLASS BASICS
    // ===========================
    // - class is a reference type
    // - can contain fields, methods, constructors, properties
    // - fields get default values if not initialized (numbers → 0, bool → false, reference → null)

    class Person
    {
        // fields
        public string name;    // default value = null
        public int age;        // default value = 0
        public bool isAlive;   // default value = false

        // constructor (not required to fill all fields)
        public Person(string name)
        {
            this.name = name;
            // age and isAlive remain default if not assigned
        }

        // method
        public void Print()
        {
            Console.WriteLine($"Name: {name}, Age: {age}, Alive: {isAlive}");
        }
    }


    // Struct for comparison
    struct Point
    {
        public int x;
        public int y;
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Normally when you are creating an object of a class, 
            you create a variable to hold the reference of that 
            object(instance of the class)

            And the type of the variable is usually the class of that object.
            */   
            
            // ===========================
            // 1) Syntax of creating class instance
            // ===========================

            Person p1 = new Person("Alice");
            p1.Print(); // Age=0, Alive=false (default values)

            Console.WriteLine();


            // ===========================
            // 2) Class = reference type vs struct = value type
            // ===========================

            // --- Example with class ---
            Person p2 = new Person("Bob");
            Person p3 = p2;  // both refer to the SAME object
            p2.age = 30;
            p3.isAlive = true;

            Console.WriteLine("Class reference behavior:");
            p2.Print(); // changes visible from both p2 and p3
            p3.Print();

            Console.WriteLine();

            // --- Example with struct ---
            Point pt1 = new Point { x = 5, y = 10 };
            Point pt2 = pt1;   // COPY created
            pt1.x = 99;

            Console.WriteLine("Struct value behavior:");
            Console.WriteLine($"pt1: {pt1.x}, {pt1.y}");
            Console.WriteLine($"pt2: {pt2.x}, {pt2.y}");
            // pt1 changed, pt2 unchanged (because struct is copied by value)
        }
    }
}

/*
Summary:
- class is reference type → assignment copies reference (two variables point to same object)
- struct is value type → assignment copies the whole value (independent copy)
- fields in class automatically initialized with defaults
- creating an instance: ClassName obj = new ClassName(arguments);
*/

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/classes
