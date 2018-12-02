
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
                               << ENEMYAMMO >> : GAMEOBJECT
---------------------------------------------------------------------------------------*/
    public abstract class EnemyAmmo : GameObject
    {
        public EnemyAmmo(Map map) : base(map) { }
        public int Damage { get; set; } = 1;
    }

/*---------------------------------------------------------------------------------------
                                CNORMBULLET : ENEMYAmmo
---------------------------------------------------------------------------------------*/
    public class CNormBullet : EnemyAmmo
    {
        double x_vec;
        double y_vec;
        double radius;

        /*============================= CNormBullet =========================*/
        public CNormBullet(Map map, double x, double y, double x_vec,
                           double y_vec, double radius) : base(map){
            X = x;
            Y = y;

            this.x_vec = x_vec;
            this.y_vec = y_vec;
            this.radius = radius;

            Damage = ScoreKeep.Norm;
            Sprite = new Circle(new SolidColorBrush(Color.FromRgb(0, 255, 255)), radius);
        }

        /*=============================== OnUpdate ==========================*/
        public override void OnUpdate()
        {
            X += x_vec;
            Y += y_vec;

            CheckOutOfBounds();
        }
    }


    ///*---------------------------------------------------------------------------------------
    //                             CNORMAMMO : AMMOInGAME
    //---------------------------------------------------------------------------------------*/
    //public class CNormAmmo : AmmoInGame
    //{
    //    Engine.Random r = new Engine.Random();
    //    public CNormAmmo(GameObject parent) : base(parent) { }

    //    /*================================= Shoot ===========================*/
    //    public override double Shoot()
    //    {
    //        double count = r.Next(8, 50);
    //        double baseAngle = r.NextDouble(0, 360);
    //        for (int i = 0; i < count; i++)
    //        {
    //            double angle = (360 / count) * i + baseAngle;
    //            angle = angle % 360;

    //            double angle_rad = angle / 180 * Math.PI;
    //            double speed = 3;

    //            double xvec = Math.Cos(angle_rad) * speed;
    //            double yvec = Math.Sin(angle_rad) * speed;

    //            Map.AddObject(new CNormBullet(Map, Parent.X + Parent.Width / 2,
    //                          Parent.Y + Parent.Height / 2, xvec, yvec, 3));
    //        }
    //        return 280;
    //    }
    //}
}
