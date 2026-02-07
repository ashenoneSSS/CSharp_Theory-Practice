using System;
using System.Collections.Generic;

// =======================
// Delegates, multicast delegates, custom delegates, anonymous methods, lambdas
// =======================
//
// Delegate (simple)
// A delegate is a type that can store a reference to a method
// The method must match the delegate signature (parameter types and return type)
// You can
// - assign a method to a delegate variable
// - pass a delegate into another method (callback)
// - return delegate from another method
// - invoke it later like a normal method call
//
// Delegate signature example
// delegate int Operation(int a, int b)
//
// Assigning methods
// Operation op = Sum
// Operation op2 = Multiply
//
// Invoking
// int r = op(2, 3)
//
// Null check
// Delegates can be null (no target)
// Use ?.Invoke(...) or if (d != null) d(...)
//
// Multicast delegate
// A delegate can hold multiple methods in an invocation list
// += adds a method to the end of the list in the order it was added
// -= removes one matching occurrence from the list
// Invoking a multicast delegate calls subscribers in order
// If the delegate returns a value, only the last subscriber's return value is observed
// If a subscriber throws, the invocation stops at that subscriber
//
// Built-in delegate types
// Action
// Represents a method that returns void
// Action a = () => { }
// Action<int> a1 = (int x) => { }
// Action<int, string> a2 = (int x, string s) => { }
//
// Func
// Represents a method that returns a value
// Last generic type argument is the return type
// Func<int> f0 = () => 123
// Func<int, int> f1 = (int x) => x + 1
// Func<int, int, bool> f2 = (int a, int b) => a == b         (will return bool value)
//
// Predicate<T>
// Represents a method T -> bool (a common "check" shape)
// Predicate<int> p = (int x) => x % 2 == 0
//
// Lambda expression (shorter form)
// (Operation)((int a, int b) => a + b)
// It literally same thing if you would do int Sum(int a, int b) function and call it
//
// Anonymous function syntax (two forms)
// Anonymous method
// (Operation)delegate (int a, int b) { return a + b; }

public delegate void Notify(string message);

public sealed class MessagePublisher
{
    public Notify OnNotify;

    public void RaiseNotification(string message)
    {
        OnNotify?.Invoke(message);
    }

    public void PrintInvocationList()
    {
        if (OnNotify is null)
        {
            Console.WriteLine("OnNotify is null (no subscribers)");
            return;
        }

        Delegate[] list = OnNotify.GetInvocationList();
        Console.WriteLine($"Subscribers count = {list.Length}");
        for (int i = 0; i < list.Length; i++)
            Console.WriteLine($"[{i}] {list[i].Method.DeclaringType?.Name}.{list[i].Method.Name}");
    }
}

public sealed class SmsSubscriber
{
    public void ReceiveSms(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}

public sealed class EmailSubscriber
{
    public void ReceiveEmail(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}

public static class Program
{
    private delegate int MathOperation(int a, int b);

    private static int Multiply(int a, int b) => a * b;
    private static int Sum(int a, int b) => a + b;

    private static int DSMO_v1(Func<int, int, int> mo, int a, int b)
    {
        Console.WriteLine("Inside method (Func)");
        return mo(a, b);
    }

    private static int DSMO_v2(MathOperation mo, int a, int b)
    {
        Console.WriteLine("Inside method (custom delegate)");
        return mo(a, b);
    }

    public static void Main()
    {
        Console.WriteLine("Example A: Multicast delegate subscriptions (publisher/subscribers)");

        var publisher = new MessagePublisher();
        var sms = new SmsSubscriber();
        var email = new EmailSubscriber();

        publisher.OnNotify += sms.ReceiveSms;
        publisher.OnNotify += email.ReceiveEmail;
        publisher.OnNotify += sms.ReceiveSms;
        publisher.OnNotify += email.ReceiveEmail;
        publisher.OnNotify += sms.ReceiveSms;
        publisher.OnNotify += email.ReceiveEmail;
        publisher.OnNotify += email.ReceiveEmail;
        publisher.OnNotify += email.ReceiveEmail;
        publisher.OnNotify -= sms.ReceiveSms;
        publisher.OnNotify += email.ReceiveEmail;
        publisher.OnNotify += sms.ReceiveSms;
        publisher.OnNotify += email.ReceiveEmail;

        int seen = 0;
        publisher.OnNotify += delegate (string message)
        {
            seen++;
            Console.WriteLine($"Anonymous method: seen={seen}, len={message.Length}");
        };

        publisher.PrintInvocationList();
        publisher.RaiseNotification("Hello World!");

        Console.WriteLine();
        Console.WriteLine("Example B: Delegate as parameter (custom delegate + Func), plus anonymous method");

        Console.WriteLine("Enter 1 == '*'  2 == '+'  3 == '-'");
        string raw = Console.ReadLine();
        int decide = int.TryParse(raw, out int tmp) ? tmp : 2;

        int x = 2, y = 3;
        int result;

        switch (decide)
        {
            case 1:
                result = DSMO_v2(Multiply, x, y);
                break;
            case 2:
                result = DSMO_v2(Sum, x, y);
                break;
            case 3:
                result = DSMO_v1(delegate (int a, int b) { return a - b; }, x, y);
                break;
            default:
                result = DSMO_v1(delegate (int a, int b) { return a + b; }, x, y);
                break;
        }

        Console.WriteLine($"Result = {result}");

        Console.WriteLine();
        Console.WriteLine("Example C: Built-in delegates Action, Func, Predicate");

        Action<string> log = delegate (string s) { Console.WriteLine($"Action: {s}"); };
        Func<int, int, int> add = (a, b) => a + b;
        Predicate<int> is_even = delegate (int n) { return n % 2 == 0; };

        log("hello");
        Console.WriteLine($"Func add(10, 20) = {add(10, 20)}");

        var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
        foreach (int n in numbers)
            if (is_even(n))
                Console.WriteLine($"Predicate true: {n}");

        // Typical mistake: signature mismatch
        // result = DSMO_v2((a) => a + 1, x, y); // Does not compile: expected (int,int) => int
    }
}

// Microsoft Learn
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions
// https://learn.microsoft.com/en-us/dotnet/api/system.delegate.getinvocationlist
// https://learn.microsoft.com/en-us/dotnet/api/system.func-3
// https://learn.microsoft.com/en-us/dotnet/api/system.action-1
// https://learn.microsoft.com/en-us/dotnet/api/system.predicate-1
