using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public abstract class AbsBullet
    {
        public Map m { get; set; }
        public GameObject Parent { get; set; }

        public AbsBullet(GameObject parent)
        {
            Parent = parent;
            Map = parent.Map;
        }
        public abstract double Shoot();
    }
}
