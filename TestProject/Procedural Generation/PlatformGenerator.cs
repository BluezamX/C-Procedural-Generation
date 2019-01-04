using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Procedural_Generation
{
    class PlatformGenerator
    {
        private Random rng = new Random();
        public int maxHeight = 0;
        public int minHeight = 0;

        public PlatformGenerator(int minY, int maxY)
        {
            minHeight = minY;
            maxHeight = maxY;
        }

        public Platform Generate(int currentX, int currentY)
        {
            int platformX = 0;
            int platformY = 0;

            int maxX = 0;
            int maxY = 0;
            int minY = 0;

            if (maxHeight < currentY + 2)
            {
                maxY = maxHeight;
            }
            else
            {
                maxY = currentY + 2;
            }

            if (minHeight > currentY - 2)
            {
                minY = minHeight;
            }
            else
            {
                minY = currentY - 2;
            }

            maxX = 5;

            int platformSize = rng.Next(5, 10);
            platformX = rng.Next(0, maxX);
            platformY = rng.Next(minY, maxY);

            return new Platform(platformX, platformY, platformSize);
        }
    }
}
