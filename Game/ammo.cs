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
            /*/string uri = "http://pixelartmaker.com/art/f59eaa826d4e49f.png";*/
            Sprite = new Rec(Width, Height, Asset.paths[0]);
        }

        public override void OnUpdate()
        {
            if (direction == "up") { Y -= 15; }
            if (direction == "down") { Y += 15; }
            if (direction == "left") { X -= 15; }
            if (direction == "right") { X += 15; }
            if (direction == "upLeft") { X -= 15; Y -= 15; }
            if (direction == "upRight") { X += 15; Y -= 15; }
            if (direction == "downLeft") { X -= 15; Y += 15; }
            if (direction == "downRight") { X += 15; Y += 15; }

            if (Y < -100) { ObDied = true; }
        }
    }
    /// <summary>
    /// Enemy ammo class that allows enemy ammo to have its own logic
    /// </summary>
    public class TempEnemyammo : Ammo
    {
        public TempEnemyammo(Map m, double X, double Y) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Width = 15;
            Height = 15;
            /*string uri = "http://pixelartmaker.com/art/f59eaa826d4e49f.png";*/
            Sprite = new Rec(Width, Height, Asset.paths[2]);
        }

        public override void OnUpdate()
        {
            Y += 3;
            CheckOutOfBounds();
            if(Y > Map.Height)
            {
                this.ObDied = true;
            }
        }

    }
  
}
