namespace ConsoleTutorial
{
    partial class Program
    {
        /// <summary>
        /// Used for all equations
        /// </summary>
        public class Equations
        {
            /// <summary>
            /// Add values
            /// </summary>
            public static int Add(params int[] numbers) 
            {
                var sum = 0;
                foreach (var number in numbers)
                    sum += number;

                return sum;
            }

            public static int[] Multiples(int number)
            {
                var square = number * number;
                var cube = number * number * number;
                var quadruple = number * number* number *number;

                return new int[] { square, cube, quadruple };
            }

            public static void Multiples(int number, out int square, out int cube, out int quadruple)
            {
                square = number * number;
                cube = number * number * number;
                quadruple = number * number * number * number;
            }

            public static void CircleArea(float radius, out float area)
            {
                area = 22 / 7 * radius * radius;
            }
        }
    }
}
