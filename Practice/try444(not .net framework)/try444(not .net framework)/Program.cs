Foo foo = new();
Bar bar = new();

public struct Foo
{
    static Foo()
    {
        Console.WriteLine("static Foo");
    }

    public Foo()
    {
        Console.WriteLine("Foo");
    }
}

public struct Bar
{
    static Bar()
    {
        Console.WriteLine("static Bar");
    }
}
