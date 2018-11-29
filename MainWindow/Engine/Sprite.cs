using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows;

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
        public Rec(double width, double height)
        {
            src = new ImageBrush();

            src.ImageSource = new BitmapImage(, UriKind.RelativeOrAbsolute));
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
        System.Windows.Media.Brush brush;
        private SolidColorBrush solColBrush;

        /*============================= Circle << CTOR ======================*/
        public Circle(SolidColorBrush solColBrush)
        {
            //deep copy mh
            this.solColBrush = solColBrush.Clone();
            //no change mh
            this.solColBrush.Freeze();
        }

        /*============================= Circle << CTOR ======================*/
        public Circle( System.Windows.Media.Brush brush, double radius)
        {
            this.brush = brush;
            this.radius = radius;
        }

        /*============================= Render =================================*/

        public override void Render(GameObject Parent, DrawingContext dc)
        {
            dc.DrawEllipse(brush, null, new System.Windows.Point(Parent.X + radius, Parent.Y + radius), radius, radius);
        }
    }
}
