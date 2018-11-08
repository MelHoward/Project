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
    public class SingleEnemy : GameObject
    {
        Engine.Random rand = new Engine.Random();
        List<AmmoInGame> bullets = new List<AmmoInGame>();
        DispatcherTimer dispancer;
        Player player;

        public SingleEnemy(Map m, Player p) : base(m)
        {
            MoveToRandom();

            Width = 80;
            Height = 48;

            sprite = new Rec(new SolidColorBrush(Color.FromRgb(0, 255, 0)), Width, Height);

            this.player = p;

            //bullets.Add(new CircleNormalBulle(this));
            bullets.Add(new CNormAmmo(this));

            dispancer = new DispatcherTimer();
            dispancer.Interval = TimeSpan.FromMilliseconds(600);
            dispancer.Tick += delegate
            {
                int bulletIndex = rand.Next(0, bullets.Count);
                double wait = bullets[bulletIndex].Shoot();
                dispancer.Interval = TimeSpan.FromMilliseconds(wait);
            };

            dispancer.Start();
        }

        public override void OnUpdate()
        {
            foreach (GameObject obj in Map.Objects)
            {
                if (!obj.ObDied && obj is ammo)
                {
                    if(isHit(this, obj)){ player.score.shotEnemy(Score.Norm); }
                }
            }
        }

        private void MoveToRandom()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += delegate
            {
                double x = rand.NextDouble(10, Map.Width - 10 - 80);

                double duration = Math.Abs(x - X) * 8;

                timer.Interval = TimeSpan.FromMilliseconds(duration);

                MoveTo(x, rand.NextDouble(10, 30), duration);
            };

            timer.Start();
        }
    }
}
