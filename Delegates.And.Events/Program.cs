using System;

namespace Delegates.And.Events
{
    class Program
    {
        delegate int PerformCalculation(int x, int y);

        public static event EventHandler CustomEvent;

        static void Main(string[] args)
        {
            PerformCalculation operations = Add; // Assign named method to a delegate

            // Add an anonymous method to the delegate
            operations += delegate(int x, int y)
            {
                Console.WriteLine($"Anonymous delegate has been called with x = {x} and y = {y}");
                return x * y;
            };

            // Add a lambda expression
            operations += (x, y) =>
            {
                Console.WriteLine($"Lambda expression has been called with x = {x} and y = {y}");
                CustomEvent?.Invoke(null, EventArgs.Empty); // used ?. to make this line thread-safe.
                return x - y;
            };

            CustomEvent += (s, e) => { Console.WriteLine("CustomEvent raised and handled"); };

            Console.WriteLine(operations(3, 5)); // What's the output?
        }

        // Named method
        static int Add(int x, int y)
        {
            Console.WriteLine($"Add has been called with x = {x} and y = {y}");
            return x + y;
        }
    }
}
