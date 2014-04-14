using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircleInversion
{
    public partial class CircleInvertor : Form
    {
        // Handle a drawing operation
        private Bitmap bitmap;
        private Point? previousPoint;

        // Circle inversion
        private CircleInversionFilter currentFilter;

        public CircleInvertor()
        {
            InitializeComponent();
            InitializeCircle();
        }

        private void CircleInvertor_ResizeEnd(object sender, EventArgs e)
        {
            InitializeCircle();
        }

        private void CircleInvertor_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                InitializeCircle();
            }
        }

        private void InitializeCircle()
        {
            // Clean up existing resources
            if (bitmap != null)
            {
                bitmap.Dispose();
            }
            var currentCircle = ComputeCircle(0.5f);

            // Prepare background image
            bitmap = new Bitmap(PictureContainer.Width, PictureContainer.Height, PixelFormat.Format32bppArgb);
            currentFilter = new CircleInversionFilter(currentCircle);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawEllipse(Pens.Gray, currentCircle.BoundingBox);
            }
            PictureContainer.Invalidate();
        }

        private void PictureContainer_MouseDown(object sender, MouseEventArgs e)
        {
            previousPoint = e.Location;
        }

        private void PictureContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (previousPoint != null)
            {
                // Draw line segment
                using (var g = Graphics.FromImage(bitmap))
                {
                    var from = previousPoint.Value;
                    var to = e.Location;

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawLine(Pens.Black, from, to);
                    g.DrawLine(Pens.DarkBlue, currentFilter.FilterPoint(from), currentFilter.FilterPoint(to));
                }
                previousPoint = e.Location;
                PictureContainer.Invalidate();
            }
        }

        private void PictureContainer_MouseUp(object sender, MouseEventArgs e)
        {
            previousPoint = null;
        }

        private void PictureContainer_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(bitmap, Point.Empty);
        }

        private Circle ComputeCircle(float radiusScale)
        {
            var rect = new Rectangle(Point.Empty, PictureContainer.Size);
            var center = new PointF(
                rect.Left + rect.Width / 2.0f,
                rect.Top + rect.Height / 2.0f);
            var radius = Math.Min(rect.Width / 2.0, rect.Height / 2.0);

            return new Circle(center, (float)(radius * radiusScale));
        }

        private void buttonInvert_Click(object sender, EventArgs e)
        {
            var watch = new Stopwatch();

            watch.Start();
            currentFilter.FilterBitmap(bitmap);
            watch.Stop();

            PictureContainer.Invalidate();
            Text = "Computed inverse in " + watch.Elapsed;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (openImageFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Draw image
                using (var image = Bitmap.FromFile(openImageFile.FileName))
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.Clip = currentFilter.Circle.ToRegion();
                    g.DrawImage(image, currentFilter.Circle.BoundingBox);
                }
                PictureContainer.Invalidate();
            }
        }
    }
}
