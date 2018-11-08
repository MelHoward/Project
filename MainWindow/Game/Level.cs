using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public class Level : Map
    {
        Player Player;
        EnemyGenerator Enemy;

        public Level(PlayArea play) : base(play)
        {
            Player = new Player(this);
            Enemy = new EnemyGenerator(this, Player);

            Objects.Add(Player);
            Objects.Add(Enemy);
        }

        public override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            dc.DrawText(new FormattedText("Score: " +
                        Player.score.Sc.ToString(),
                        System.Globalization.CultureInfo.CurrentCulture,
                        System.Windows.FlowDirection.LeftToRight,
                        Default.Typeface, 12, Brushes.Black),
                        new System.Windows.Point(5, 5));
        }
    }
}
