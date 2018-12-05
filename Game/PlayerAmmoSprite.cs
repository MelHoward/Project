using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using twoDTDS.Engine;


namespace twoDTDS.Game
{
    /*---------------------------------------------------------------------------------------
                                PlayerAmmoSPRITE : SPRITE
    ---------------------------------------------------------------------------------------*/
    public class PlayerAmmoSprite : Sprite
    {
        ImageBrush src;
        public string[] uri = Asset.Paths;
        public double Width { get; set; }
        public double Height { get; set; }     

        /*============================= Rec << CTOR =========================*/
        public PlayerAmmoSprite(double width, double height)
        {
            src = new ImageBrush();
            src.ImageSource = new BitmapImage(new Uri(uri[2]));
            Width = width;
            Height = height;
        }

        /*============================= Render ==============================*/
        public override void Render(GameObject Parent, DrawingContext dc)
        {
            dc.DrawImage(src.ImageSource, new Rect(Parent.X, Parent.Y, Width, Height));
        }
    }
}
