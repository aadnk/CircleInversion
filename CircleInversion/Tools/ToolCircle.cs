using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircleInversion.Tools
{
    class ToolCircle : ITool
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
                DrawCircles(surface, g, startingPoint.Value, currentPoint.Value);
            }
        }

        public void OnMouseUp(IDestinationSurface surface, MouseButtons button, Point location)
        {
            // Draw line on the original
            if (startingPoint != null && currentPoint != null)
            {
                using (var g = Graphics.FromImage(surface.FinalImage))
                {
                    DrawCircles(surface, g, startingPoint.Value, currentPoint.Value);
                }
            }
            // And we have a result
            startingPoint = null;
            currentPoint = null;
        }

        private void DrawCircles(IDestinationSurface surface, Graphics g, PointF starting, PointF ending)
        {
            Circle normal = new Circle(starting, (float)starting.Distance(ending));
            Circle filtered = surface.Filter.FilterCircle(normal);

            g.DrawEllipse(Pens.Black, normal.BoundingBox);
            g.DrawEllipse(Pens.DarkBlue, filtered.BoundingBox);
        }
    }
}
