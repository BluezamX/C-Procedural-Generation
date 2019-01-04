using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Procedural_Generation
{
    public class Platform
    {
        public int xDif;
        public int y;
        public int size;

        public Platform(int _x, int _y, int _size)
        {
            xDif = _x;
            y = _y;
            size = _size;
        }

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < size; i++)
            {
                output += "=";
            }
            return output;
        }
    }
}
