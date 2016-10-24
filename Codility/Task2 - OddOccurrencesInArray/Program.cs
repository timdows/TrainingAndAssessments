using System;
using System.Linq;

namespace Task2___OddOccurrencesInArray
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var array = new[]
            {
                9,
                3,
                9,
                3,
                9,
                7,
                9
            };

            Console.WriteLine(Solution(array) == 7 ? "Test passed" : "Test failed");
        }

        public static int Solution(int[] array)
        {
            if (array.Length == 0)
            {
                return 0;
            }

            return array.FirstOrDefault(i => array.Count(a_item => a_item == i) == 1);
        }
    }
}
