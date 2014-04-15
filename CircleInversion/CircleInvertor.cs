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

using CircleInversion.Tools;
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
    public partial class CircleInvertor : Form, IDestinationSurface
    {
        // Current image
        public Bitmap FinalImage { get; private set; }

        // Circle inversion
        public CircleInversionFilter Filter { get; private set; }

        // Current tool
        private ITool tool;

        // Detect minimizing
        private bool wasMaximized;

        public void InvalidateImage()
        {
            PictureContainer.Invalidate();
            PictureContainer.Update();
        }

        public CircleInvertor()
        {
            InitializeComponent();
            InitializeTool();
            InitializeCircle();
        }

        private void CircleInvertor_ResizeEnd(object sender, EventArgs e)
        {
            if (FinalImage != null && FinalImage.Width == PictureContainer.Width && FinalImage.Height == PictureContainer.Height)
                return;
            InitializeCircle();
        }

        private void CircleInvertor_SizeChanged(object sender, EventArgs e)
        {
            bool isMaximized = this.WindowState == FormWindowState.Maximized;

            if (isMaximized || wasMaximized)
            {
                InitializeCircle();
                wasMaximized = isMaximized;
            }
        }

        private void InitializeCircle()
        {
            var currentCircle = ComputeCircle((float)circleSizeSelector.Value);
            Debug.Print("InitializeCircle to " + PictureContainer.Width + ", " + PictureContainer.Height);

            // Prepare background image
            FinalImage = PrepareBitmap(FinalImage, PictureContainer.Width, PictureContainer.Height);
            Filter = new CircleInversionFilter(currentCircle);

            DrawGrayCircle();
            PictureContainer.Invalidate();
        }

        private void DrawGrayCircle()
        {
            using (var g = Graphics.FromImage(FinalImage))
            {
                g.DrawEllipse(Pens.Gray, Filter.Circle.BoundingBox);
            }
        }

        private void InitializeTool()
        {
            if (toolPencil.Checked)
                tool = new ToolPencil();
            if (toolLine.Checked)
                tool = new ToolLine();
            if (toolCircle.Checked)
                tool = new ToolCircle();
        }

        private Bitmap PrepareBitmap(Bitmap existing, int width, int height)
        {
            Bitmap result = existing;

            // Create a new bitmap
            if (result == null || result.Width != width || result.Height != height)
            {
                // Clean up existing resources
                if (result != null)
                {
                    result.Dispose();
                }
                result = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            }

            // Clear bitmap content
            using (var g = Graphics.FromImage(result))
            {
                g.Clear(Color.White);
            }
            return result;
        }

        private void PictureContainer_MouseDown(object sender, MouseEventArgs e)
        {
            tool.OnMouseDown(this, e.Location);
        }

        private void PictureContainer_MouseMove(object sender, MouseEventArgs e)
        {
            tool.OnMouseMove(this, e.Button, e.Location);
        }

        private void PictureContainer_MouseUp(object sender, MouseEventArgs e)
        {
            tool.OnMouseUp(this, e.Button, e.Location);
        }

        private void PictureContainer_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(FinalImage, Point.Empty);
            
            if (tool != null)
            {
                tool.OnSurfaceDrawing(this, e.Graphics);
            }
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

        private void ComputeInverse()
        {
            var watch = new Stopwatch();

            watch.Start();
            Filter.FilterBitmap(FinalImage);
            DrawGrayCircle();
            watch.Stop();

            PictureContainer.Invalidate();
            Text = "Computed inverse in " + watch.Elapsed.TotalMilliseconds + " ms";
        }

        private void buttonInvert_Click(object sender, EventArgs e)
        {
            ComputeInverse();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (openImageFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Draw image
                using (var image = Bitmap.FromFile(openImageFile.FileName))
                using (var g = Graphics.FromImage(FinalImage))
                {
                    g.Clip = Filter.Circle.ToRegion();
                    g.DrawImage(image, Filter.Circle.BoundingBox);
                }

                // Now compute the inverse
                ComputeInverse();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            InitializeCircle();
        }

        private void circleSizeSelector_ValueChanged(object sender, EventArgs e)
        {
            InitializeCircle();
        }

        private void toolPencil_Click(object sender, EventArgs e)
        {
            UpdateTool(sender);
        }

        private void toolLine_Click(object sender, EventArgs e)
        {
            UpdateTool(sender);
        }

        private void toolCircle_Click(object sender, EventArgs e)
        {
            UpdateTool(sender);
        }

        private void UpdateTool(object sender)
        {
            foreach (ToolStripButton button in toolStripTools.Items.OfType<ToolStripButton>())
            {
                button.Checked = button == sender;
            }
            InitializeTool();
        }

        private void toolStripAbout_Click(object sender, EventArgs e)
        {
            AboutProgram about = new AboutProgram();
            about.ShowDialog();
        }
    }
}
