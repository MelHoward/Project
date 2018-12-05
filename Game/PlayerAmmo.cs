using twoDTDS.Engine;

namespace twoDTDS.Game
{
/*--------------------------------------------------------------------------------------
                                PlayerAmmo : AMMO
---------------------------------------------------------------------------------------*/
    public class PlayerAmmo : Ammo
    {
        public string direction;
        /*============================= PlayerAmmo >> CTOR =========================*/
        public PlayerAmmo(Map m, double X, double Y) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Width = 6;
            Height = 15;
            Sprite = new PlayerAmmoSprite(Width, Height);
        }

        /*============================= OnUpdate() =========================*/
        public override void OnUpdate()
        {
            if (direction == "up") { Y -= 15; }
            if (direction == "down") { Y += 15; }
            if (direction == "left") { X -= 15; }
            if (direction == "right") { X += 15; }

            if (Y < -100) { ObDied = true; }
        }
    }

}
