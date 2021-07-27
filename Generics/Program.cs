using System;
using System.Collections.Generic;
using ConsoleTutorial.HospitalModels;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
        
            Console.ReadLine();
        }

        static void UseStack()
        {
            var books = new Stack<int>();
            books.Push(1);
            books.Push(2);
            books.Push(3);
            Console.WriteLine(books.Pop());
            Console.WriteLine(books.Peek());
        }

        static void UseQueue()
        {
            var cities = new Queue<string>();
            cities.Enqueue("Bridgeport");
            cities.Enqueue("New Haven");
            cities.Enqueue("Stamford");
            Console.WriteLine(cities.Dequeue());
            Console.WriteLine(cities.Peek() );
        }

        static void UseList()
        {
            var countries = new List<string>();
            countries.Add("USA");
            countries.Add("Italy");
            countries.Add("Japan");

            foreach (var country in countries)
                Console.WriteLine(country);
        }

        static void UseDictionary()
        {
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(10, "Mary");
            dictionary.Add(2, "Peter");
            dictionary.Add(3, "Samuel");
            
            foreach (var item in dictionary)
                Console.WriteLine($"{item.Key}: {item.Value}");
        }

        static void UseListHospital()
        {
            var hospitals = new List<Hospital>();
            hospitals.Add(new Hospital { Id = 1, Name = "Rex", Specialization = "Orthopdics" });
            hospitals.Add(new Hospital { Id = 2, Name = "Mayo Clinic", Specialization = "Pediatrics" });
            foreach (var hospital in hospitals)
                Console.WriteLine($"{hospital.Id} {hospital.Name}");
        }

        static void UseDictionaryPatient()
        {
            var patientDictionary = new Dictionary<int, Patient>();
            patientDictionary.Add(1, new Patient { Id = 1, Name = "Elizabeth", Mobile = "123456789" });
            patientDictionary.Add(2, new Patient { Id = 2, Name = "Thomas", Mobile = "123456789" });
            patientDictionary.Add(3, new Patient { Id = 3, Name = "Carla", Mobile = "123456789" });

            foreach (var patient in patientDictionary)
                Console.WriteLine($"{patient.Key}: {patient.Value.Name}");
        }

        static void UseGeneric()
        {
            var myGen = new MyGenericClass<string>();
            myGen.M1("string");
        }
    }
}
