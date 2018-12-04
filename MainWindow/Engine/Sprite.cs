using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace twoDTDS.Engine
{

    /*---------------------------------------------------------------------------------------
                                           << Sprite >>
    ---------------------------------------------------------------------------------------*/
    public abstract class Sprite
    {
        public abstract void Render(GameObject parent, DrawingContext dc);
    }
/*---------------------------------------------------------------------------------------
                                        REC : Sprite
---------------------------------------------------------------------------------------*/
    public class Rec : Sprite
    {
        ImageBrush src;

        public Rec (int v1, int v2, string uri)
        {
        }

        public Rec (object p, double height, string uri)
        {
            Height = height;
        }

        public double Width { get; set; }
        public double Height { get; set; }

        /*============================= Rec << CTOR
            //Added a string uri to parameters to get sprite image for each sprite created
        public Rec(double width, double height, string uri)
        {
            src = new ImageBrush();
            
            Uri u = new Uri(uri);
            src.ImageSource = new BitmapImage(u);
            Width = width;
            Height = height;
        }

        /*============================= Render ==============================*/
    public override void Render(GameObject parent, DrawingContext dc)
        {
            if (parent != null)
            {
                dc.DrawImage(src.ImageSource, new Rect(parent.X, parent.Y, Width, Height));
            }
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
            //deep copy mh
            if (solColBrush != null) this.solColBrush = solColBrush.Clone();
            //no change mh
            this.solColBrush?.Freeze();
        }

        /*============================= Circle << CTOR ======================*/
        public Circle(Brush brush, double radius)
        {
            this.brush = brush;
            this.radius = radius;
        }

        /*============================= Render =================================*/

        public override void Render(GameObject parent, DrawingContext dc)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (dc != null) dc.DrawEllipse(brush, null, Point(parent.X + radius, parent.Y + radius), radius, radius);
        }

        private Point Point (double v1, double v2)
        {
            throw new NotImplementedException( );
        }
    }
}
