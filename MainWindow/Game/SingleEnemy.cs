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
        DispatcherTimer dispense;
        Player player;

        public SingleEnemy(Map m, Player p) : base(m)
        {
            MoveToRandom();

            Width = 80;
            Height = 48;

            Sprite = new Rec(Width, Height);

            this.player = p;

            if (dispense == null)
            {
                dispense = new DispatcherTimer();
                dispense.Interval = TimeSpan.FromMilliseconds(75);
                dispense.Tick += delegate
                {
                    Map.AddObject(new ammo(Map, X + Width / 2, Y));
                };
            }
            dispense.Start();
        }

        public override void OnUpdate()
        {
            foreach (GameObject obj in Map.Objects)
            {
                if (!obj.ObDied && obj is ammo)
                {
                    if(IsHit(this, obj)){ player.myScore.ShotEnemy(ScoreKeep.Norm); }
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
