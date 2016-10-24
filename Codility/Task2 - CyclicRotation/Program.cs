using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2___CyclicRotation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var array = new[]
            {
                3, 8, 9, 7, 6
            };

            var a = Solution(array, 3);
        }

        public static int[] Solution(int[] array, int times)
        {
            for (var i = 0; i < times; i++)
            {
                array = MoveOne(array);
            }
            return array;
        }

        public static int[] MoveOne(int[] array)
        {
            var result = new int[array.Length];

            // Insert last one to the first place
            result[0] = array.Last();

            // Go over all the others
            for (var j = 1; j < array.Length; j++)
            {
                result[j] = array[j - 1];
            }

            return result;
        }
    }
}
