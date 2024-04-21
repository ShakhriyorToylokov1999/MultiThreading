/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    class Program
    {
        const int TaskAmount = 100;
        const int MaxIterationsCount = 1000;

        static void Main(string[] args)
        {   
            HundredTasks();

            Console.ReadLine();
        }

        static void HundredTasks()
        {
            Task[] tasks = new Task[100];
        
            for (int i = 1; i < tasks.Length; i++)
            {
                int taskNumber = i; 
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 1; j <= 1000; j++)
                    {
                        Output(taskNumber,j);
                    }
                });
                Task.WaitAny(tasks[i]);
            }

            Console.WriteLine("All tasks completed. Press any key to exit.");
            Console.ReadKey();
        }

        static void Output(int taskNumber, int iterationNumber)
        {
            Console.WriteLine($"Task #{taskNumber + 1 } – {iterationNumber}"); //tasknumber + 1 since array starts with 0 so I want to start printing from 1
        }
    }
}
