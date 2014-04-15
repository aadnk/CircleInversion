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
            var rect = new RectangleF(Point.Empty, surface.FinalImage.Size);
            g.DrawLine(Pens.Black, startingPoint.Value, currentPoint.Value);

            // Draw the filtered path
            var path = GenerateFilteredPath(surface.Filter, starting, ending, rect);
            g.DrawPath(Pens.Black, path);
        }

        private GraphicsPath GenerateFilteredPath(CircleInversionFilter filter, PointF start, PointF end, RectangleF display)
        {
            GraphicsPath path = new GraphicsPath();
            List<PointF> points = new List<PointF>();
            List<PointF> current = new List<PointF>();

            GenerateFilteredPoints(filter, points, start, end, display, 10);

            if (points.Count == 0)
                return path;
            current.Add(points[0]);

            for (int i = 1; i < points.Count; i++)
            {
                double distance = Distance(points[i - 1], points[i]);

                // Break up discontinuities
                if (distance > 10)
                {
                    path.AddLines(current.ToArray());
                    path.StartFigure();
                    current.Clear();
                }
                current.Add(points[i]);
            }
            path.AddLines(current.ToArray());
            return path;
        }

        private void GenerateFilteredPoints(CircleInversionFilter filter, List<PointF> points, PointF start, PointF end, RectangleF display, double minDistance)
        {
            var filteredStart = filter.FilterPoint(start);
            var filteredEnd = filter.FilterPoint(end);
            var distance = Distance(filteredStart, filteredEnd);

            // More to generate?
            if (distance > minDistance)
            {
                if (!display.Contains(filteredStart) && !display.Contains(filteredEnd))
                {
                    return;
                }

                // Divide the line into two (undo the filtering)
                PointF middle = new PointF(
                   (start.X + end.X) / 2.0f,
                   (start.Y + end.Y) / 2.0f
                );

                GenerateFilteredPoints(filter, points, start, middle, display, minDistance);
                GenerateFilteredPoints(filter, points, middle, end, display, minDistance);
            }
            else
            {
                points.Add(filteredStart);
                points.Add(filteredEnd);
            }
        }

        private double Distance(PointF a, PointF b)
        {
            var dX = a.X - b.X;
            var dY = a.Y - b.Y;
            return Math.Sqrt(dX * dX + dY * dY);
        }
    }
}
