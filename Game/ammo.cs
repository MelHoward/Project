
using twoDTDS.Engine;

/* *Ammo (in twoDTDS.Game)
        + Ammo(Map m)
        + OnUpdate():void
   * Playerammo (in twoDTDS.Game)
        + Playerammo(Map m, double X, double Y)
        + OnUpdate():void
   * TempEnemyammo (in twoDTDS.Game)
        + TempEnemyammo(Map m, double X, double Y)
        + OnUpdate():void
*/
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
            X = X;
            Y = Y;
            Width = 3;
            Height = 15;
        }

        /*=============================== OnUpdate ==========================*/
        public override void OnUpdate()
        {
            Y -= 15;
            if (Y < -100)
            {
                ObDied = true;
            }
        }
    }
    /// <summary>
    /// Player ammo class that allows player ammo to have its own logic
    /// </summary>
    public class Playerammo : Ammo
    {
        public string direction;
        public string uri = Asset.bullet[1];
        
        public Playerammo(Map m, double X, double Y) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Width = 6;
            Height = 15;
            //uri = "http://pixelartmaker.com/art/f59eaa826d4e49f.png";
            
            Sprite = new Rec(Width, Height, uri);
        }

        public override void OnUpdate()
        {
            if (direction == "up")
            {
                Y -= 15;
            }

            if (direction == "down")
            {
                Y += 15;
            }

            if (direction == "left")
            {
                X -= 15;
            }

            if (direction == "right")
            {
                X += 15;
            }

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
            Width = 5;
            Height = 5;
            Sprite = new Rec(Width, Height, Asset.bullet[0]);
        }

        public override void OnUpdate()
        {
            Y += 3;
            CheckOutOfBounds();
        }

    }
  
}
