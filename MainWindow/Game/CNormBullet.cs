using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public class CNormBullet : EnemyAmmo
    {
        double x_vec;
        double y_vec;
        double radius;

        public CNormBullet(Map map, double x, double y, double x_vec,
                           double y_vec, double radius) : base(map){
            X = x;
            Y = y;

            this.x_vec = x_vec;
            this.y_vec = y_vec;
            this.radius = radius;

            Damage = Score.Norm;
            sprite = new Circle(new SolidColorBrush(Color.FromRgb(0, 255, 255)), radius);
        }

        public override void OnUpdate()
        {
            X += x_vec;
            Y += y_vec;

            CheckOutOfBounds();
        }
    }
}
