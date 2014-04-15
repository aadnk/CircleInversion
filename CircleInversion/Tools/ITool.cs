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
