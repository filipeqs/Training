using System;

namespace ConsoleTutorial.HospitalModels
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Location { get; set; }
        public string Specialization { get; set; }

        public virtual void Discount()
        {
            Console.WriteLine("No discount!");
        }
    }

    public class NursingHome : Hospital
    {
        public string Pharmacy { get; set; }
    }

    public class Clinic : NursingHome
    {
        public override void Discount()
        {
            Console.WriteLine("Especial Discount");
        }
    }
}
