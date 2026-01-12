using System;

namespace _25_09_16_is_as
{
    // ===========================
    // is / as operators (OOP)
    // ===========================
    // - is checks type compatibility and returns bool
    // - Use is when you need a boolean check or pattern variable

    // - as tries safe cast and returns null if cast failed (works only with reference types and nullable value types)
    // - Use as when you want to cast and then check for null

    // - Both are common with inheritance, interfaces, and polymorphism

    interface IPrintable
    {
        void Print();
    }

    class Animal
    {
        public virtual void Speak()
        {
            Console.WriteLine("Animal speaks");
        }
    }

    class Dog : Animal, IPrintable
    {
        public string Breed { get; private set; }

        public Dog(string breed)
        {
            Breed = breed;
        }

        public override void Speak()
        {
            Console.WriteLine("Woof");
        }

        public void Fetch()
        {
            Console.WriteLine("Dog fetches");
        }

        public void Print()
        {
            Console.WriteLine("Dog: " + Breed);
        }
    }

    class Cat : Animal
    {
        public override void Speak()
        {
            Console.WriteLine("Meow");
        }

        public void Scratch()
        {
            Console.WriteLine("Cat scratches");
        }
    }

    class Car : IPrintable
    {
        public string Model { get; private set; }

        public Car(string model)
        {
            Model = model;
        }

        public void Print()
        {
            Console.WriteLine("Car: " + Model);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // ===========================
            // Example 1: is (simple check)
            // ===========================
            Animal a1 = new Dog("Husky");
            Animal a2 = new Cat();

            Console.WriteLine(a1 is Dog); // true
            Console.WriteLine(a2 is Dog); // false
            Console.WriteLine();

            // ===========================
            // Example 2: is + pattern variable
            // ===========================

            // If the check is true, a new variable "d" of type Dog is created,
            // and it refers to the same object as "a1" (the underlying new Dog("Husky") instance).
            if (a1 is Dog d)
            {
                d.Fetch();
                Console.WriteLine("Breed: " + d.Breed);
            }

            if (a2 is Cat c)
            {
                c.Scratch();
            }

            Console.WriteLine();

            // ===========================
            // Example 3: is with interface
            // ===========================
            object obj1 = new Dog("Labrador");
            object obj2 = new Car("BMW");
            object obj3 = "just string";

            // - Works with interfaces too
            if (obj1 is IPrintable p1)
                p1.Print();

            if (obj2 is IPrintable p2)
                p2.Print();

            if (obj3 is IPrintable p3)
                p3.Print(); // won't run

            Console.WriteLine();

            // ===========================
            // Example 4: as (safe cast)
            // ===========================
            // - as returns null if cast fails
            object o1 = new Dog("Beagle");
            Dog dog1 = o1 as Dog;
            if (dog1 != null)
            {
                dog1.Speak();
                dog1.Fetch();
            }

            object o2 = new Cat();
            Dog dog2 = o2 as Dog; // fails → null
            Console.WriteLine(dog2 == null); // true
            Console.WriteLine();

            // ===========================
            // Example 5: as with interface + method call
            // ===========================
            // - Useful when you want to try multiple behaviors
            object unknown = new Car("Audi");

            IPrintable printable = unknown as IPrintable;
            if (printable != null)
                printable.Print();

            Dog maybeDog = unknown as Dog;
            if (maybeDog != null)
                maybeDog.Fetch();
            else
                Console.WriteLine("Not a Dog, cannot call Fetch");
            Console.WriteLine();

            // ===========================
            // Example 6: using is/as inside a method (polymorphism)
            // ===========================
            HandleAnimal(new Dog("Corgi"));
            HandleAnimal(new Cat());
            HandleAnimal(new Animal());
            Console.WriteLine();


            Console.ReadLine();
        }
        static void HandleAnimal(Animal animal)
        {
            // - This method accepts base class type
            // - But runtime object might be Dog or Cat
            animal.Speak();

            // is pattern
            if (animal is Dog d)
            {
                Console.WriteLine("HandleAnimal detected Dog");
                d.Fetch();
                return;
            }

            // as casting
            Cat c = animal as Cat;
            if (c != null)
            {
                Console.WriteLine("HandleAnimal detected Cat");
                c.Scratch();
                return;
            }

            Console.WriteLine("HandleAnimal: just Animal");
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast
