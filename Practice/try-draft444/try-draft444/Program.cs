using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Before try");

        try
        {
            Console.WriteLine("Inside try, before throw");
            throw new Exception("Boom! This is my exception.");
            Console.WriteLine("Inside try, after throw");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Caught exception:");
            Console.WriteLine(ex.GetType().Name);
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("After try/catch");
    }
}
