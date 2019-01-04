using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static TestProject.Constants;

namespace TestProject.MapGeneration
{
    static class MapGenerator
    {
        static Random rng = new Random();
        static double[,] map1 = new double[mapsize, mapsize];
        static double[,] map2 = new double[mapsize, mapsize];

        public static void Display()
        {
            // Normal
            map1 = Generate();
            for (int y = 0; y < mapsize; y++)
            {
                for (int x = 0; x < mapsize; x++)
                {
                    Colorate(map1[y, x]);
                    Console.Write("  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();

            for (int y = 0; y < mapsize; y++)
            {
                for (int x = 0; x < mapsize; x++)
                {
                    map2 = Generalize(Generate(map1[x, y]));

                    Console.WriteLine(y + ":" + x);
                    for (int i = 0; i < mapsize; i++)
                    {
                        for (int j = 0; j < mapsize; j++)
                        {
                            Colorate(map2[i, j]);
                            Console.Write("  ");
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }
        }

        static double[,] Generate(double template)
        {
            double[,] array = new double[mapsize, mapsize];

            for (int y = 0; y < mapsize; y++)
            {
                for (int x = 0; x < mapsize; x++)
                {
                    array[y, x] = (rng.NextDouble() / 2) + (template / 2);
                }
            }

            return array;
        }

        static double[,] Generate()
        {
            double[,] array = new double[mapsize, mapsize];

            for (int y = 0; y < mapsize; y++)
            {
                for (int x = 0; x < mapsize; x++)
                {
                    array[y, x] = rng.NextDouble();
                }
            }

            return array;
        }

        static double[,] Generalize(double[,] map)
        {
            for (int y = 1; y < mapsize - 1; y++)
            {
                for (int x = 1; x < mapsize - 1; x++)
                {
                    if (map[y, x] > .8)
                    {
                        map[y - 1, x - 1] = map[y - 1, x - 1] + .05;
                        map[y + 1, x - 1] = map[y + 1, x - 1] + .05;
                        map[y - 1, x + 1] = map[y - 1, x + 1] + .05;
                        map[y + 1, x + 1] = map[y + 1, x + 1] + .05;
                    }
                    else if (map[y, x] < .2)
                    {
                        map[y - 1, x - 1] = map[y - 1, x - 1] - .05;
                        map[y + 1, x - 1] = map[y + 1, x - 1] - .05;
                        map[y - 1, x + 1] = map[y - 1, x + 1] - .05;
                        map[y + 1, x + 1] = map[y + 1, x + 1] - .05;
                    }
                }
            }

            return map;
        }

        static void Colorate(double number)
        {
            if (number >= .85)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            else if (number >= .42)
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
