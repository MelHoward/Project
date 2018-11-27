using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
        private Brush brush;

        public double Width { get; set; }
        public double Height { get; set; }

        /*============================= Rec << CTOR =========================*/
        public Rec(Brush Brush, double width, double height)
        {
            brush = Brush.Clone();
            brush.Freeze();

            Width = width;
            Height = height;
        }

        /*============================= Render =================================*/
        public override void Render(GameObject Parent, DrawingContext dc)
        {
            dc.DrawRectangle(brush, null, new System.Windows.Rect
                            (Parent.X, Parent.Y, Width, Height));
        }
    }

/*---------------------------------------------------------------------------------------
                              CIRCLE : Sprite
---------------------------------------------------------------------------------------*/
    public class Circle : Sprite
    {
        double radius = 5;
        Brush brush;
        private SolidColorBrush solidColorBursh;

        /*============================= Circle << CTOR ======================*/
        public Circle(SolidColorBrush solidColorBursh)
        {
            //deep copy mh
            this.solidColorBursh = solidColorBursh.Clone();
            //no change mh
            this.solidColorBursh.Freeze();
        }

        /*============================= Circle << CTOR ======================*/
        public Circle(Brush brush, double radius)
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
