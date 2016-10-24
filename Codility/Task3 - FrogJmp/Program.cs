using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task3___FrogJmp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (Solution(10, 85, 30) == 3 && Solution(1, 5, 2) == 2)
            {
                Console.WriteLine("Test passed");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
        }

        public static int Solution(int X, int Y, int D)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            return (int) Math.Ceiling(((double)Y - (double)X) / D);
        }
    }
}
