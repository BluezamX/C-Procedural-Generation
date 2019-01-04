using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.MapGeneration;
using TestProject.Noise;
using TestProject.Procedural_Generation;
using TestProject.RandomGenerated;

namespace TestProject
{
    class Program
    {
        private static readonly string empty = " * ";
        private static readonly string forest = " F ";
        private static readonly string grassland = " G ";
        private static readonly string river = " R ";
        private static readonly string hLine = " - ";
        private static readonly string vLine = " | ";
        private static string output = "";

        private static readonly int hSize = 15;
        private static readonly int vSize = 15;

        private static int[,] heightMap = new int[hSize, vSize];

        private static Random rng = new Random();

        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            /*
            Grid();
            Console.WriteLine(output);
            Console.ReadKey();
            output = "";

            MapGenerator generator = new MapGenerator();
            generator.width = 50;
            generator.height = 50;
            generator.randomFillPercent = 40;
            generator.useRandomSeed = true;
            generator.seed = "";
            generator.Start();

            for (int i = 0; i < generator.height; i++)
            {
                for (int j = 0; j < generator.width; j++)
                {
                    output += generator.map[i, j].passable? "  ": "XX";
                }
                output += "\n";
            }

            Console.WriteLine(output);
            Console.ReadKey();
            output = "";

            NoiseGenerator.Display(32, 64);
            Console.ReadKey();
            */

            //MapGenerator.Display();

            PlatformManager pm = new PlatformManager();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
        
        /*
        private static string Platformer()
        {
            Platform currentPlatform = pg.Generate(currentX, currentY);
            currentX = currentPlatform.xDif + currentPlatform.size;
            currentY = currentPlatform.y;
            platforms.Add(currentPlatform);

            for (int i = 0; i < platforms.Count; i++)
            {
                Platform p = platforms[i];
                p.xDif += currentPlatform.size;
                if (p.xDif > screenWidth)
                {
                    platforms.Remove(p);
                    i--;
                }
            }
            return DisplayPlatforms();
        }

        private static string DisplayPlatforms()
        {
            string result = "";
            for (int y = 0; y < pg.maxHeight; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    Platform platform = platforms.Find(p => p.y == (pg.maxHeight - y) && p.xDif == x);
                    if (platform != null)
                    {
                        string temp = WritePlatform(platform, x, y);
                        result += temp;
                        x += temp.Length;
                    }
                    else
                    {
                        result += toSkip;
                    }
                }
                result += "\n";
            }
            return result;
        }

        private static string WritePlatform(Platform p, int x, int y)
        {
            string result = "";

            if (screenWidth < p.size + x)
            {
                for (int i = x; i < screenWidth; i++)
                {
                    result += "=";
                    toSkip += " ";
                }
            }
            else
            {
                result += p.ToString();
            }

            return result;
        }

        private static string UpdateSkip()
        {
            string result = "";
            for (int i = 1; i < toSkip.Length; i++)
            {
                result += " ";
            }
            return result;
        }
         */

        static private void Grid()
        {
            for (int i = 0; i < vSize; i++)
            {
                for (int j = 0; j < hSize; j++)
                {
                    if (i == 0)
                    {
                        output += hLine;
                    }
                    else if (i == vSize - 1)
                    {
                        output += hLine;
                    }
                    else if (j % 2 == 0)
                    {
                        output += vLine;
                    }
                    else if (i % 2 == 0)
                    {
                        output += hLine;
                    }
                    else
                    {
                        output += empty;
                    }
                }
                if (i != hSize - 1)
                {
                    output += "\n";
                }
            }
        }

        static private void SetHeightMap()
        {
            int maxMountain = (int)(Math.Floor(hSize / 2.0) * Math.Floor(vSize / 2.0)) / 4;
            int[] amountain = new int[rng.Next(0, maxMountain)];
            for (int i = 0; i < vSize; i++)
            {
                for (int j = 0; j < hSize; j++)
                {

                }
            }
        }

        /* 
        
            6 Elevation levels: 0-5
        
        0:  Coast, Sea              
        1:  Ground          Forest, River
        2:  Ground          Forest, River
        3:  Hills           Forest, River
        4:  Cliffs          River
        5:  Mountains       Volcano

            Terrain Types

        Plains              1-2
        Woodlands           1-3
        Hills               1-3
        Cliffs              1-4
        Canyons             0-4
        Mountain            2-5
        Mountains           3-5

        Coastal             0-1

        */
    }
}
