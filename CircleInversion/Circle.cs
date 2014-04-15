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
        /// Calculate the squared distance from a given point to the center of the circle.
        /// </summary>
        /// <param name="x">X coordinate of the point.</param>
        /// <param name="y">Y coordinate of the point.</param>
        /// <returns>The squared distance.</returns>
        public double SquaredDistanceToCenter(float x, float y)
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
