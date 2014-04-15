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
    /// <summary>
    /// Represents a drawing tool.
    /// </summary>
    interface ITool
    {
        /// <summary>
        /// Invoked when a user clicks the drawing surface.
        /// </summary>
        /// <param name="surface">The drawing surface.</param>
        /// <param name="location">The clicked point.</param>
        void OnMouseDown(IDestinationSurface surface, Point location);

        /// <summary>
        /// Invoked when a user moves the cursor to a specific point, whether clicking or not.
        /// </summary>
        /// <param name="surface">The drawing surface.</param>
        /// <param name="button">The currently clicking button.</param>
        /// <param name="location">The point the cursor has moved to.</param>
        void OnMouseMove(IDestinationSurface surface, MouseButtons button, Point location);

        /// <summary>
        /// Invoked when a user releases the mouse button.
        /// </summary>
        /// <param name="surface">The drawing surface.</param>
        /// <param name="button">The released mouse button.</param>
        /// <param name="location">The current location.</param>
        void OnMouseUp(IDestinationSurface surface, MouseButtons button, Point location);

        /// <summary>
        /// Invoked when the surface is drawing.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="g">The graphics context.</param>
        void OnSurfaceDrawing(IDestinationSurface surface, Graphics g);
    }

    public interface IDestinationSurface
    {
        /// <summary>
        /// Image with the final bitmap data.
        /// </summary>
        Bitmap FinalImage { get; }

        /// <summary>
        /// Retrieve the current inversion filter.
        /// </summary>
        CircleInversionFilter Filter { get; }

        /// <summary>
        /// Invalidate the final image.
        /// </summary>
        void InvalidateImage();
    }
}
