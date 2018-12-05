using System;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    /*---------------------------------------------------------------------------------------
                                 CNORMAMMO : AMMOInGAME
    ---------------------------------------------------------------------------------------*/
    public class CNormAmmo : AmmoInGame
    {
        Engine.Random r = new Engine.Random();

        public CNormAmmo(GameObject parent) : base(parent)
        {
        }

        /*================================= Shoot ===========================*/
        public override double Shoot()
        {
            double count = r.Next(8, 50);
            double baseAngle = r.NextDouble(0, 360);
            for (int i = 0; i < count; i++)
            {
                double angle = (360 / count) * i + baseAngle;
                angle = angle % 360;

                double angle_rad = angle / 180 * Math.PI;
                double speed = 3;

                double xvec = Math.Cos(angle_rad) * speed;
                double yvec = Math.Sin(angle_rad) * speed;
                Map.AddObject(new CNormEnemyBullet(Map, Parent.X + Parent.Width / 2,
                    Parent.Y + Parent.Height / 2, xvec, yvec, 3));
            }

            return 280;
        }
    }
}
