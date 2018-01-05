using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ThreadingSample
{
    class Program
    {
        private static bool _stopRunning;

        static void Main()
        {
            Console.WriteLine("Press any key to start running the sample...");
            Console.ReadKey(true);

            try
            {
                // ReadNumbersAndAddThem();
                // ForegroundBlockingThread();
                // BackgroundBlockingThread();
                // ReadNumbersAndComputeThem();
            }
            finally
            {
                // When you run this app with CTRL+F5 (start without debugging), it pauses at the end.
                // The normal behavior though, is to run and exit immediately, so I added these lines when the debugger is attached.
                if (Debugger.IsAttached)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
            }
        }

        static void ReadNumbersAndAddThem()
        {
            Console.WriteLine("Keep entering numbers and confirm them with <ENTER> to add them.");
            Console.WriteLine("To stop, press <ENTER> on an empty line.");

            var queue = new Queue<int>();
            string input;

            var adderThread = new Thread(NumberAdderThread)
            {
                Name = "Adds numbers",
                IsBackground = true // Default = false when manually creating new threads this way
            };
                
            adderThread.Start(queue);

            while (!string.IsNullOrWhiteSpace(input = Console.ReadLine()))
            {
                if (int.TryParse(input, out var number))
                {
                    queue.Enqueue(number);
                }
            }

            _stopRunning = true;
        }

        static void NumberAdderThread(object state)
        {
            if (!(state is Queue<int> queue))
            {
                return; 
            }

            var queueCount = 0;
            while (!_stopRunning)
            {
                var currentQueueCount = queue.Count;
                if (currentQueueCount > queueCount)
                {
                    queueCount = currentQueueCount;

                    var total = queue.Sum();
                    Console.WriteLine($"Total of all entered numbers: {total}");
                }

                Thread.Sleep(1000);
            }
        }

        static void NumberSubtractorThread(object state)
        {
            if (!(state is Queue<int> queue))
            {
                return;
            }

            var queueCount = 0;
            while (!_stopRunning)
            {
                var currentQueueCount = queue.Count;
                if (currentQueueCount > queueCount)
                {
                    queueCount = currentQueueCount;

                    var total = queue.Peek() * 2;
                    total -= queue.Sum();

                    Console.WriteLine($"First - sum of all other entered numbers: {total}");
                }

                Thread.Sleep(1000);
            }
        }

        static void ForegroundBlockingThread()
        {
            new Thread(() =>
                {
                    while (!_stopRunning)
                    {
                        Console.WriteLine("Thread is still running");
                        Thread.Sleep(1000);
                    }
                })
                {
                    // IsBackground = false // A foreground thread
                }
                .Start();
        }

        static void BackgroundBlockingThread()
        {
            new Thread(() =>
                {
                    while (!_stopRunning)
                    {
                        Console.WriteLine("Thread is still running");
                        Thread.Sleep(1000);
                    }
                })
                {
                    IsBackground = true // A background thread
                }
                .Start();
        }

        static void ReadNumbersAndComputeThem()
        {
            Console.WriteLine("Keep entering numbers and confirm them with <ENTER> to perform computations with them.");
            Console.WriteLine("To stop, press <ENTER> on an empty line.");

            var queue = new Queue<int>();
            string input;

            ThreadPool.QueueUserWorkItem(NumberAdderThread, queue);
            ThreadPool.QueueUserWorkItem(NumberSubtractorThread, queue);

            while (!string.IsNullOrWhiteSpace(input = Console.ReadLine()))
            {
                if (int.TryParse(input, out var number))
                {
                    queue.Enqueue(number);
                }
            }

            _stopRunning = true;
        }
    }
}
