/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            Task parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task");
                throw new Exception("Simulating exception foe Parent task have a failure");
            });

            // Task a)
            Task task1 = parentTask.ContinueWith((task) =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Continuation task 1: Executed regardless of the result of the parent task");
                
            });

            // Task b) 
            Task task2 = parentTask.ContinueWith((task) =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Continuation task 2: Executed when the parent task was finished without success");
             
            }, TaskContinuationOptions.OnlyOnFaulted);

            // Task c)
            Task task3 = parentTask.ContinueWith((task) =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Continuation task 3: Parent task thread has been reused for continuation: ");
            }, TaskContinuationOptions.AttachedToParent);

            // Task d)
            Task task4 = parentTask.ContinueWith((task) =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Continuation task 4: Executed outside of the thread pool when the parent task is cancelled");
            }, TaskContinuationOptions.LongRunning | TaskContinuationOptions.NotOnCanceled);

            Task.WaitAll(task1, task2, task3, task4);

            Console.WriteLine("Tasks have been completed");

            Console.ReadLine();
        }

    }
}
