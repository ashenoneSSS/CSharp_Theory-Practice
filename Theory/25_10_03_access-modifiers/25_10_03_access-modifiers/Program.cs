using System;

namespace _25_09_14_AccessModifiers
{
    // ===========================
    // ACCESS MODIFIERS IN C#
    // ===========================
    //
    // - public      → accessible from anywhere
    // - private     → accessible only inside the same class (default for class members)
    // - protected   → accessible in the same class and in derived (child) classes
    //
    // Key facts:
    // - if no modifier is specified → members of a class are private by default
    // - private fields and methods are NOT inherited by derived classes
    // - to make them visible to child classes but not to external code → use "protected"
    // - access modifiers can be used for classes, methods, fields, and constructors


    // ===========================
    // EXAMPLE 1: private
    // ===========================
    class ExampleDefault
    {
        int number; // default → private
        void Display() // default → private
        {
            Console.WriteLine("Default private field and method");
        }

        public void TryAccess()
        {
            number = 10;
            Display(); // allowed
        }
    }


    // ===========================
    // EXAMPLE 2: public vs private
    // ===========================
    class Person
    {
        private string name;    // visible only inside Person
        public int age;         // visible everywhere

        public void SetName(string n)
        {
            name = n;
        }

        public void Print()
        {
            Console.WriteLine($"Name: {name}, Age: {age}");
        }
    }


    // ===========================
    // EXAMPLE 3: protected
    // ===========================
    class Animal
    {
        protected string species;  // visible to this class and its children
        private int age;           // NOT visible to child classes

        protected void MakeSound()
        {
            Console.WriteLine("Generic animal sound");
        }

        public void SetAge(int a)
        {
            age = a; // accessible here
        }
    }

    class Dog : Animal
    {
        public void Speak()
        {
            species = "Dog";  // allowed (protected field inherited)
            MakeSound();      // allowed (protected method inherited)
            // age = 5;       // ❌ not allowed (private not inherited)
            Console.WriteLine($"Species: {species}");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // Example 1
            ExampleDefault ex = new ExampleDefault();
            // ex.number = 10;
            ex.TryAccess(); // works because public method inside accesses private data
            Console.WriteLine();


            // Example 2
            Person p = new Person();
            // p.name = "Alice";
            p.age = 25; // public → accessible
            p.SetName("Alice");
            p.Print();
            Console.WriteLine();


            // Example 3
            Dog d = new Dog();
            d.Speak(); // uses protected members from Animal
            Console.WriteLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers
