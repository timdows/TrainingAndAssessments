using System;
using System.Collections;
using System.Linq;

namespace Task1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (solution(1041) == 5 && solution(15) == 0)
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
        }

        public static int solution(int number)
        {
            // Create a bit array
            var bitArray = new BitArray(new[] {number});
            // Cast it to an int array that is reversed
            var bits = bitArray.Cast<bool>().Select(bit => bit ? 1 : 0).ToArray().Reverse();

            var maximalGap = 0;
            var runningGap = 0;
            var started = false;

            foreach (var bit in bits)
            {
                if (started && bit == 0)
                {
                    runningGap++;
                }

                if (bit == 1)
                {
                    // Only start if iterated over al leading zeros
                    started = true;

                    // Check if the last gap is larger than the previous
                    if (runningGap > maximalGap)
                    {
                        maximalGap = runningGap;
                    }

                    runningGap = 0;
                }
            }

            return maximalGap;
        }
    }
}
