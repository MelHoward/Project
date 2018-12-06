using System.Windows;
using System.Windows.Media.Imaging;
using twoDTDS.Engine;

namespace twoDTDS.Game
{ 
/*---------------------------------------------------------------------------------------
                                     SPEEDPOWERUP : POWERUP 
---------------------------------------------------------------------------------------*/
    public class SpeedPowerUp : PowerUp
    {
        public int speedFrames = 0;

        public SpeedPowerUp(Map m, Player player, double X, double Y) : base(m)
        {
            p = player;
            this.X = (float) X;
            this.Y = (float) Y;
            Width = 40;
            Height = 40;
            string uri = Asset.env[1];
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
            else if (speedFrames == -1)
            {
                this.ObDied = true;
                player.speed = 3;
            }
            speedFrames--;

        }
        public override void OnUpdate()
        {
            SpeedUp(p);
            if (speedFrames <= 0)
                speedFrames--;
        }

        public override string ToString ()
        {
            return base.ToString( );
        }

        protected override void OnPropertyChanged (DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        protected override bool ShouldSerializeProperty (DependencyProperty dp)
        {
            return base.ShouldSerializeProperty(dp);
        }

        public override void SetRate ()
        {
            base.SetRate( );
        }

        public override void Duration (double durationMS)
        {
            base.Duration(durationMS);
        }

        public override void Render (WriteableBitmap img)
        {
            base.Render(img);
        }

        public override bool IsHit (GameObject other)
        {
            return base.IsHit(other);
        }
    }

}
