using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    class EnemyMoveToRandom : Enemy
    {
        public EnemyMoveToRandom(Map m, Player p) : base(m, p)
        {
            MoveToRandom();
            Width = 50;
            Height = 48;
            HitPoints = 2;
            uri = Asset.enemy[0];
            Sprite = new Rec(Width, Height, uri);
            Spawner();

            this.player = p;

            if (dispense == null)
            {
                dispense = new DispatcherTimer();
                dispense.Interval = TimeSpan.FromMilliseconds(400);
                dispense.Tick += delegate
                {
                    Map.AddObject(new TempEnemyammo(Map, X + Width / 2, Y));
                };
            }
            dispense.Start();
        }

        /*=============================== MoveToRandom ======================*/
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
