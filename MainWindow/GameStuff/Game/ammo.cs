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
        //string uri;
        /*=============================== Ammo >> CTOR ======================*/
        public Ammo(Map m) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Width = 3;
            Height = 15;
     
            //Sprite = new Rec(Width, Height);
        }

        /*=============================== OnUpdate ==========================*/
        public override void OnUpdate()
        {
            Y -= 15;
            if (Y < -100) {  ObDied = true;  }
        }
    }
    /// <summary>
    /// Player ammo class that allows player ammo to have its own logic
    /// </summary>
    public class Playerammo : Ammo
    {
        public string direction;

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
            if (direction == "up") { Y -= 15; }
            if (direction == "down") { Y += 15; }
            if (direction == "left") { X -= 15; }
            if (direction == "right") { X += 15; }

            if (Y < -100) { ObDied = true; }
        }
    }
    /// <summary>
    /// Enemy ammo class that allows enemy ammo to have its own logic
    /// </summary>
    public class TempEnemyammo : Ammo
    {
        public int Damage { get; set; } = 10;

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
    /// <summary>
    /// This doesnt need to exist but it is used 
    /// </summary>
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
