using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.RandomGenerated
{
    class Tile
    {
        public bool passable;
        public int height;
        public string type;

        public Tile(bool passable)
        {
            this.passable = passable;
        }

        public Tile(int height)
        {
            this.height = height;
        }

        public Tile(bool passable, int height)
        {
            this.passable = passable;
            this.height = height;
        }

        public override string ToString()
        {
            return passable ? "  " : "XX";
        }
    }
}
