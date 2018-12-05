using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
/*---------------------------------------------------------------------------------------
                       BULLETONPATH : ENEMYAMMO
---------------------------------------------------------------------------------------*/
    public class BulletOnPath : EnemyAmmo
    {
        GameObject target;
        double angle = 0;
        double angleOffset = new Engine.Random().NextDouble(-30, 30);
        double xVec = 0;
        double yVec = 0;
        double speed = 2.5;


        /*--------------------  BulletOnPath >> CTOR ----------------------------*/
        public BulletOnPath(Map map, GameObject parent, GameObject target,
                            double x, double y) : base(map)
        {
            this.target = target;
            Damage = ScoreKeep.Guid;

            X = parent.X + parent.Width / 2;
            Y = parent.Y + parent.Height / 2;

            Width = 6;
            Height = 6;

            Sprite = new Circle(new SolidColorBrush(Color.FromRgb(100, 190, 255)), 3);
            angle = GetAngleToTarget(target);
            SetAngle(angle + angleOffset);

            DispatcherTimer t = new DispatcherTimer();
            t.Interval = TimeSpan.FromSeconds(2.5);

            t.Tick += delegate
            {
                ObDied = true;
                t.Stop();
            };
        t.Start();
        }

        /*================================== GetAngleToTarget =============================*/
        private double GetAngleToTarget(GameObject target)
        {
            double xx = target.X - X;
            double yy = Y - target.Y;
            double angle = Math.Atan(Math.Abs(yy) / Math.Abs(xx)) / Math.PI * 180;

            if (xx < 0 && yy > 0)
                angle = 180 - angle;
            else if (xx < 0 && yy < 0)
                angle = 180 + angle; 
            else if (xx > 0 && yy < 0)
                angle = 360 - angle; 

            return angle;
        }

        /*================================== SetAngle =============================*/
        private void SetAngle(double angle)
        {
            angle = angle % 360;

            double angle_rad = angle / 180 * Math.PI;

            xVec = Math.Cos(angle_rad) * speed;
            yVec = -Math.Sin(angle_rad) * speed;
        }

        /*================================== OnUpdate =============================*/
        public override void OnUpdate()
        {
            double tAngle = GetAngleToTarget(target);

            angle = angle + (tAngle - angle) / 120;
            speed += 0.07;

            SetAngle(angle + angleOffset);

            X += xVec;
            Y += yVec;

            CheckOutOfBounds();
        }

        /*================================== RelativeDouble =============================*/
        private double RelativeDouble(double r)
        {
            if (Math.Abs(r) < 0.0001) {  return 0; }
            else return r;
        }
    }
}
