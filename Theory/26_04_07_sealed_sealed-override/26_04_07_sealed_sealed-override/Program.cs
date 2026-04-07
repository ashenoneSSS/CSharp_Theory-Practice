using System;

// =======================
// sealed and sealed override in C#
// =======================
//
// sealed class
// A sealed class cannot be inherited
// You use it when you want to stop further inheritance completely
// Typical usecases: safety, invariants, simpler design, performance opportunities
//
// sealed override
// You can seal only an override of a virtual/abstract method
// It means: "this override is the final one, nobody can override it further"
// You use it to allow inheritance, but lock a specific behavior at some level
//
// Where it matters
// Polymorphism still works
// Base reference calling a virtual method will dispatch to the most derived override that exists
// If an override is sealed, dispatch stops there (derived classes cannot change it)

public class Animal
{
    public virtual string Speak() => "Animal sound";
}

public class Dog : Animal
{
    public override string Speak() => "Woof";
}

public sealed class Cat : Animal
{
    public override string Speak() => "Meow";
}

// Cat cannot be inherited
// public class Tiger : Cat { } // Does not compile: cannot derive from sealed type




public class Document
{
    public virtual string Render() => "Render: base document";
}

public class HtmlDocument : Document
{
    // This override is final for all further derived classes
    public sealed override string Render() => "Render: HTML document";
}

public class FancyHtmlDocument : HtmlDocument
{
    // You cannot override Render anymore because it was sealed in HtmlDocument
    // public override string Render() => "Render: fancy HTML"; // Does not compile: sealed member
}




public class Account
{
    public virtual decimal GetFee(decimal amount) => amount * 0.01m;
}

public class PremiumAccount : Account
{
    // Not sealed, still overridable further
    public override decimal GetFee(decimal amount) => amount * 0.005m;
}

public class VipAccount : PremiumAccount
{
    // Sealed at this level, no further overrides allowed
    public sealed override decimal GetFee(decimal amount) => 0m;
}

public class UltraVipAccount : VipAccount
{
    // public override decimal GetFee(decimal amount) => 1m; // Does not compile: sealed member
}




public class PipelineStep
{
    public virtual string Execute(string input) => $"Base execute: {input}";
}

public class NormalizeStep : PipelineStep
{
    // Sealing protects the normalization rule from being changed by descendants
    public sealed override string Execute(string input) => $"Normalized: {input.Trim().ToLowerInvariant()}";

    // You can still add new virtual methods here if you want extensibility elsewhere
    public virtual string Describe() => "Trims and lowercases input";
}

public class LoggingNormalizeStep : NormalizeStep
{
    // Execute is sealed, cannot override
    // But Describe is virtual, so overriding it is allowed
    public override string Describe() => "Trims and lowercases input (with logging in caller)";
}



public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Example 1: sealed class stops inheritance, but polymorphism still works");

        Animal a1 = new Dog();
        Animal a2 = new Cat();

        Console.WriteLine(a1.Speak());
        Console.WriteLine(a2.Speak());

        Console.WriteLine();
        Console.WriteLine("Example 2: sealed override blocks further overrides in an inheritance chain");

        Document d1 = new Document();
        Document d2 = new HtmlDocument();
        Document d3 = new FancyHtmlDocument();

        Console.WriteLine(d1.Render());
        Console.WriteLine(d2.Render());
        Console.WriteLine(d3.Render()); // Still uses HtmlDocument.Render because it is sealed

        Console.WriteLine();
        Console.WriteLine("Example 3: sealing later in the chain (override is allowed until it becomes sealed)");

        Account acc1 = new Account();
        Account acc2 = new PremiumAccount();
        Account acc3 = new VipAccount();
        Account acc4 = new UltraVipAccount();

        Console.WriteLine(acc1.GetFee(1000m));
        Console.WriteLine(acc2.GetFee(1000m));
        Console.WriteLine(acc3.GetFee(1000m));
        Console.WriteLine(acc4.GetFee(1000m)); // Same as VipAccount because GetFee is sealed there

        Console.WriteLine();
        Console.WriteLine("Example 4: seal a critical behavior but allow extension elsewhere");

        PipelineStep step = new LoggingNormalizeStep();
        Console.WriteLine(step.Execute("  HeLLo  ")); // Execute is sealed in NormalizeStep
        Console.WriteLine(((NormalizeStep)step).Describe()); // Describe is overridable and overridden
    }
}

// Microsoft Learn
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/override
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual