namespace Generics
{
    public class MyGenericClass<T>
    {
        public T SomePropert { get; set; }

        public void M1(T value) 
        {
            System.Console.WriteLine(value);
        }
    }
}
