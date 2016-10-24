using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task3___TapeEquilibrium
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var array = new int[]
            {
                //-1000, 1000
                3, 1, 2, 4, 3
            };

            if (Solution(array) == 1)
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
        }

        public static int Solution(int[] array)
        {
            if (array.Length == 1)
            {
                return array[0];
            }
            if (array.Length == 2)
            {
                return Math.Abs(array[0] - array[1]);
            }

            var sumBefore = 0;
            var sumAfter = array.Sum();
            var minimum = int.MaxValue;

            for (var i = 0; i < array.Length - 1; i++)
            {
                sumBefore += array[i];
                //var sumAfter = array.Skip(i + 1).Sum();
                sumAfter -= array[i];

                var difference = Math.Abs(sumBefore - sumAfter);

                if (difference < minimum)
                {
                    minimum = difference;
                }
            }
            return minimum;
        }
    }
}
