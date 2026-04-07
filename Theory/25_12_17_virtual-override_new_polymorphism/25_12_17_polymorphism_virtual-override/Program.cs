using System;
using System.Collections.Generic;

// ===========================
// VIRTUAL / OVERRIDE
// ===========================
// - virtual allows a base class method to be overridden in derived classes
// - override replaces the base implementation in a derived class
// - The method that actually runs is chosen at runtime based on the REAL object type (polymorphism)
// - However, you can also create a variable of a base class and store an object of a derived class in it
// - When you store a Dog object into a Mammal variable, the variable type becomes Mammal (upcasting)
// - You can only access members that exist in Mammal through that variable
// - Dog-only fields (like breed) are not lost, they are just inaccessible through the Mammal reference (hidden)

// ===========================
// NEW (method hiding)
// ===========================
// - new does NOT override
// - new hides a base member with the same name
// - The method that runs is chosen at compile time based on the VARIABLE type (not the real object type)
// - You can hide both non-virtual and virtual members
// - If you omit "new" when hiding, the compiler warns you
// - You can still reach the base version by casting to the base type

// ===========================
// Example 1: Mammal -> Dog/Cat
// ===========================
class Mammal
{
    public virtual void speak()                 // virtual class could have implementation
    {
        Console.WriteLine("Mammal Speaks");
    }
}

class Dog : Mammal
{
    public string breed;

    public Dog(string breed)
    {
        this.breed = breed;
    }

    public override void speak()                // without "override" m3.speak() will implement "Mammal Speaks", but not "Woof!"
    {
        Console.WriteLine("Woof!");
    }

    public void PrintBreed()
    {
        Console.WriteLine("Breed: " + breed);
    }
}

class Cat : Mammal
{
    public override void speak()
    {
        Console.WriteLine("Meow!");
    }
}

// ===========================
// Example 2: Shape -> Rectangle/Square/Circle
// ===========================
// - Base class reference in a list stores different derived objects
// - Calling a virtual method on Shape chooses the correct override automatically
abstract class Shape
{
    public virtual float getArea()
    {
        // - Base version is usually not used if all shapes must define area
        // - Here we keep it to show override behavior, but we will override in every derived class
        return 0.0f;
    }
}

class Rectangle : Shape
{
    private float width;
    private float height;

    public Rectangle(float width, float height)
    {
        this.width = width;
        this.height = height;
    }

    public override float getArea()
    {
        return width * height;
    }
}

class Square : Shape
{
    private float side;

    public Square(float side)
    {
        this.side = side;
    }

    public override float getArea()
    {
        return side * side;
    }
}

class Circle : Shape
{
    private float radius;

    public Circle(float radius)
    {
        this.radius = radius;
    }

    public override float getArea()
    {
        return 3.1415f * radius * radius;
    }
}

// ===========================
// Example 3: new hides a NON-VIRTUAL method
// ===========================
class ParentNonVirtual
{
    public void Hello()
    {
        Console.WriteLine("ParentNonVirtual.Hello");
    }
}

class ChildNonVirtual : ParentNonVirtual
{
    public new void Hello()
    {
        Console.WriteLine("ChildNonVirtual.Hello (hidden)");
    }
}

// ===========================
// Example 4: new hides a VIRTUAL chain (override vs new)
// ===========================
class BaseVirtual
{
    public virtual void Ping()
    {
        Console.WriteLine("BaseVirtual.Ping");
    }
}

class MidOverride : BaseVirtual
{
    public override void Ping()
    {
        Console.WriteLine("MidOverride.Ping (override)");
    }
}

class DerivedHide : MidOverride
{
    public new void Ping()
    {
        Console.WriteLine("DerivedHide.Ping (new hides, NOT override)");
    }
}

class ConsoleApp
{
    static void Main()
    {
        // ===========================
        // Example 1 output
        // ===========================

        // - These call overridden versions because the variable type is Dog/Cat
        Dog d1 = new Dog("Labrador");
        d1.speak();
        d1.PrintBreed();

        Cat c1 = new Cat();
        c1.speak();

        Console.WriteLine();

        // - However, you can also create a variable of a base class and store an object of a derived class in it
        Mammal m1 = new Mammal();
        Mammal m2 = new Cat();
        Mammal m3 = new Dog("Husky");

        // - IMPORTANT:
        // - The real objects are Mammal, Cat, Dog
        // - But the variable types are Mammal, Mammal, Mammal
        // - You can only access Mammal members through m2/m3

        m1.speak(); // Mammal Speaks
        m2.speak(); // Meow!  (real object is Cat)
        m3.speak(); // Woof!  (real object is Dog)

        // - The breed attribute of the Dog object is not accessible because when you store the Dog object
        // - into a Mammal variable, it implicitly gets converted into Mammal
        // - It means that you can only access the attributes and methods that are present in the Mammal class
        // - It doesn't mean that the value stored in the breed field is lost, it is only inaccessible
        // - and you can say that it's essentially hidden

        // Console.WriteLine(((Dog)m3).breed); // would work after casting, but without cast it's inaccessible
        // Console.WriteLine(m3.breed);        // ERROR: Mammal does not have "breed"

        Console.WriteLine();


        // ===========================
        // Example 2 output
        // ===========================
        List<Shape> shapes = new List<Shape>();

        shapes.Add(new Rectangle(25, 50));
        shapes.Add(new Square(50));
        shapes.Add(new Circle(10));

        Console.WriteLine(shapes[0].getArea()); // 1250.0
        Console.WriteLine(shapes[1].getArea()); // 2500.0
        Console.WriteLine(shapes[2].getArea()); // 314.15

        // - One list type (Shape) can store many different derived objects
        // - One call (getArea) runs different code depending on the real object type

        Console.WriteLine();

        // ===========================
        // Example 3 output (new hides NON-VIRTUAL)
        // ===========================
        ParentNonVirtual p1 = new ParentNonVirtual();
        ParentNonVirtual p2 = new ChildNonVirtual();
        ChildNonVirtual p3 = new ChildNonVirtual();

        p1.Hello(); // ParentNonVirtual.Hello
        p2.Hello(); // ParentNonVirtual.Hello (chosen by VARIABLE type ParentNonVirtual)
        p3.Hello(); // ChildNonVirtual.Hello (hidden)

        Console.WriteLine();

        // ===========================
        // Example 4 output (override chain + new hides at the end)
        // ===========================
        BaseVirtual v1 = new BaseVirtual();
        BaseVirtual v2 = new MidOverride();
        BaseVirtual v3 = new DerivedHide();
        MidOverride v4 = new DerivedHide();
        DerivedHide v5 = new DerivedHide();

        v1.Ping(); // BaseVirtual.Ping
        v2.Ping(); // MidOverride.Ping (override)
        v3.Ping(); // MidOverride.Ping (override dispatch stops at MidOverride because DerivedHide did NOT override)
        v4.Ping(); // MidOverride.Ping (variable type is MidOverride)
        v5.Ping(); // DerivedHide.Ping (new hides)

        // You can still call the override chain by casting
        ((MidOverride)v5).Ping(); // MidOverride.Ping
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/override
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier