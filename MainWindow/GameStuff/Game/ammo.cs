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
            ///Sprite = new Rec(Width, Height);
        }

        /*=============================== OnUpdate ==========================*/
        public override void OnUpdate()
        {
            Y -= 15;
            if (Y < -100) {  ObDied = true;  }
        }
    }


}
