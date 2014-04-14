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
        public unsafe void FilterBitmap(Bitmap bitmap)
        {
            var rect = new Rectangle(Point.Empty, bitmap.Size);
            //var data = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            try
            {
                //int* source = (int*)data.Scan0;

                for (int y = 0; y < rect.Height; y++)
                {
                    for (int x = 0; x < rect.Width; x++)
                    {
                        double squaredDistance = Circle.SquaredDistanceToCenter(x, y);

                        // Skip the inside of the circle
                        if (squaredDistance < Circle.SquaredRadius)
                            continue;

                        double ratio = Circle.SquaredRadius / squaredDistance;
                        int srcX = (int) (ratio * (x - Circle.Center.X) + Circle.Center.X);
                        int srcY = (int) (ratio * (y - Circle.Center.Y) + Circle.Center.Y);

                        // Get the corresponding inner point
                        bitmap.SetPixel(x, y, bitmap.GetPixel(srcX, srcY));
                    }
                }
            }
            finally
            {
                //bitmap.UnlockBits(data);
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
            double squaredDistance = Circle.SquaredDistanceToCenter(x, y);
            double ratio = Circle.SquaredRadius / squaredDistance;

            // Similar triangles - the ratio will be the same
            return new PointF(
               (float)(ratio * (x - Circle.Center.X) + Circle.Center.X),
               (float)(ratio * (y - Circle.Center.Y) + Circle.Center.Y)
            );
        }
    }
}
