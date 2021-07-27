using System;

namespace ConsoleTutorial
{
    partial class Program
    {
        public class WaterBill : IBilling
        {
            public void M1()
            {
                Console.WriteLine("Method one");
            }

            public void M2()
            {
                Console.WriteLine("Method three");
            }
        }
    }
}
