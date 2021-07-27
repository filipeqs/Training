using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ConsoleTutorial.HospitalModels;

namespace ConsoleTutorial
{
    partial class Program
    {
        static void Main(string[] args)
        {
            UseDelegate();
            Console.ReadLine();
        }

        static void UseTypes()
        {
            // Primitive type
            // Integer types
            // Byte, Int16/short, Int32/int, Int64/long
            // float, double, decimal
            // boolean

            // 8 bit - 00000000 - 2^8
            // 0 to 255
            byte byteNumber = 255;
            // -128 - 127
            sbyte sbyteNumber = 127;

            // Floating Types
            // float, double, decimal
            float flo = 10.5f;
            double doub = 10000.4;
            decimal dec = 2000000.323m;

            // Verbatim literal
            string path = @"c:\training\videos\new";
        }

        static void UseStringConcat()
        {
            // Concatination
            var firstName = "Filipe";
            var lastName = "Silva";
            Console.WriteLine("Hi " + firstName + " " + lastName + " welcome to the class!");
            Console.WriteLine("HI {0} {1} welcome to the class!", firstName, lastName);
            Console.WriteLine($"HI {firstName} {lastName} welcome to the class!");

            Console.WriteLine("Hello World");
        }

        static void UseContains()
        {
            var fileContent = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. 
                Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
                when an unknown printer took a galley of type and scrambled it to make a type specimen book. 
                It has survived not only five centuries, but also the leap into electronic typesetting, remaining 
                essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing 
                Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including 
                versions of Lorem Ipsum";

            var wordSearch = "industry";
            var contains = fileContent.Contains(wordSearch);
            if (contains)
                Console.WriteLine($"There a mention about {wordSearch}");
            else
                Console.WriteLine($"There is no mention about {wordSearch}");
        }

        static void UseSplit()
        {
            var price = 100.00f;
            Console.WriteLine(string.Format("{0:c}", price));

            var value = "Tie,Boot,Belt";
            var items = value.Split(',');
            foreach (var item in items)
                Console.WriteLine(item);
        }

        static void UseSplitExtension()
        {
            var value = "Test,One,Two";
            var items = value.SplitString(',');
            foreach (var item in items)
                Console.WriteLine(item);
        }

        static void UseTrimEnd()
        {
            var message = string.Empty;
            for (int i = 0; i < 5; i++)
                message += i + ",";

            message = message.TrimEnd(',');
            message = message.Replace(",", "-");
            Console.WriteLine(message);
        }

        static void UseStringBuilder()
        {
            var message = new StringBuilder();
            for (int i = 0; i < 5; i++)
                message.Append(i + ",");

            Console.WriteLine(message);
        }

        static void AcceptNullValue()
        {
            int? totalTickets = null;

            var availableTickets = totalTickets ?? 0;

            Console.WriteLine(availableTickets);
        }

        static void UseTryParse()
        {
            int age;
            var isNumber = false;

            Console.WriteLine("Enter your age");

            do
            {
                var input = Console.ReadLine();

                isNumber = int.TryParse(input, out age);

                if (!isNumber)
                    Console.WriteLine("Enter a number");
            } while (!isNumber);


            Console.WriteLine($"Your age is {age}");
        }

        static void UseArrayList()
        {
            var car1 = new Car() { Color = "blue", ModelNo = 1 };
            var car2 = new Car() { Color = "black", ModelNo = 2 };

            var cars = new ArrayList();
            cars.Add(car1);
            cars.Add(car2);

            foreach (var car in cars)
            {
                var currentCar = (Car)car;
                Console.WriteLine(currentCar.Color);
            }
        }

        static void UseMethods()
        {
            var multiplesTen = Equations.Multiples(10);
            foreach (var multiple in multiplesTen)
                Console.WriteLine(multiple);

            Equations.Multiples(10, out int square, out int cube, out int quadruple);
            Console.WriteLine($"Square: {square}");
            Console.WriteLine($"Cube: {cube}");
            Console.WriteLine($"Quadruple: {quadruple}");

            float radius = 2;
            Equations.CircleArea(radius, out float area);
            Console.WriteLine($"Area of the circle {area}");

            var sum = Equations.Add(1, 2, 3);
            Console.WriteLine($"The sum is: {sum}");
        }

        static void UsePolymorphism()
        {
            Hospital hospital = new Clinic(); // Upcasting
            hospital.Discount(); // Polymorphism
            hospital = new NursingHome();
            hospital.Discount();

            List<Hospital> hospitals = new List<Hospital>();
            hospitals.Add(new Clinic());
            hospitals.Add(new NursingHome());

            hospitals[0].Discount();
            hospitals[1].Discount();
        }

        public delegate void Mydelegate();
        static void UseDelegate()
        {
            var d = new Mydelegate(Add);
            d += Message;
            
            d.Invoke();
        }

        static void Add()
        {
            Console.WriteLine("Add");
        }

        public static void Message()
        {
            Console.WriteLine("Delegate work");
        }
    }
}
