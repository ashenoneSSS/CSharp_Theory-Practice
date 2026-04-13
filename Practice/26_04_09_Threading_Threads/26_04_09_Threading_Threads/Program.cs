using System;
using System.Threading.Tasks;

namespace _26_04_09_Threading_Threads
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Start");

            await Task.Delay(3000);

            Console.WriteLine("End");
        }
    }
}

/*
            int counter = 0;
            object locker = new object();

            Thread t1 = new Thread(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    lock(locker)
                    {
                        counter++;
                    }
                }
            });

            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    lock (locker)
                    {
                        counter++;
                    }
                }
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine(counter);
 */