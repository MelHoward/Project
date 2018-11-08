using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace twoDTDS.Engine
{
    public class Rec : Sprite
    {
        private Brush brush;

        public double Width { get; set; }
        public double Height { get; set; }

        public Rec(Brush Brush, double width, double height)
        {
            brush = Brush.Clone();
            brush.Freeze();

            Width = width;
            Height = height;
        }

        public override void Render(GameObject Parent, DrawingContext dc)
        {
            dc.DrawRectangle(brush, null, new System.Windows.Rect
                            (Parent.X, Parent.Y, Width, Height));
        }
    }
}
