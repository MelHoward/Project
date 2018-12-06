using System;
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
        float angle = 0;
        double angleOffset = new Engine.Random().NextDouble(-30, 30);
        float xVec = 0;
        float yVec = 0;
        double speed = 2.5;

        /*--------------------  BulletOnPath >> CTOR ----------------------------*/
        public BulletOnPath(Map map, GameObject parent, GameObject target,
                                               float x, float y) : base(map)
        {
            this.target = target;
            Damage = ScoreKeep.Guide;

            X = parent.X + parent.Width / 2;
            Y = parent.Y + parent.Height / 2;

            Width = 6;
            Height = 6;

            Sprite = new Circle(new SolidColorBrush(Color.FromRgb(120, 190, 255)), 3);
            angle = GetAngleToTarget(target);
            SetAngle((float) (angle + angleOffset));
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
        private float GetAngleToTarget(GameObject target)
        {
            float xx = target.X - X;
            float yy = Y - target.Y;
            double angle = Math.Atan(Math.Abs(yy) / Math.Abs(xx)) / Math.PI * 180;

            if (xx < 0 && yy > 0)
                angle = 180 - angle;
            else if (xx < 0 && yy < 0)
                angle = 180 + angle; 
            else if (xx > 0 && yy < 0)
                angle = 360 - angle; 
            return (float) angle;
        }

        /*================================== SetAngle =============================*/
        private void SetAngle(float angle)
        {
            angle = angle % 360;

            double angle_rad = angle / 180 * Math.PI;

            xVec = (float) (Math.Cos(angle_rad) * speed);
            yVec = (float) (-Math.Sin(angle_rad) * speed);
        }

        /*================================== OnUpdate =============================*/
        public override void OnUpdate()
        {
            float tAngle = (float) GetAngleToTarget(target);
            angle = angle + (tAngle - angle) / 120;
            speed += 0.07;
            SetAngle((float) (angle + angleOffset));

            X += xVec;
            Y += yVec;

            CheckOutOfBounds();
        }

        /*================================== Relativefloat =============================*/
        private float Relativefloat(float r)
        {
            if (Math.Abs(r) < 0.0001) {  return 0; }
            else return r;
        }
    }
}
