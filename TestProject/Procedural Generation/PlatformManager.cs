using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace TestProject.Procedural_Generation
{
    public class PlatformManager
    {
        #region Variables
        // Game settings
        private static char[,] screen;
        private static int screenWidth = 96;
        private static int screenHeight = 20;

        // Platform generation settings
        private static int minGeneration = 4;
        private static int maxGeneration = 18;

        private static PlatformGenerator pg;
        private static List<Platform> platforms = new List<Platform>();

        // Physics variables
        private static int oldX = 0;
        private static int currentX = 0;
        private static int currentY = 8;
        private static string toSkip = "";

        private static Player player;

        // Timer variables
        private static float timeLapse = 200;
        private static float jumpTime = 200;
        private static Timer graphicUpdater;
        private static Timer physicUpdater;
        private static Timer jumpTimer;
        private static bool isJumping = false;

        private static long score = 0;
        #endregion

        public PlatformManager()
        {
            screen = new char[screenHeight, screenWidth];
            pg = new PlatformGenerator(minGeneration, maxGeneration);
            player = new Player();

            // Physics
            physicUpdater = new Timer(timeLapse);
            physicUpdater.Elapsed += ExecutePhysics;
            physicUpdater.AutoReset = true;
            physicUpdater.Enabled = true;

            jumpTimer = new Timer(jumpTime);
            jumpTimer.Elapsed += Land;
            jumpTimer.Enabled = true;

            // Graphics
            graphicUpdater = new Timer(timeLapse);
            graphicUpdater.Elapsed += ExecuteGraphics;
            graphicUpdater.AutoReset = true;
            graphicUpdater.Enabled = true;
            while (true)
            {
                score++;
            }
        }

        private void Land(object source, EventArgs e)
        {
            isJumping = false;
        }

        #region Graphics
        private void ExecuteGraphics(object source, ElapsedEventArgs e)
        {
            Console.Clear();
            Console.Write(Display());
        }

        // Makes sure everything is printed correctly
        public string Display()
        {
            string result = "";

            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    if (y == player.y && x == player.x)
                    {
                        result += "O";
                    }
                    else if (screen[y, x] == '=')
                    {
                        result += "=";
                    }
                    else
                    {
                        result += " ";
                    }
                }
                result += "\n";
            }
            return result;
        }
        #endregion

        #region Physics
        // Controls platform generation and movement
        private void ExecutePhysics(object source, ElapsedEventArgs e)
        {
            Platformer();
            if (player.y == screenHeight - 1)
            {
                graphicUpdater.Stop();
                physicUpdater.Stop();
                Console.WriteLine();
                Console.WriteLine("Game over! Score: " + Math.Floor(Math.Sqrt(score)));
            }
            else if (isJumping)
            {
                player.y -= 1;
            }
            else if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar && screen[player.y + 1, player.x] == '=')
            {
                isJumping = true;
                jumpTimer.Start();
                player.y -= 1;
            }
            else if (screen[player.y + 1, player.x] != '=' && !isJumping)
            {
                player.y += 1;
            }
        }

        // Generate a new platform
        public void Platformer()
        {
            Platform newPlatform = pg.Generate(currentX, currentY);
            currentY = newPlatform.y;

            if (currentX + newPlatform.size + newPlatform.xDif > screenWidth)
            {
                MoveScreen(newPlatform.size + newPlatform.xDif);
                currentX -= newPlatform.size + newPlatform.xDif;
                oldX -= newPlatform.size + newPlatform.xDif;
            }

            while (currentX < oldX + newPlatform.size + newPlatform.xDif)
            {
                screen[currentY, currentX] = '=';
                currentX += 1;
            }
            oldX = currentX;
        }

        // Update all rows so the newly added ones fit.
        public void MoveScreen(int amount)
        {
            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = amount; x < screenWidth; x++)
                {
                    screen[y, x - amount] = screen[y, x];
                }
            }
            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = screenWidth - amount; x < screenWidth; x++)
                {
                    screen[y, x] = '\0';
                }
            }
        }
        #endregion
    }
}
