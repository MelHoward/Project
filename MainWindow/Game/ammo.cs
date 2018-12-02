using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
/*---------------------------------------------------------------------------------------
                            Ammo : GAMEOBJECT
---------------------------------------------------------------------------------------*/
    public class Ammo : GameObject
    {
        /*=============================== Ammo >> CTOR ======================*/
        public Ammo(Map m, double X, double Y) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Width = 3;
            Height = 15;
            Sprite = new Rec("D:\\Documents\\Programs\\Project352\\Project\\MainWindow\\img\\up.png", Width, Height);
        }

        /*=============================== OnUpdate ==========================*/
        public override void OnUpdate()
        {
            Y -= 15;
            if (Y < -100) {  ObDied = true;  }
        }
    }
}
