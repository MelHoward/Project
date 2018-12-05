using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using Brush = System.Windows.Media.Brush;
using Point = System.Windows.Point;

namespace twoDTDS.Engine
{
    /*---------------------------------------------------------------------------------------
                                           << Sprite >>
    ---------------------------------------------------------------------------------------*/
    public abstract class Sprite
    {
        public abstract void Render(GameObject Parent, DrawingContext dc);
    }

/*---------------------------------------------------------------------------------------
                                        REC : Sprite
---------------------------------------------------------------------------------------*/
    public class Rec : Sprite
    {
        ImageBrush src;
        public double Width { get; set; }
        public double Height { get; set; }

        /*============================= Rec << CTOR =========================*/
        public Rec(double width, double height, string uri)
        {
            src = new ImageBrush();
            
            Uri u = new Uri(uri);
            src.ImageSource = new BitmapImage(u);
            Width = width;
            Height = height;

        }

        /*============================= Render ==============================*/
        public override void Render(GameObject Parent, DrawingContext dc)
        {
            dc.DrawImage(src.ImageSource, new Rect(Parent.X, Parent.Y, Width, Height));
        }
    }

    /*---------------------------------------------------------------------------------------
                                  CIRCLE : Sprite
    ---------------------------------------------------------------------------------------*/
    public class Circle : Sprite
    {
        double radius = 5;
        Brush brush;
        private SolidColorBrush solColBrush;

        /*============================= Circle << CTOR ======================*/
        public Circle(SolidColorBrush solColBrush)
        {
            this.solColBrush = solColBrush.Clone();
            this.solColBrush.Freeze();
        }

        /*============================= Circle << CTOR ======================*/
        public Circle( Brush brush, double radius)
        {
            this.brush = brush;
            this.radius = radius;
        }

        /*============================= Render =================================*/
        public override void Render(GameObject Parent, DrawingContext dc)
        {
            dc.DrawEllipse(brush, null, new Point(Parent.X + radius, Parent.Y + radius), radius, radius);
        }
    }
}
