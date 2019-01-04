using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.RandomGenerated
{
    class MeshGenerator
    {
        public SquareGrid grid;

        public void Generate(int[,] map, float size)
        {
            grid = new SquareGrid(map, size);
        }

        public class SquareGrid
        {
            public Square[,] squares;

            public SquareGrid(int[,] map, float size)
            {
                int nodeCountx = map.GetLength(0);
                int nodeCounty = map.GetLength(1);

                float mapWidth = nodeCountx * size;
                float mapHeigth = nodeCounty * size;

                ControlNode[,] controlNodes = new ControlNode[nodeCountx, nodeCounty];

                for (int x = 0; x < nodeCountx; x++)
                {
                    for (int y = 0; y < nodeCounty; y++)
                    {
                        Vector3 pos = new Vector3(mapWidth / 2 + x * size + size / 2, mapHeigth / 2 + y * size + size / 2, 0);
                        controlNodes[x, y] = new ControlNode(pos, map[x, y] == 1, size);
                    }
                }
            }
        }

        public class Square
        {
            public ControlNode topLeft, topRight, bottomLeft, bottomRight;
            public Node centerTop, centerRight, centerLeft, centerBottom;

            public Square(ControlNode tL, ControlNode tR, ControlNode bL, ControlNode bR, Node cT, Node cR, Node cL, Node cB)
            {
                topLeft = tL;
                topRight = tR;
                bottomLeft = bL;
                bottomRight = bR;

                centerTop = cT;
                centerRight = cR;
                centerLeft = cL;
                centerBottom = cB;
            }
        }

        public interface INodeType
        {
        }

        public class Node : INodeType
        {
            public Vector3 position;
            public int vertexIndex = -1;

            public Node(Vector3 pos)
            {
                position = pos;
            }
        }

        public class ControlNode : INodeType
        {
            public bool active;
            public Node above, right;

            public ControlNode(Vector3 pos, bool active, float size)
            {
                this.active = active;
                above = new Node(new Vector3((pos.x + 1) * size, pos.y * size, pos.z * size));
                right = new Node(new Vector3(pos.x * size, (pos.y + 1) * size, pos.z * size));
            }
        }

        public class Vector3
        {

            public float x;
            public float y;
            public float z;

            public Vector3(float a, float b, float c)
            {
                x = a;
                y = b;
                z = c;
            }
        }
    }
}
