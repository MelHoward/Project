using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    class EnemyMoveToPlayer : Enemy
    {
        public EnemyMoveToPlayer(Map m, Player p) : base(m, p)
        {
            MoveToPlayer();
            Width = 50;
            Height = 48;
            HitPoints = 4;
            uri = Asset.enemy[0];
            Sprite = new Rec(Width, Height, uri);
            Spawner();

            this.player = p;
        }

        private void MoveToPlayer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += delegate
            {
                double x = rand.NextDouble(100, 3000);
                double duration = rand.Next(500, 4000);
                timer.Interval = TimeSpan.FromMilliseconds(x);
                MoveTo(player.X, player.Y, duration);
            };
            timer.Start();
        }
    }
}
