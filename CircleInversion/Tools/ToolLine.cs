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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircleInversion.Tools
{
    class ToolLine : ITool
    {
        private Point? startingPoint;
        private Point? currentPoint;

        public void OnMouseDown(IDestinationSurface surface, Point location)
        {
            startingPoint = location;
            currentPoint = null;
        }

        public void OnMouseMove(IDestinationSurface surface, MouseButtons button, Point location)
        {
            if (startingPoint != null)
            {
                currentPoint = location;
                surface.InvalidateImage();
            }
        }

        public void OnSurfaceDrawing(IDestinationSurface surface, Graphics g)
        {
            if (startingPoint != null && currentPoint != null)
            {
                DrawLine(surface, g, startingPoint.Value, currentPoint.Value);                    
            }
        }

        public void OnMouseUp(IDestinationSurface surface, MouseButtons button, Point location)
        {
            // Draw line on the original
            if (startingPoint != null && currentPoint != null)
            {
                using (var g = Graphics.FromImage(surface.FinalImage))
                {
                    DrawLine(surface, g, startingPoint.Value, currentPoint.Value);  
                }
            }

            // And we have a result
            startingPoint = null;
            currentPoint = null;
        }

        private void DrawLine(IDestinationSurface surface, Graphics g, PointF starting, PointF ending)
        {
            g.DrawLine(Pens.Black, startingPoint.Value, currentPoint.Value);
            g.DrawPath(Pens.Black, surface.Filter.FilterLine(starting, ending));
        }
    }
}
