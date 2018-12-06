using System.Windows;
using System.Globalization;
using System.Windows.Media;
using twoDTDS.Engine;
using System.Windows.Threading;
using System;

namespace twoDTDS.Game
{

    /*---------------------------------------------------------------------------------------
                                    ENEMYGENERATOR : GAMEOBJECT
    ---------------------------------------------------------------------------------------*/
    public class EnemyGenerator : GameObject
    {
        public int EnemiesSpawned = 0;
        public int EnemyCap = 1;
        private double spawnRate = 3;
        /*==================== EnemyGenerator >> CTOR =======================*/
        public EnemyGenerator(Map m, Player p) : base(m)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(spawnRate);
            timer.Tick += delegate
            {
                setSpawnRate();
                timer.Interval = TimeSpan.FromSeconds(spawnRate);
                createEnemy(m, p);
                EnemiesSpawned++;
                if(EnemiesSpawned == EnemyCap)
                {
                    timer.Stop();
                }
            };
            timer.Start();
        }

        private void setSpawnRate()
        {
            if(EnemiesSpawned < 10)
            {
                spawnRate = 3;
            }
            if(EnemiesSpawned > 10 && EnemiesSpawned < 25)
            {
                spawnRate = 2;
            }
            else
            {
                spawnRate = 1.5;
            }
        }

        private void createEnemy(Map m, Player p)
        {
            Engine.Random rand = new Engine.Random();
            double generator = rand.NextDouble(0, 100);

            if(generator <= 20)
            {
                Map.AddObject(new EnemyMoveToRandom(m, p));
            }
            if(generator > 20)
            {
                Map.AddObject(new EnemyMoveToPlayer(m,p));
            }
        }
    }

/*---------------------------------------------------------------------------------------
                                LEVEL : MAP
---------------------------------------------------------------------------------------*/
    public class Level : Map
    {
        Player Player;
        EnemyGenerator Enemy;

        /*========================= Level >> CTOR ===========================*/
        public Level(PlayArea play) : base(play)
        {
            Player = new Player(this);
            Enemy = new EnemyGenerator(this, Player);
            Objects.Add(Player);
            Objects.Add(Enemy);
        }

        /*============================= OnRender ===========================+*/
        public override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawText(new FormattedText("Score: " + Player.myScore.Sc.ToString(),
                        CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        Default.Typeface, 12, Brushes.White), new Point(-93, 90));

            dc.DrawText(new FormattedText("HP: " + Player.myScore.HP.ToString(),
                        CultureInfo.CurrentCulture, FlowDirection.LeftToRight, 
                        Default.Typeface, 12, Brushes.White), new Point(-93,120));

        }
    }
}
