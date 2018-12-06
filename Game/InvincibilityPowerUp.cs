using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public class InvincibilityPowerUp : PowerUp
    {
        int InvincibilityFrames;

        public InvincibilityPowerUp(Map m, Player player, double X, double Y) : base(m)
        {
            p = player;
            this.X = X;
            this.Y = Y;
            Width = 40;
            Height = 40;
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
