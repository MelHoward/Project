using System.Windows;
using System.Globalization;
using System.Windows.Media;
using twoDTDS.Engine;

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
            Map.AddObject(new SingleEnemy(m, p));
        }
    }

/*---------------------------------------------------------------------------------------
                                LEVEL : MAP
---------------------------------------------------------------------------------------*/
    public class Level : Map
    {
        Player player;
        EnemyGenerator Enemy;

        /*========================= Level >> CTOR ===========================*/
        public Level(PlayArea play) : base(play)
        {
            player = new Player(this);
            Enemy = new EnemyGenerator(this, player);
            Objects.Add(player);
            Objects.Add(Enemy);
        }

        /*============================= OnRender ===========================+*/
        public override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawText(new FormattedText("Score: " + player.myScore.Sc.ToString(),
                        CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        Default.Typeface, 15, Brushes.White), new Point(-93, 90));

            dc.DrawText(new FormattedText("HP: " + player.myScore.HP.ToString(),
                        CultureInfo.CurrentCulture, FlowDirection.LeftToRight, 
                        Default.Typeface, 15, Brushes.White), new Point(-93, 120));
        }
    }
}
