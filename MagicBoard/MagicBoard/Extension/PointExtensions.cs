using System.Windows;

namespace MagicBoard.Utility
{
    public static class PointExtensions
    {
        public static Point Add(this Point currentPoint, Point newPoint)
        {
            var point = new Point();
            point.Offset(currentPoint.X, currentPoint.Y);
            point.Offset(newPoint.X, newPoint.Y);
            return point;
        }

        public static void Offset(this Point currentPoint, Point newPoint)
        {
            currentPoint.Offset(newPoint.X, newPoint.Y);
        }
    }
}
