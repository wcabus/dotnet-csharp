using System;

namespace Exceptions
{
    class Program
    {
        static void Main()
        {
            try
            {
                SomethingOld();
                SomethingNew();
                SomethingBorrowed();
                SomethingBlue();
            }
            catch (Exception e) when (LogException(e))
            {
                // Because LogException returns false, we will never enter this block!
                // But it allows us to log every exception AND handle more specific exceptions at the same time
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Not implemented exception has been dealt with");
            }
            catch (CustomException)
            {
                // Need to catch this, because the "generic" exception handler is not being used!
            }
        }

        static void SomethingOld()
        {

        }

        static void SomethingNew()
        {
            throw new NotImplementedException();
        }

        static void SomethingBorrowed()
        {
            throw new CustomException();
        }

        static void SomethingBlue()
        {

        }

        static bool LogException(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();

            return false;
        }
    }
}
