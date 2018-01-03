using System;
using System.Linq;

namespace TypeSystem
{
    class Program
    {
        [Test]
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
            
            // This next line will throw an exception at run-time
            // ReadAttributes();
        }

        static void ReadAttributes()
        {
            var attribute = typeof(Program).GetMethod(nameof(Main)).GetCustomAttributes(typeof(TestAttribute), false)
                .OfType<TestAttribute>().FirstOrDefault();
        }
    }

    public class TestAttribute : Attribute
    {
        public TestAttribute()
        {
            throw new NotImplementedException();
        }
    }

    public interface ITest
    {
        void Add(int data);
    }

    public class ImplicitTest : ITest
    {
        public void Add(int data) { }
    }

    public class ExplicitTest : ITest
    {
        void ITest.Add(int data) { }
    }

    public class InterfaceDifference
    {
        public void Test()
        {
            var implicitInstance = new ImplicitTest();
            var explicitInstance = new ExplicitTest();

            implicitInstance.Add(1);
            ((ITest)explicitInstance).Add(2);
        }
    }
}
