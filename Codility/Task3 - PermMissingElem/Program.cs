using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task3___PermMissingElem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var array = new[]
            {
                2, 3, 1, 5
            };

            if (Solution(array) == 4)
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
            // The length of the array should be one larger than the actual one
            long arrayLength = array.Length + 1;
            ulong sum = ((ulong)arrayLength * (ulong)(arrayLength + 1))/2;

            ulong actual = 0;
            foreach (var i in array)
            {
                actual += (ulong)i;
            }
            ulong missing = sum - actual;

            return (int)missing;
        }
    }
}
