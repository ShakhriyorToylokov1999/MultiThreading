/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static readonly Semaphore semaphore = new Semaphore(0, 10);

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine("\n Enter a number to start decrement: ");
            int inputNumber;
            int.TryParse(Console.ReadLine(), out inputNumber);

            Console.WriteLine("Task a: Using Thread class and Join: ");
            Thread thread = new Thread(() => DecrementAndPrint(inputNumber));
            thread.Start();
            thread.Join();

            Console.WriteLine("\n Task b: Using ThreadPool and Semaphore:");
            ThreadPool.QueueUserWorkItem(new WaitCallback(DecrementAndPrintWithSemaphore), inputNumber);
            semaphore.WaitOne();

            Console.ReadLine();
        }


        private static void DecrementAndPrint(int state)
        {
            Console.WriteLine($"{state} -  Thread Id {Thread.CurrentThread.ManagedThreadId} ");
            state--;

            if (state > 0)
            {
                Thread thread = new Thread(() => DecrementAndPrint(state));
                thread.Start();
                thread.Join();
            }
        }

        private static void DecrementAndPrintWithSemaphore(object state)
        {
            int value = (int)state;
            Console.WriteLine($"{value} - Thread Id {Thread.CurrentThread.ManagedThreadId}");
            value--;

            if (value > 0)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DecrementAndPrintWithSemaphore), value);
            }
            else
            {
                semaphore.Release();
            }
        }

    }
}
