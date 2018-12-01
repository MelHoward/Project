
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

    public abstract class PlayerAmmo : GameObject
    {
        public PlayerAmmo(Map map) : base(map) { }
        public int Damage { get; set; } = 1;
    }

/*---------------------------------------------------------------------------------------
                                CNORMBULLET : ENEMYAmmo
---------------------------------------------------------------------------------------*/
    public class CNormEnemyBullet : EnemyAmmo
    {
        double x_vec;
        double y_vec;
        double radius;

        /*============================= CNormBullet =========================*/
        public CNormEnemyBullet(Map map, double x, double y, double x_vec,
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

    public class CNormPlayerBullet : PlayerAmmo
    {
        public double upVel = 0;
        public double downVel = 0;
        public double leftVel = 0;
        public double rightVel = 0;

        /*============================= CNormBullet =========================*/
        public CNormPlayerBullet(Map map, double x, double y) : base(map)
        {
            this.X = X;
            this.Y = Y;
            Width = 3;
            Height = 15;
            Sprite = new Rec(Width, Height);
        }

        /*=============================== OnUpdate ==========================*/
        public override void OnUpdate()
        {
            Y -= upVel;
            Y += downVel;
            X += rightVel;
            X -= leftVel;
            if (Y < -100) { ObDied = true; }
        }
    }


    /*---------------------------------------------------------------------------------------
                                 CNORMAMMO : AMMOInGAME
    ---------------------------------------------------------------------------------------*/
    public class CNormAmmo : AmmoInGame
    {
        Engine.Random r = new Engine.Random();
        public CNormAmmo(GameObject parent) : base(parent) { }

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
