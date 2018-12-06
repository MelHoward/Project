using System.Windows;
using System.Globalization;
using System.Windows.Media;
using twoDTDS.Engine;

namespace twoDTDS.Game
{


    /*---------------------------------------------------------------------------------------
                                    LEVEL : MAP
    ---------------------------------------------------------------------------------------*/
    public class Level : Map
    {

        EnemyGenerator Enemy;
        /*========================= Level >> CTOR ===========================*/
        public Level(PlayArea play) : base(play)
        {
            Player Player= new Player(this);
            Enemy = new EnemyGenerator(this, Player);

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
