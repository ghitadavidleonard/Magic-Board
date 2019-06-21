using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MagicBoard.Utility
{
    public class PathGenerator
    {
        private int row;
        readonly private int col;
        private int length;
        private enum Directions { Up, Down, Left, Right};

        readonly Random rnd = new Random();

        public PathGenerator(int length, int col, int row)
        {
            this.length = length;
            this.col = col;
            this.row = row;
        }

        private Point GetRandomLocation()
        {
            var direction = (Directions)(rnd.Next(0, 40) % 4);
            switch (direction)
            {
                case Directions.Up: return new Point(0, -1);
                case Directions.Down: return new Point(0, +1);
                case Directions.Left: return new Point(-1, 0);
                case Directions.Right: return new Point(+1, 0);
                default: return new Point(0, 0);
            }
        }

        private bool IsValidMove(Point point, bool[,] visited) => IsWithinBounds((int) point.X,(int) point.Y) && IsWithinTheLength(visited) && !visited[(int) point.Y,(int) point.X];
        

        private bool IsWithinBounds(int x, int y)
        {
            if ((x > 0 && y > 0) && (x < col && y < row))
                return true;
            else
                return false;
        }

        private bool IsWithinTheLength(bool[,] visited)
        {
            int visitedNodes = 0;
            foreach (var verify in visited)
            {
                if(verify == true)
                {
                    visitedNodes++;
                }
            }

            if (length > visitedNodes)
                return true;
            else
                return false;
        }

        public List<Point> GeneratePath()
        {
            int tryCount = 0;
            bool braked = false;
            int tryBrakedTwo = 0;
            bool brakedTwo = false;

            List<Point> coordList = new List<Point>(length);
            bool[,] visited = new bool[row+1, col+1];

            var currentLocation = new Point(1, 1);
            visited[1, 1] = true;
            coordList.Add(currentLocation);
          
            while(IsWithinTheLength(visited))
            {
                tryCount = 0;
                braked = false;
                var newLocation = new Point();
                do
                {
                    newLocation = GetRandomLocation();
                    if (tryCount >= 6) {
                        braked = true;
                        break;
                    }
                    tryCount++;
                } while (!IsValidMove(currentLocation.Add(newLocation), visited));

                if (!braked)
                {
                    tryBrakedTwo = 0;
                    brakedTwo = false;
                    currentLocation.Offset(newLocation.X, newLocation.Y);
                    visited[(int)currentLocation.Y, (int)currentLocation.X] = true;
                    coordList.Add(currentLocation);
                }
                else
                    tryBrakedTwo++;

                if (tryBrakedTwo >= 6) {
                    brakedTwo = true;
                    break;
                }
            }

            if (brakedTwo)
                return GeneratePath();

            return coordList;
        }

    }
}
