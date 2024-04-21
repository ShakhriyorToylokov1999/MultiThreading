/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code
            Task<int[]> task1 = Task.Run(() =>
            {
                Random random = new Random();
                int[] initialArray = new int[10];

                for (int i = 0; i < initialArray.Length; i++)
                {
                    initialArray[i] = random.Next(1,100);
                }

                Console.WriteLine("Task 1: Initial array ");
                PrintArray(initialArray);
                
                return initialArray;

            });
            
            Task<int[]> task2 = task1.ContinueWith(x =>
            {

                Random random = new Random();
                int randomNumber = random.Next(1,20);

                int[] resultArray = new int[x.Result.Length];

                for (int i = 0; i < x.Result.Length; i++)
                {
                    resultArray[i] = x.Result[i] * randomNumber;
                }

                Console.WriteLine("Task 2: Multiplied by random number {0} ", randomNumber);
                PrintArray(resultArray);
               
                return resultArray;
            });

            Task<int[]> task3 = task2.ContinueWith(x =>
            {
               Array.Sort(x.Result);

               Console.WriteLine("Task 3: Sorted array ");
               PrintArray(x.Result);
                
               return x.Result;
            
            });
            
            Task<double> task4 = task3.ContinueWith(x =>
            {
                int sum = 0;
                
                for(int i = 0; i < x.Result.Length; i++)
                {
                    sum+=x.Result[i];
                }

                double average = sum / x.Result.Length;
                Console.WriteLine("Task 4: The average of the array is : {0}", average);
                
                return average;
            });
            
            Console.ReadLine();
        }

        private static void PrintArray(int[] arr)
        {
            Console.WriteLine("The numbers in the array: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write("{0} ", arr[i]);
            }
            Console.WriteLine();
        }
    }
}
