﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public abstract class PowerUp : GameObject
    {
        protected Player p;
        protected bool pickedUp;

        public PowerUp(Map map) : base(map)
        {
           
        }
    }

    public class SpeedPowerUp : PowerUp
    {
        public int speedFrames = 0;

        public SpeedPowerUp(Map m, Player player, double X, double Y) : base(m)
        {
            p = player;
            this.X = X;
            this.Y = Y;
            Width = 20;
            Height = 20;
            string uri = "http://pixelartmaker.com/art/f59eaa826d4e49f.png";
            Sprite = new Rec(Width, Height, uri);
            pickedUp = false;
        }

        public void SpeedUp(Player player)
        {

            foreach (GameObject obj in Map.Objects)
            {
                if (!obj.ObDied && obj is SpeedPowerUp)
                {
                    if (IsHit(player, obj))
                    {
                        if (pickedUp == false)
                        {
                            speedFrames = 300;
                            obj.Sprite = null;
                            obj.Width = 0;
                            obj.Height = 0;
                            pickedUp = true;
                        }
                    }
                }
            }

            if (speedFrames > 0)
            {
                player.speed = 5;
            }
            else if(speedFrames == -1)
            {
                this.ObDied = true;
                player.speed = 3;
            }

            speedFrames--;
           
        }

        public override void OnUpdate()
        {
            SpeedUp(p);
            if(speedFrames <= 0)
            speedFrames--;
        }
    }

    public class InvincibilityPowerUp : PowerUp
    {
        int InvincibilityFrames;

        public InvincibilityPowerUp(Map m, Player player, double X, double Y) : base(m)
        {
            p = player;
            this.X = X;
            this.Y = Y;
            Width = 20;
            Height = 20;
            string uri = "http://pixelartmaker.com/art/f59eaa826d4e49f.png";
            Sprite = new Rec(Width, Height, uri);
            pickedUp = false;
        }

        public void BecomeInvincible(Player player)
        {
            foreach (GameObject obj in Map.Objects)
            {
                if (!obj.ObDied && obj is InvincibilityPowerUp)
                {
                    if (IsHit(player, obj))
                    {
                        if (pickedUp == false)
                        {
                            InvincibilityFrames = 300;
                            obj.Sprite = null;
                            obj.Width = 0;
                            obj.Height = 0;
                            pickedUp = true;
                        }
                    }
                }
            }

            if (InvincibilityFrames > 0)
            {
                player.invincible = true;
                if (InvincibilityFrames % 2 == 0)
                {
                    player.Sprite = null;
                }
                else
                {
                    player.Sprite = new Rec(40, 45, player.uri);
                }
            }
            else if (InvincibilityFrames == -1)
            {
                player.invincible = false;
                this.ObDied = true;
            }

            InvincibilityFrames--;
        }

        public override void OnUpdate()
        {
            BecomeInvincible(p);
            if (InvincibilityFrames <= 0)
                InvincibilityFrames--;
        }
    }

}
