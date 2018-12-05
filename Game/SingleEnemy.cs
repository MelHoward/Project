using System;
using System.Collections.Generic;
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
        int frames;
        string[] uri = Asset.Paths;

        /*========================  SingleEnemy  ===========================*/
        public SingleEnemy(Map m, Player p) : base(m)
        {
            MoveToRandom();
            Width = 80;
            Height = 48;
            Health = 400;
            Sprite = new Rec(Width, Height, Asset.Paths[1]);

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

        /*======================== OnUpdate ================================*/
        public override void OnUpdate()
        {
            foreach (GameObject obj in Map.Objects)
            {
                if (!obj.ObDied && obj is PlayerAmmo)
                {
                    if(IsHit(this, obj))
                    {
                        player.myScore.ShotEnemy(ScoreKeep.Norm);
                        Health -= 25;
                        EnemyHit(this);
                    }
                }
                if(Health == 0)
                {
                    this.ObDied = true;
                    dispense.Stop();
                }
                if(frames == 30)
                {
                    frames = 0;
                    Sprite = new Rec(Width, Height, Asset.Paths[1]);
                    Width = 80;
                    Height = 48;
                }
                frames++;
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

        /*=============================== EnemyHit ======================*/
        private void EnemyHit(GameObject enemy)
        {
            if(frames < 30)
            {
                Sprite = null;
                enemy.Width = 0;
                enemy.Height = 0;
            }
            
        }
    }
}
