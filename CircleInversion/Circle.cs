using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleInversion
{
    /// <summary>
    /// A circle represented by a center and a radius.
    /// </summary>
    public class Circle
    {
        public PointF Center { get; private set; }
        public float Radius { get; private set; }
        public float SquaredRadius { get; private set; }
        public RectangleF BoundingBox { get; private set; }

        public Circle(PointF center, float radius)
        {
            this.Center = center;
            this.Radius = radius;
            this.SquaredRadius = Radius * Radius;
            this.BoundingBox = new RectangleF(Center.X - Radius, Center.Y - Radius, Diameter, Diameter);
        }

        public float Diameter
        {
            get { return Radius * 2; }
        }

        /// <summary>
        /// Calculate the distance from a given point to the center of the circle.
        /// </summary>
        /// <param name="x">X coordinate of the point.</param>
        /// <param name="y">Y coordinate of the point.</param>
        /// <returns>The distance.</returns>
        public double DistanceToCenter(float x, float y)
        {
            var dX = Center.X - x;
            var dY = Center.Y - y;
            return Math.Sqrt(dX * dX + dY * dY);
        }

        /// <summary>
        /// Convert this circle to a region.
        /// </summary>
        public Region ToRegion()
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(BoundingBox);
            return new Region(path);
        }
    }
}
