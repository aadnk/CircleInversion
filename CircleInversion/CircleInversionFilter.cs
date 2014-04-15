/*
 *  CircleInversion - Simple drawing program for testing out circle inversion.
 *  Copyright (C) 2014 Kristian S. Stangeland
 *
 *  This program is free software; you can redistribute it and/or modify it under the terms of the 
 *  GNU General Public License as published by the Free Software Foundation; either version 2 of 
 *  the License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
 *  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 *  See the GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License along with this program; 
 *  if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 
 *  02111-1307 USA
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleInversion
{
    public class CircleInversionFilter
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
            var data = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);

            try
            {
                int* original = (int*)data.Scan0;
                int* current = (int*)original;
                int scanWidth = data.Stride / sizeof(int);

                int centerX = (int)Circle.Center.X;
                int centerY = (int)Circle.Center.Y;

                for (int y = 0; y < rect.Height; y++)
                {
                    for (int x = 0; x < rect.Width; x++)
                    {
                        double squaredDistance = Circle.SquaredDistanceToCenter(x, y);

                        // Skip the inside of the circle
                        if (squaredDistance < Circle.SquaredRadius)
                        {
                            current++; 
                            continue;
                        }

                        double ratio = Circle.SquaredRadius / squaredDistance;
                        int srcX = (int)(ratio * (x - centerX)) + centerX;
                        int srcY = (int)(ratio * (y - centerY)) + centerY;

                        // Get the corresponding inner point
                        *(current++) = *(original + srcX + srcY * scanWidth);
                    }
                    current += scanWidth - data.Width;
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
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
