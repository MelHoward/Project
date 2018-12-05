using twoDTDS.Engine;

namespace twoDTDS.Game
{
    /*---------------------------------------------------------------------------------------
                                TEMPENEMY : AMMO
    ---------------------------------------------------------------------------------------*/
    public class TempEnemyammo : Ammo
    {
        public int Damage { get; set; } = 10;

        /*============================= TempEnemyAmmo >> CTOR =========================*/
        public TempEnemyammo(Map m, double X, double Y) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Width = 15;
            Height = 15;
            Sprite = new PlayerAmmoSprite(Width, Height);
        }

        /*============================= OnUpdate =========================*/
        public override void OnUpdate()
        {
            Y += 5;
            if (Y < -100) { ObDied = true; }
        }
    }

}
