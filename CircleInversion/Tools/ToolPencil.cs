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
