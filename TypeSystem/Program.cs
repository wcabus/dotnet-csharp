using System;

namespace TypeSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var pointRef = new Point();
            var pointRef2 = pointRef;

            pointRef.X = 5;
            pointRef2.Y = 2;

            Console.WriteLine($"Point 1: {pointRef}, Point 2: {pointRef2}");

            var pointVal = new Point2D();
            var pointVal2 = pointVal;

            pointVal.X = 5;
            pointVal2.Y = 2;

            Console.WriteLine($"Point 1: {pointVal}, Point 2: {pointVal2}");
        }
    }
}
