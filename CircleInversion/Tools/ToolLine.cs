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
        private Brush copy;
        private Point? startingPoint;

        public void OnMouseDown(IDestinationSurface surface, Point location)
        {
            copy = new TextureBrush(surface.FinalImage);
            startingPoint = location;
        }

        public void OnMouseMove(IDestinationSurface surface, MouseButtons button, Point location)
        {
            var rect = new RectangleF(Point.Empty, surface.FinalImage.Size);

            if (startingPoint != null)
            {
                using (var g = Graphics.FromImage(surface.FinalImage))
                {
                    g.FillRectangle(copy, rect);
                    g.DrawLine(Pens.Black, startingPoint.Value, location);

                    // Draw the filtered path
                    var path = GenerateFilteredPath(surface.Filter, startingPoint.Value, location, rect);
                    g.DrawPath(Pens.Black, path);
                }
                surface.InvalidateImage();
            }
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

        public void OnMouseUp(IDestinationSurface surface, MouseButtons button, Point location)
        {
            // Clean up original
            if (copy != null)
            {
                copy.Dispose();
                copy = null;
            }

            // And we have a result
            startingPoint = null;
        }
    }
}
