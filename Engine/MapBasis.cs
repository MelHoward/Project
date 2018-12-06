using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace twoDTDS.Engine
{
    class MapBasis
    {
        public object MillisecondsPassedSinceLastTick { get; private set; }

        public void LoadMap(WriteableBitmap map)
        {
            BitmapImage bit = new BitmapImage(new Uri(Asset.env[0]));
            WriteableBitmap write = new WriteableBitmap(bit);

        }
    }
}

