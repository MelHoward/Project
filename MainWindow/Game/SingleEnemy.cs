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
        int Health;
        string uri;

        /*========================  SinglEnemy  ===========================*/
        public SingleEnemy(Map m, Player p) : base(m)
        {
            MoveToRandom();
            Width = 80;
            Height = 48;
            Health = 100;
            uri = @"D:\\Documents\\Programs\\Project352\\Project\\MainWindow\\Assets\\Demon.png";
            Sprite = new Rec(Width, Height, uri);

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
        /// <summary>
        /// Enemies die
        /// </summary>
        /*======================== OnUpdate ================================*/
        public override void OnUpdate()
        {
            foreach (GameObject obj in Map.Objects)
            {
                if (!obj.ObDied && obj is Playerammo)
                {
                    if(IsHit(this, obj)){ player.myScore.ShotEnemy(ScoreKeep.Norm);
                        Health -= 25;
                    }
                }
                if(Health == 0)
                {
                    this.ObDied = true;
                    dispense.Stop();
                }
            }
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
