using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ships
{
    static class Ship2Can
    {
        public static Canvas GetCanvas(System.Drawing.Point location, ShipState state)
        {
            var can = new Canvas();
            can.Width = size;
            can.Height = size;
            can.Background = state2imgbrush[state];
            Canvas.SetTop(can, (location.Y)*size);
            Canvas.SetLeft(can, (location.X)*size);
            return can;
        }
        private static Dictionary<ShipState, ImageBrush> state2imgbrush = new Dictionary<ShipState, ImageBrush>
        {
            { ShipState.Hit, new ImageBrush(getImageSource(Ships.Properties.Resources.hit)) },
            { ShipState.MissOrEmpty, new ImageBrush(getImageSource(Ships.Properties.Resources.miss)) },
            { ShipState.Hidden, new ImageBrush(getImageSource(Ships.Properties.Resources.empty)) },
            { ShipState.Sunk, new ImageBrush(getImageSource(Ships.Properties.Resources.sunk))},
            { ShipState.Put, new ImageBrush(getImageSource(Ships.Properties.Resources.put))},
            { ShipState.Putting, new ImageBrush(getImageSource(Ships.Properties.Resources.putting))}
        };
        private static ImageSource getImageSource(System.Drawing.Bitmap bitmap)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        public static readonly int size = 20;
    }
}
