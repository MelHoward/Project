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

        bool hit = false;
        bool leftOrRight = false;
        bool sideHit = false;
        bool botTopHit = false;


        public void CollisionSetBack(GameObject other)
        {
            double leftX = X - Width,
                   rightX = X + Width + 1,
                   bottomY = Y - other.Height,
                   topY = Y + Height;
            

            if (other.X - Width < X && other.X < X)
            {
                other.X = other.X - 1;
                hit = true;
                leftOrRight = false;
                sideHit = true;
                
            }
            if (other.X + Width > X && other.X > X)
            {
                other.X = other.X + 1;
                hit = true;
                leftOrRight = true;
                hit = false;
               
            }
            
     
        }
       
        public void checkTopBot(GameObject other)
            {

            double leftX = X - Width,
                   rightX = X + Width + 1,
                   bottomY = Y - other.Height,
                   topY = Y + Height;

            if (Y > other.Height + Height && (other.Y > Y &&  other.Y - other.Height  > Y - other.Height))
            {
                other.Y = other.Y + 1;
                hit = true;
                if (other.X < X && (other.Y + other.Height < Y - other.Height && other.Y - Height > Y + Height))
                {
                    other.X += 1;
                }
                else if(other.X > X && (other.Y + other.Height < Y - other.Height && other.Y - Height > Y + Height))
                    {
                    other.X -= 1;
                }
            }
            if (other.Y - other.Height > Height && Y > other.Y - other.Height && other.Y + other.Height > Y - Height)
            {
                other.Y = other.Y - 1;
                hit = true;


                if (other.X < X && (other.Y + other.Height < Y - other.Height && other.Y - Height > Y + Height))
                {
                    other.X += 1;
                }
                else if(other.X > X && (other.Y + other.Height < Y - other.Height && other.Y - Height > Y + Height))
                {
                    other.X -= 1;
                }
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
