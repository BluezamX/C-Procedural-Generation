using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.RandomGenerated
{
    class MapGeneratorTest
    {
        public int width;
        public int height;

        public string seed;
        public bool useRandomSeed;
        public int mountainPercentage;

        Random rng;
        public int randomFillPercent;
        public Tile[,] map;

        public void Start()
        {
            GenerateMap();
        }

        void GenerateMap()
        {
            map = new Tile[width, height];
            RandomFillMap();
        }

        void RandomFillMap()
        {
            if (useRandomSeed)
            {
                seed = DateTime.Now.ToString("h:mm:ss tt");
            }

            rng = new Random(seed.GetHashCode());
            
            GenerateHeightMap();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        map[x, y].passable = false;
                    }
                    else if (map[x, y].height >= 4)
                    {
                        map[x, y].passable = false;
                    }
                    else
                    {
                        map[x, y].passable = true; // rng.Next(0, 100) < randomFillPercent);
                    }
                }
            }
        }

        void SmoothMap()
        {
            for (int x = 1; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int surroundingWalls = GetSurroundingWallCount(x, y);

                    if (surroundingWalls >= 4)
                    {
                        map[x, y].passable = false;
                    }
                    else if (surroundingWalls < 4)
                    {
                        map[x, y].passable = true;
                    }
                }
            }
        }

        void GenerateHeightMap()
        {
            int minMountains = (width / 8) * (height / 8);
            int maxMountains = (width / 4) * (height / 4);
            int amountains = rng.Next(minMountains, maxMountains);
            int mounterCounter = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if (mounterCounter < amountains)
                    {
                        map[x, y] = new Tile(5);
                    }
                    else
                    {
                        map[x, y] = new Tile(rng.Next(1,3));
                    }
                    mounterCounter++;
                }
            }
        }

        int GetSurroundingWallCount(int gridX, int gridY)
        {
            int wallCount = 0;

            for (int nextX = gridX - 1; nextX < gridX - 1; nextX++)
            {
                for (int nextY = gridY - 1; nextY < gridY - 1; nextY++)
                {
                    if (nextX >= 0 && nextX < width && nextY >= 0 && nextY < height)
                    {
                        if (nextX != gridX || nextY != gridY)
                        {
                            wallCount += map[nextX, nextY].passable ? 0 : 1;
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }
            return wallCount;
        }

        void OnDrawExtras()
        {
            if (map != null)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {

                    }
                }
            }
        }
    }
}
