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
        public int EnemyCap = 5;
        /*==================== EnemyGenerator >> CTOR =======================*/
        public EnemyGenerator(Map m, Player p) : base(m)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += delegate
            {
                timer.Interval = TimeSpan.FromSeconds(5);
                Map.AddObject(new SingleEnemy(m, p));
                EnemiesSpawned++;
                if(EnemiesSpawned == EnemyCap)
                {
                    timer.Stop();
                }
            };
            timer.Start();
        }
    }

/*---------------------------------------------------------------------------------------
                                LEVEL : MAP
---------------------------------------------------------------------------------------*/
    public class Level : Map
    {
        Player Player;
        EnemyGenerator Enemy;
        bool LevelComplete;

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

            if (Enemy.EnemiesSpawned == Enemy.EnemyCap)
            {
                LevelComplete = true;
            }
        }
    }
}
