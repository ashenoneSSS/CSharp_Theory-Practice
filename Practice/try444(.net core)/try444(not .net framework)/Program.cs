using System;
using System.Threading.Tasks;

public class Program
{
    static async Task demo_delay()
    {
        Console.WriteLine("start");
        await Task.Delay(2000);
        Console.WriteLine("after 2 sec");
    }

    public static void Main()
    {
        demo_delay().GetAwaiter().GetResult();
    }
}