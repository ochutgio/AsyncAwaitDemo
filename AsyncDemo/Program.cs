using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncDemo
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Start 3 tasks use await still block behind code");
            await DoSomethingAsync();
            await DoSomething2Async();
            await DoSomething3Async();
            Console.WriteLine("Total time for 3 tasks: 5s + 1s + 3s = 9s");

            Console.WriteLine("");
            Console.WriteLine("Demo Async 3 tasks");
            var task1 = DoSomethingAsync();
            Console.WriteLine("Task 1 is running - 5s");
            var task2 = DoSomething2Async();
            Console.WriteLine("Task 2 is running - 1s");
            var task3 = DoSomething3Async();
            Console.WriteLine("task 3 is running - 3s");
            
            var resultTasks = new List<Task> { task1, task2, task3 };
            while (resultTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(resultTasks);
                if (finishedTask == task1)
                {
                    Console.WriteLine(task1.Result);
                }
                else if (finishedTask == task2)
                {
                    Console.WriteLine(task2.Result);
                }
                else
                {
                    Console.WriteLine(task3.Result);
                }
                resultTasks.Remove(finishedTask);
            }

            await Task.WhenAll(task1, task2, task3);
            Console.WriteLine("Total time for 3 tasks: 5s");

            Console.ReadLine();

        }

        public static async Task<int> DoSomethingAsync()
        {
            await Task.Delay(5000); // simulate job

            Console.WriteLine("DoSomethingAsync is done");

            return 1;
        }

        public static async Task<int> DoSomething2Async()
        {
            await Task.Delay(1000); // simulate job

            Console.WriteLine("DoSomething2Async is done");

            return 2;
        }

        public static async Task<int> DoSomething3Async()
        {
            await Task.Delay(3000); // simulate job

            Console.WriteLine("DoSomething3Async is done");

            return 3;
        }
    }
}
