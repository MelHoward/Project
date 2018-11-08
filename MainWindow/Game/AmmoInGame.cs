using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public abstract class AmmoInGame
    {
        public Map Map { get; set; }
        public GameObject Parent { get; set; }

        public AmmoInGame(GameObject parent)
        {
            Parent = parent;
            Map = parent.Map;
        }
        public abstract double Shoot();
    }

}
