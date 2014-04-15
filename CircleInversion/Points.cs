using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleInversion
{
    public static class Points
    {
        /// <summary>
        /// Calculate the distance between this point and a given point.
        /// </summary>
        /// <param name="p1">This point.</param>
        /// <param name="p2">The given point.</param>
        /// <returns>The distance.</returns>
        public static double Distance(this PointF p1, PointF p2)
        {
            var dX = p1.X - p2.X;
            var dY = p1.Y - p2.Y;
            return Math.Sqrt(dX * dX + dY * dY);
        }

        /// <summary>
        /// Calculate the distance between this point and a given point.
        /// </summary>
        /// <param name="p1">This point.</param>
        /// <param name="p2">The given point.</param>
        /// <returns>The distance.</returns>
        public static double Distance(this Point p1, PointF p2)
        {
            var dX = p1.X - p2.X;
            var dY = p1.Y - p2.Y;
            return Math.Sqrt(dX * dX + dY * dY);
        }
    }
}
