using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        int HitPoints;
        int frames;
        string uri;
        string spawnPoint = "down";

        /*========================  SinglEnemy  ===========================*/
        public SingleEnemy(Map m, Player p) : base(m)
        {
            MoveToRandom();
            Width = 80;
            Height = 48;
            HitPoints = 4;
            uri = @"C:\Users\Corey\Source\Repos\Project\MainWindow\Assets\Demon.png";
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
                    if(IsHit(this, obj))
                    {
                        player.myScore.ShotEnemy(ScoreKeep.Norm);
                        obj.ObDied = true;
                        HitPoints -= 1;
                        EnemyHit(this);
                    }
                }
                if(HitPoints == 0)
                {
                    this.ObDied = true;
                    dispense.Stop();
                }
                if(frames == 30)
                {
                    frames = 0;
                    Sprite = new Rec(Width, Height, uri);
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

        private void Spawner()
        {
            System.Random spawn = new System.Random();

            if(spawnPoint == "top")
            {
                X = 400;
            }
            if(spawnPoint == "left")
            {
                Y = 300;
            }
            if(spawnPoint == "right")
            {
                Y = 300;
                X = 750;
            }
            if(spawnPoint == "down")
            {
                Y = 500;
                X = 400;
            }
        }

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
