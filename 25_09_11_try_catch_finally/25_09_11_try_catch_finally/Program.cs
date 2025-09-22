using System;

namespace _25_09_11_try_catch_finally
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ===========================
            // Syntax of try-catch-finally
            // ===========================
            //
            // try
            // {
            //     // code that may throw exception
            // }
            // catch (SpecificException ex)
            // {
            //     // handle specific exception
            // }
            // catch (Exception ex)
            // {
            //     // handle any other exception
            // }
            // finally
            // {
            //     // code that always runs (cleanup, close files, etc.)
            // }
            


            // The code inside "try" starts executing line by line.
            //
            // If no error occurs → the whole block executes until the end, then:
            // - if there is a catch → it is skipped,
            // - if there is a finally → it will run.
            //
            // If an exception happens inside "try" → execution immediately stops on that line.
            // Then a matching "catch" is searched. If a suitable catch exists, control goes there.
            //
            // If no catch exists but there is a finally → finally will still run,
            // and after that the program will crash with the error.
            //
            // If both catch and finally exist → catch executes first, then finally always runs.



            // ===========================
            // Example 1: simple try/catch/finally
            // ===========================
            int a = 6;
            int[] b = new int[3] { 3, 2, 0 };

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    Console.WriteLine($"b = {b[i]}");
                    int c = a / b[i];          // risky operation → can throw DivideByZeroException
                    Console.WriteLine($"{a} / {b[i]} = {c}");
                }
                catch
                {
                    Console.WriteLine("\nu stupid"); // runs only if error occurs
                }
                finally
                {
                    Console.WriteLine("finally block\n"); // always runs
                }
            }

            // ===========================
            // Example 2: calculator with multiple catch blocks
            // ===========================
            bool correctFinish = false;
            while (true)
            {
                if (correctFinish == false)
                {
                    try
                    {
                        Console.Write("Enter first number: ");
                        int n1 = int.Parse(Console.ReadLine()); // can throw FormatException

                        Console.Write("Enter second number: ");
                        int n2 = int.Parse(Console.ReadLine()); // can throw FormatException

                        Console.Write("Enter operation (+ - * /): ");
                        string op = Console.ReadLine();

                        int result;

                        switch (op)
                        {
                            case "+":
                                result = n1 + n2;
                                Console.WriteLine($"Result: {result}");
                                correctFinish = true;
                                break;
                            case "-":
                                result = n1 - n2;
                                Console.WriteLine($"Result: {result}");
                                correctFinish = true;
                                break;
                            case "*":
                                result = n1 * n2;
                                Console.WriteLine($"Result: {result}");
                                correctFinish = true;
                                break;
                            case "/":
                                result = n1 / n2; // can throw DivideByZeroException
                                Console.WriteLine($"Result: {result}");
                                correctFinish = true;
                                break;
                            default:
                                throw new InvalidOperationException("Wrong operation"); // custom exception
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Input is not a number: " + ex.Message);
                    }
                    catch (DivideByZeroException ex)
                    {
                        Console.WriteLine("Division by zero: " + ex.Message);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine("Invalid operation: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Other error: " + ex.Message);
                    }
                    finally
                    {
                        Console.WriteLine("finally always runs here too\n");
                    }
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Congrats!");
            Console.ReadLine();
        }
    }
}
