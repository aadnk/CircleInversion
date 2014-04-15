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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircleInversion.Tools
{
    class ToolPencil : ITool
    {
        private Point? currentPoint;

        public void OnMouseDown(IDestinationSurface surface, Point location)
        {
            currentPoint = location;
        }

        public void OnMouseMove(IDestinationSurface surface, MouseButtons button, Point location)
        {
            if (currentPoint != null)
            {
                var filter = surface.Filter;

                // Draw line segment
                using (var g = Graphics.FromImage(surface.FinalImage))
                {
                    var from = currentPoint.Value;
                    var to = location;

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawLine(Pens.Black, from, to);
                    g.DrawLine(Pens.DarkBlue, filter.FilterPoint(from), filter.FilterPoint(to));
                }
                surface.InvalidateImage();
                currentPoint = location;
            }
        }

        public void OnMouseUp(IDestinationSurface surface, MouseButtons button, Point location)
        {
            currentPoint = null;
        }
    }
}
