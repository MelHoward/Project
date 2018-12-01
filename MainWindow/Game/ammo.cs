using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
/*---------------------------------------------------------------------------------------
                            Ammo : GAMEOBJECT
---------------------------------------------------------------------------------------*/
    public class Ammo : GameObject
    {
        /*=============================== Ammo >> CTOR ======================*/
        public Ammo(Map m) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Width = 3;
            Height = 15;
            Sprite = new Rec(Width, Height);
        }

        /*=============================== OnUpdate ==========================*/
        public override void OnUpdate()
        {
            Y -= 15;
            if (Y < -100) {  ObDied = true;  }
        }
    }

    public class Playerammo : Ammo
    {
        public double upVel = 0;
        public double downVel = 0;
        public double leftVel = 0;
        public double rightVel = 0;

        public Playerammo(Map m, double X, double Y) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Width = 6;
            Height = 15;
            Sprite = new PlayerammoSprite(Width, Height);
        }

        public override void OnUpdate()
        {
            Y -= upVel;
            Y += downVel;
            X += rightVel;
            X -= leftVel;
            if (Y < -100) { ObDied = true; }
        }
    }

    public class TempEnemyammo : Ammo
    {

        public TempEnemyammo(Map m, double X, double Y) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Width = 15;
            Height = 15;
            Sprite = new PlayerammoSprite(Width, Height);
        }

        public override void OnUpdate()
        {
            Y += 5;
            if (Y < -100) { ObDied = true; }
        }
    }

    public class PlayerammoSprite : Sprite
    {
        ImageBrush src;
        public double Width { get; set; }
        public double Height { get; set; }

        /*============================= Rec << CTOR =========================*/
        public PlayerammoSprite(double width, double height)
        {
            src = new ImageBrush();

            Uri u = new Uri("http://pixelartmaker.com/art/f59eaa826d4e49f.png");
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
}
