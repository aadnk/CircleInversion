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

        /// <summary>
        /// Construct a new circle from center coordinates and a radius.
        /// </summary>
        /// <param name="centerX">The center x-coordinate.</param>
        /// <param name="centerY">The center y-coordinate.</param>
        /// <param name="radius">The radius.</param>
        public Circle(float centerX, float centerY, float radius)
            : this(new PointF(centerX, centerY), radius)
        {
        }

        /// <summary>
        /// Construct a new circle from a center and a radius.
        /// </summary>
        /// <param name="center">The center point in the XY-plane.</param>
        /// <param name="radius">The radius.</param>
        public Circle(PointF center, float radius)
        {
            this.Center = center;
            this.Radius = radius;
            this.SquaredRadius = radius * radius;
            this.BoundingBox = new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);
        }

        /// <summary>
        /// Construct a circle that passes through the three points. 
        /// </summary>
        /// <param name="a">First point.</param>
        /// <param name="b">Second point.</param>
        /// <param name="c">Third point.</param>
        /// <returns>The unique circle, or NULL if the points are colinear.</returns>
        public static Circle FromPoints(PointF a, PointF b, PointF c)
        {
            double A = b.X - a.X;
            double B = b.Y - a.Y;
            double C = c.X - a.X;
            double D = c.Y - a.Y;

            double E = A * (a.X + b.X) + B * (a.Y + b.Y);
            double F = C * (a.X + c.X) + D * (a.Y + c.Y);

            double G = 2 * (A * (c.Y - b.Y) - B * (c.X - b.X));
            if (G == 0.0)
                return null;

            // Circle center
            double px = (D * E - B * F) / G;
            double py = (A * F - C * E) / G;
            
            // Circle radius
            double dx = a.X - px;
            double dy = a.Y - py;
            double radius = Math.Sqrt(dx * dx + dy * dy);

            return new Circle((float)px, (float)py, (float)radius);
        }

        /// <summary>
        /// Calculate the angle from the center to the given point, measured from the x-axis.
        /// </summary>
        /// <param name="x">The point.</param>
        /// <returns>The resulting angle in degrees (0, 360).</returns>
        public double Angle(PointF point)
        {
            return Angle(point.X, point.Y);
        }

        /// <summary>
        /// Calculate the angle from the center to the given point, measured from the x-axis.
        /// </summary>
        /// <param name="x">The x-coordinate of the point.</param>
        /// <param name="y">The y-coordinate of the point.</param>
        /// <returns>The resulting angle in degrees (0, 360)</returns>
        public double Angle(float x, float y)
        {
            double angle = Math.Atan2(y - Center.Y, x - Center.X);

            if (angle < 0)
                angle += 2 * Math.PI;
            return angle * (180.0 / Math.PI);
        }

        /// <summary>
        /// Retrieve the full diameter of the circle.
        /// </summary>
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
        public double Distance(float x, float y)
        {
            return Math.Sqrt(SquaredDistance(x, y));
        }

        /// <summary>
        /// Calculate the squared distance from a given point to the center of the circle.
        /// </summary>
        /// <param name="x">The point.</param>
        /// <returns>The squared distance.</returns>
        public double SquaredDistance(PointF point)
        {
            return SquaredDistance(point.X, point.Y);
        }

        /// <summary>
        /// Calculate the squared distance from a given point to the center of the circle.
        /// </summary>
        /// <param name="x">X coordinate of the point.</param>
        /// <param name="y">Y coordinate of the point.</param>
        /// <returns>The squared distance.</returns>
        public double SquaredDistance(float x, float y)
        {
            var dX = Center.X - x;
            var dY = Center.Y - y;
            return dX * dX + dY * dY;
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
