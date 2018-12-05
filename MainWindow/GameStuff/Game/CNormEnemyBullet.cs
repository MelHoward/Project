﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
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
            double y_vec, double radius) : base(map)
        {
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
}