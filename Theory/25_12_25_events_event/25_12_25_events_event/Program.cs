using System;

// =======================
// Events in C#
// =======================
//
// Event
// An event is a member based on a delegate type
// It stores an invocation list (multicast) like a delegate
//
// Key difference from a public delegate field
// With event, outside code can only subscribe/unsubscribe using += and -=
// Outside code cannot invoke the event
// Outside code cannot overwrite the invocation list with '='
//
// Declaring an event
// public event ParamLess empty
// public event Action<int> quantity_changed
// public event Func<int, bool> reorder_check
//
// Raising an event inside the declaring type
// empty?.Invoke()
// quantity_changed?.Invoke(value)
// bool decision = reorder_check?.Invoke(quantity) ?? false
//
// Typical usage
// Publisher raises the event when something happens
// Subscribers attach methods (handlers) to react
//
// Common mistakes
// Exposing a delegate field instead of an event breaks encapsulation
// Modifying the collection while enumerating (not about events, but common with handlers touching shared state)
// Forgetting to unsubscribe in long-lived publishers can keep subscribers alive

public delegate void ParamLess();

public sealed class stock
{
    private int quantity;

    public int Quantity => quantity;
    public string LastStatus { get; private set; } = "";
    public bool LastReorderDecision { get; private set; }

    public stock(int quantity)
    {
        if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        this.quantity = quantity;
    }

    public event ParamLess empty;
    public event Action<string> status_changed;
    public event Func<int, bool> reorder_check;

    public void take(int count)
    {
        if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

        int before = quantity;

        quantity -= count;
        if (quantity < 0) quantity = 0;

        // Example: assignment inside Invoke argument
        // LastStatus is assigned and the assigned value is passed to subscribers
        status_changed?.Invoke(LastStatus = $"take({count})  {before} -> {quantity}");

        // Raise empty when stock becomes empty (or already empty)
        if (quantity == 0)
            empty?.Invoke();

        // Example: assigning the Invoke result
        // For multicast Func<int,bool>, only the last handler's return value is observed
        LastReorderDecision = reorder_check?.Invoke(quantity) ?? false;
    }

    // Typical mistake from the original snippet (kept as a commented example)
    // It can make quantity negative because it sets quantity to 0 and then subtracts again
    // public void take_buggy(int count)
    // {
    //     if ((quantity - count) < 0)
    //     {
    //         quantity = 0;
    //         empty?.Invoke();
    //     }
    //     quantity -= count; // quantity can become negative here
    // }
}

public static class Program
{
    private static void UI_message()
    {
        Console.WriteLine("OUT OF STOCK");
    }

    private static void Email_message()
    {
        Console.WriteLine("send reorder request");
    }

    private static void Log_status(string message)
    {
        Console.WriteLine($"STATUS: {message}");
    }

    private static bool Reorder_policy_A(int qty)
    {
        Console.WriteLine($"Reorder_policy_A called with qty={qty}");
        return true;
    }

    private static bool Reorder_policy_B(int qty)
    {
        Console.WriteLine($"Reorder_policy_B called with qty={qty}");
        return false;
    }

    public static void Main(string[] args)
    {
        var Sclad = new stock(100);

        Sclad.empty += UI_message;
        Sclad.empty += Email_message;

        Sclad.status_changed += Log_status;

        // Multicast Func event: both handlers run, but the last return value is the observed one
        Sclad.reorder_check += Reorder_policy_A;
        Sclad.reorder_check += Reorder_policy_B;

        // You can unsubscribe like with delegates
        Sclad.empty -= UI_message;

        // Typical mistake: outside code cannot invoke an event
        // Sclad.empty?.Invoke(); // Does not compile: event can only appear on left side of += or -=

        Console.WriteLine("take 40");
        Sclad.take(40);
        Console.WriteLine($"Quantity={Sclad.Quantity}  LastStatus='{Sclad.LastStatus}'  LastReorderDecision={Sclad.LastReorderDecision}");

        Console.WriteLine();
        Console.WriteLine("take 40");
        Sclad.take(40);
        Console.WriteLine($"Quantity={Sclad.Quantity}  LastStatus='{Sclad.LastStatus}'  LastReorderDecision={Sclad.LastReorderDecision}");

        Console.WriteLine();
        Console.WriteLine("take 40");
        Sclad.take(40);
        Console.WriteLine($"Quantity={Sclad.Quantity}  LastStatus='{Sclad.LastStatus}'  LastReorderDecision={Sclad.LastReorderDecision}");

        Console.WriteLine();
        Console.WriteLine("take 40");
        Sclad.take(40);
        Console.WriteLine($"Quantity={Sclad.Quantity}  LastStatus='{Sclad.LastStatus}'  LastReorderDecision={Sclad.LastReorderDecision}");

        // Typical mistake: overwriting the whole invocation list is not allowed with events
        // Sclad.empty = Email_message; // Does not compile

        // Typical note: if empty was a public delegate field (not an event), this would compile and allow external invocation
        // public ParamLess empty; // Bad: outside code could do Sclad.empty = null or Sclad.empty()
    }
}

// Microsoft Learn
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
// https://learn.microsoft.com/en-us/dotnet/standard/events/
// https://learn.microsoft.com/en-us/dotnet/api/system.action-1
// https://learn.microsoft.com/en-us/dotnet/api/system.func-2
