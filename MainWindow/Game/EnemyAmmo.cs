using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public abstract class EnemyAmmo : GameObject
    {
        public EnemyAmmo(Map map) : base(map) { }
        public int Damage { get; set; } = 1;
    }
}

