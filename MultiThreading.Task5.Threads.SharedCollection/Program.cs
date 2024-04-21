/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static List<int> collection = new List<int>();
        static object lockObject = new object();

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            Console.WriteLine("The array elements are being added: ");
            Task task1 = Task.Factory.StartNew(() => Add());
            Task task2 = Task.Factory.StartNew(() => Print());

            Task.WaitAll(task1, task2);

            Console.WriteLine("Array has been completed ");
            Console.ReadLine();
        }

        static void Add()
        {
            for (int i = 1; i <= 10; i++)
            {
                lock (lockObject)
                {
                    collection.Add(i);
                    Monitor.Pulse(lockObject); 
                }
                Thread.Sleep(100);
            }
        }

        static void Print()
        {
            lock (lockObject)
            {
                while (collection.Count < 10)
                {
                    Monitor.Wait(lockObject);
                }
            }

            for (int i = 1; i <= 10; i++)
            {
                lock (lockObject)
                {
                    Console.WriteLine($"[{string.Join(", ", collection.GetRange(0, i))}]");
                }
                Thread.Sleep(100);
            }
        }

    }
}
