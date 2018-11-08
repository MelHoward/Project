using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twoDTDS.Engine;

namespace twoDTDS.Game 
{
    public class EnemyGenerator : GameObject
    {
        public EnemyGenerator(Map m, Player p) : base(m)
        {
            Map.AddObject(new SingleEnemy(m, p));
        }
    }
}
