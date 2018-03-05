using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karios.Survay
{
    class CaptureScreen
    {
        private static Image CaptureDesktop()
        {
            // Start the process...
            var memoryImage = new Bitmap(1000, 900);
            var s = new Size(memoryImage.Width, memoryImage.Height);

            // Create graphics
            var memoryGraphics = Graphics.FromImage(memoryImage);

            // Copy data from screen
            memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);
            // Save it!
            return memoryImage;
        }
    }
}
