using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            UseThread();
            Console.ReadLine();
        }

        static void UseThread()
        {
            var t1 = new Thread(Even);
            var t2 = new Thread(Odd);

            t1.Start();

            // Continue to next thread only after t1 ends
            // t1.Join();

            Console.WriteLine("sadsa");

            // Stop the thread for 5 seconds
            // Thread.Sleep(5000);

            t2.Start();
        }

        static void Even()
        {
            for (int i = 0; i < 100; i += 2)
                Console.WriteLine(i);
        }

        static void Odd()
        {
            for (int i = 1; i < 100; i += 2)
                Console.WriteLine(i);
        }
    }
}
