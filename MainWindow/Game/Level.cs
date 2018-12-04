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
        /*==================== EnemyGenerator >> CTOR =======================*/
        public EnemyGenerator(Map m, Player p) : base(m)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += delegate
            {
                timer.Interval = TimeSpan.FromSeconds(5);
                Map.AddObject(new SingleEnemy(m, p));
            };
            timer.Start();
        }
    }   
/*---------------------------------------------------------------------------------------
                                LEVEL : MAP
---------------------------------------------------------------------------------------*/
    public class Level : Map
    {
        Player Ppayer;
        readonly EnemyGenerator Enemy;

        /*========================= Level >> CTOR ===========================*/
        public Level(PlayArea play) : base(play)
        {
            Player1 = new Player(this);
            Enemy = new EnemyGenerator(this, Player1);
            Objects.Add(Player1);
            Objects.Add(Enemy);
        }
        public Player Player1 { get => Player2; set => Player2 = value; }

        public EnemyGenerator Enemy1 => Enemy;

        public Player Player2 { get => Player5; set => Player5 = value; }
        public Player Player3 { get => Player5; set => Player5 = value; }
        public Player Player4 { get => Player5; set => Player5 = value; }
        public Player Player5 { get => Player7; set => Player7 = value; }
        public Player Player6 { get => Player7; set => Player7 = value; }
        public Player Player7 { get { return Player; } set => Player = value; }

        /*============================= OnRender ===========================+*/
        public override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawText(new FormattedText("Score: " + Player1.myScore.Sc.ToString(),
                        CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        Default.Typeface, 12, Brushes.White), new Point(-93, 90));

            dc.DrawText(new FormattedText("HP: " + Player1.myScore.HP.ToString(),
                        CultureInfo.CurrentCulture, FlowDirection.LeftToRight, 
                        Default.Typeface, 12, Brushes.White), new Point(-93,120));
        }
    }
}
