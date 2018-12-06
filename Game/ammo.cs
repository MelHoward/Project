
using twoDTDS.Engine;
/*
        Table of Contents
   ----------------------------------------------------
     * *Ammo (in twoDTDS.Game)
           + Ammo(Map m)
           + OnUpdate():void

   * Playerammo (in twoDTDS.Game)
           + Playerammo(Map m, float X, float Y)
           + OnUpdate():void

   * TempEnemyammo (in twoDTDS.Game)
            + TempEnemyammo(Map m, float X, float Y)
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
        
        public Playerammo(Map m, float X, float Y) : base(m)
        {
            this.X = (float) X;
            this.Y = (float) Y;
            Width = 6;
            Height = 15;            
            Sprite = new Rec(Width, Height, uri);
        }

        public override void OnUpdate()
        {
            if (direction == "up"){  Y -= 15;  }
            if (direction == "down"){ Y += 15; }
            if (direction == "left")  {  X -= 15; }
            if (direction == "right")  { X += 15; }

            if (direction == "upRight")
            {
                X += 15;
                Y -= 15;
            }
            if (direction == "upLeft")
            {
                X -= 15;
                Y -= 15;
            }
            if (direction == "downLeft")
            {
                X -= 15;
                Y += 15;
            }
            if (direction == "downRight")
            {
                X += 15;
                Y += 15;
            }
            if (Y < -100) { ObDied = true; }
        }
    }



    public class TempEnemyammo : Ammo
    {
        public TempEnemyammo(Map m, float X, float Y) : base(m)
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
