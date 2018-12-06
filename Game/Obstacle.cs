using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    /*public class Obstacle : GameObject
    {
        //protected string uri;

        public Obstacle(Map m) : base(m)
        {
            
        }
        /*
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
    }*/

    public class Wall : GameObject
    {
        public double leftWall = 31;
        public double rightWall = 770;
        public double topWall = 70;
        public double botWall = 470;

        public Wall(Map m, double X, double Y, double Height, double Width) : base(m)
        {
            this.X = X;
            this.Height = Height;
            this.Y = Y;
            this.Width = Width;
        }

        public void HitLeftWall(GameObject other)
        {
            double playerLeft = other.X - Width;

            if (playerLeft < X)
            {
                other.X = X + 1;
            }
        }

        public void HitRightWall(GameObject other)
        {
            double playerRight = other.X + other.Width;

            if (playerRight >= X)
            {
                other.X = X - other.Width;
            }
        }

        public void HitTopWall(GameObject other)
        {
            double playerTop = other.Y - other.Height;

            if (playerTop <= topWall)
            {
                other.Y = topWall + 1;
            }
        }

        public void HitBotWall(GameObject other)
        {
            double playerBot = other.Y + other.Height;

            if(playerBot >= botWall)
            {
                other.Y = botWall - other.Height - 1;
            }
        }

        public void RightTunnel(GameObject other)
        {
            double playerRight = other.X + other.Width;
            double playerTop = other.Y - other.Height;
            double playerBot = other.Y;

            if (playerRight >= rightWall && (playerTop > 200 && playerBot < 270))
            {
                other.X = rightWall - playerRight + other.Width;
            }
        }
        public void LeftTunnel(GameObject other)
        {
            double playerRight = other.X + other.Width;
            double playerTop = other.Y - other.Height;
            double playerBot = other.Y;
            double playerLeft = other.X - Width;

            if (playerLeft <= leftWall && (playerTop > 200 && playerBot < 270))
            {
                other.X = 700;
            }
        }
    }
}
