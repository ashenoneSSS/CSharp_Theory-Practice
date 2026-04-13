using System;
using System.Threading.Tasks;

public class Program
{
    // async method
    // "async" allows using "await" inside the method
    // The method returns Task, which represents the ongoing operation that will complete in the future
    static async Task Operation(string TaskName, int Duration)
    {
        // This line runs immediately when Operation(...) is called
        Console.WriteLine($"\t{TaskName} Started\n");

        // await does NOT block the current thread
        // It splits the method into two parts
        // Part 1 runs until the first await
        // Then the method returns a Task to the caller, and the rest is scheduled to continue later
        //
        // Task.Delay(Duration) creates a timer-based task
        // When the timer finishes, the continuation of this method is queued to run
        await Task.Delay(Duration);

        // This line runs AFTER the delay completes (in a continuation)
        // In a console app, there is usually no synchronization context
        // So the continuation typically runs on a thread pool thread
        Console.WriteLine($"\t{TaskName} Completed\n");
    }



    // async Main is allowed
    // The runtime will wait for the returned Task to complete before the process exits
    public async static Task Main()
    {
        // Calling an async method starts executing it immediately up to its first await
        // So both operations begin right away and run concurrently (overlap in time)
        //
        // After each Operation hits "await Task.Delay", it returns a Task to Main
        // task1/task2 are handles you can await later
        Task task1 = Operation("Task1", 4000);
        Task task2 = Operation("Task2", 2000);

        // This loop runs on the Main thread
        // Thread.Sleep blocks the current thread for 500ms each iteration
        //
        // Important
        // Thread.Sleep is a blocking call (it does not cooperate with async)
        // The work inside Operation can still continue because Task.Delay uses timers and continuations
        // Those continuations can run on thread pool threads even while Main thread is blocked
        //
        // Note
        // This code needs "using System.Threading;" to compile because of Thread.Sleep
        // Or you would write System.Threading.Thread.Sleep(...)
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(500);
            Console.WriteLine($"Iteration {i}\n");
        }

        // await here means
        // If task1 is not finished yet, Main will asynchronously wait for it
        // If task1 is already finished, await continues immediately
        await task1;
        await task2;

        // At this point both operations are completed
        // Main returns and the process can exit
    }
}