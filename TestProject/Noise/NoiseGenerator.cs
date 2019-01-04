using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Noise
{
    static class NoiseGenerator
    {
        static int noiseHeight = 1;
        static int noiseWidth = 1;
        static double[,] noise;
        static double[,] noiseSmooth;
        static double[,] noiseRe;

        static Random rng = new Random();
        static double sqrt = Math.Sqrt(2);
        // Edit these for more center-heavy results
        static double dividerDirect = .7 / sqrt;
        static double dividerDiagonal = .7 - dividerDirect;

        public static void Display(int nY, int nX)
        {
            noiseHeight = nY;
            noiseWidth = nX;
            noise = new double[noiseHeight, noiseWidth];
            noiseSmooth = new double[noiseHeight, noiseWidth];
            noiseRe = new double[noiseHeight, noiseWidth];
            string output = "";
            Generate();
            // Normal
            for (int y = 0; y < noiseHeight; y++)
            {
                for (int x = 0; x < noiseWidth; x++)
                {
                    Colorate(noise[y, x]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(output);
            Console.WriteLine();

            // Smoothened
            for (int y = 1; y < noiseHeight - 1; y++)
            {
                for (int x = 1; x < noiseWidth - 1; x++)
                {
                    SmoothNoise(y, x);
                    Colorate(noiseSmooth[y, x]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(output);
            Console.WriteLine();

            // SubNoised
            for (int y = 1; y < noiseHeight - 1; y++)
            {
                for (int x = 1; x < noiseWidth - 1; x++)
                {
                    ReNoise(noise, y, x);
                    Colorate(noiseRe[y, x]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(output);
            Console.WriteLine();

            // Both
            for (int y = 1; y < noiseHeight - 1; y++)
            {
                for (int x = 1; x < noiseWidth - 1; x++)
                {
                    ReNoise(noiseSmooth, y, x);
                    Colorate(noiseSmooth[y, x]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(output);
        }

        static void Generate()
        {
            for (int y = 0; y < noiseHeight; y++)
            {
                for (int x = 0; x < noiseWidth; x++)
                {
                    noise[y, x] = rng.NextDouble();
                    if (noise[y, x] > .8)
                    {
                        Console.WriteLine(noise[y, x]);
                    }
                }
            }
        }

        static void SmoothNoise(int y, int x)
        {
            if ((y % 2 == 0 && x % 2 == 0) || (y % 2 == 1 && x % 2 == 1))
            if (noise[y, x] > .85)
            {
                noiseSmooth[y, x] = noise[y, x];
                return;
            }
            double upperleft = noise[y - 1, x - 1] * (dividerDiagonal / 4);
            double upperright = noise[y + 1, x - 1] * (dividerDiagonal / 4);
            double lowerleft = noise[y - 1, x + 1] * (dividerDiagonal / 4);
            double lowerright = noise[y + 1, x + 1] * (dividerDiagonal / 4);

            double center = noise[y, x] * 0.7;

            double up = noise[y - 1, x] * (dividerDirect / 4);
            double left = noise[y, x - 1] * (dividerDirect / 4);
            double right = noise[y, x + 1] * (dividerDirect / 4);
            double down = noise[y + 1, x] * (dividerDirect / 4);

            noiseSmooth[y, x] = upperleft + upperright + lowerleft + lowerright + up + left + right + down;
        }

        static void ReNoise(double [,] array, int y, int x)
        {
            double n = array[y, x];
            double r = 0;
            if (n > .95)
            {
                r = (1 - n) * 75;
            }
            else if (n < .05)
            {
                r = n * 75;
            }

            int minY = 0 > y - r ? 0 : (int)Math.Floor(y - r);
            int minX = 0 > x - r ? 0 : (int)Math.Floor(x - r);
            int maxY = y + r > array.GetLength(0)? array.GetLength(0) : (int)Math.Ceiling(y + r);
            int maxX = x + r > array.GetLength(1)? array.GetLength(1) : (int)Math.Ceiling(x + r);

            for (int y1 = minY; y1 < maxY; y1++)
            {
                for (int x1 = minX; x1 < maxX; x1++)
                {
                    if (Math.Sqrt((y1 - y) * (y1 - y) + (x1 - x) * (x1 - x)) < r)
                    {
                        noiseRe[y1, x1] = (n * .80) + (array[y1, x1] * .20);
                    }
                }
            }
        }

        static void Colorate(double number)
        {
            if (number >= .97)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            else if (number >= .82)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            else if (number >= .65)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            }
            else if (number >= .35)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if (number >= .20)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
            }
        }
    }
}