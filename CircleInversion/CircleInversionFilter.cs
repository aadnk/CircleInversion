using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleInversion
{
    class CircleInversionFilter
    {
        public Circle Circle { get; private set;}

        public CircleInversionFilter(Circle circle)
        {
            this.Circle = circle;
        }

        /// <summary>
        /// Apply the inversion filter to every point outside the circle in the bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap to modify.</param>
        public void FilterBitmap(Bitmap bitmap)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    double originalDistance = Circle.DistanceToCenter(x, y);

                    // Skip the inside of the circle
                    if (originalDistance < Circle.Radius)
                        continue;
                    // Get the corresponding inner point
                    PointF innerPoint = FilterPoint(x, y);
                    bitmap.SetPixel(x, y, bitmap.GetPixel((int)innerPoint.X, (int)innerPoint.Y));
                }
            }
        }

        /// <summary>
        /// Filter a single point using circle inversion.
        /// </summary>
        /// <param name="point">The original point.</param>
        /// <returns>The filtered point.</returns>
        public PointF FilterPoint(PointF point)
        {
            return FilterPoint(point.X, point.Y);
        }

        /// <summary>
        /// Filter a single point using circle inversion.
        /// </summary>
        /// <param name="x">The original x coordinate.</param>
        /// <param name="y">The original y coordinate.</param>
        /// <returns>The filtered point.</returns>
        public PointF FilterPoint(float x, float y)
        {
            // d_0 * d_1 = r^2 => d_1 = r^2 / d_0
            double originalDistance = Circle.DistanceToCenter(x, y);
            double newDistance = Circle.SquaredRadius / originalDistance;
            double ratio = newDistance / originalDistance;

            // Similar triangles - the ratio will be the same
            return new PointF(
               (float)(ratio * (x - Circle.Center.X) + Circle.Center.X),
               (float)(ratio * (y - Circle.Center.Y) + Circle.Center.Y)
            );
        }
    }
}
