using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public class Obstacle : GameObject
    {
        protected string uri;

        public Obstacle(Map m) : base(m)
        {
            
        }



        public void CollisionSetBackRight(GameObject other)
        {
            double leftX = X - other.Width,
                   rightX = X - Width,
                   bottomY = Y - other.Height,
                   topY = Y + Height;

            if (other.Width + other.X >= rightX)
            {
                other.X = rightX - 1;
            }
            if (other.Width + Width <= leftX)
            {
                other.X = leftX + 1;
            }
            
        }
       
    }

    public class Rock : Obstacle
    {
        public Rock(Map m, double X, double Y) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Height = 50;
            Width = 50;
            //uri = "http://pixelartmaker.com/art/da268f06e621b21.png";
            Sprite = new Rec(Width, Height, Asset.paths[2]);
        }

       /* public override bool IsHit(GameObject other)
        {
            double leftX = X - other.Width,
                   rightX = X + Width,
                   bottomY = Y - other.Height,
                   topY = Y + Height;

            if (other.X - 3 == rightX || other.X + 3 == leftX || other.Y - 3 == bottomY || other.Y + 3 == topY)
            {
                return true;
            }
            return false;
        }*/
    }
}
