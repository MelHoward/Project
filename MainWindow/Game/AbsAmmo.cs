using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public abstract class Bulleter
    {
        public Map map { get; set; }
        public GameObject Parent { get; set; }

        public Bulleter(GameObject parent)
        {
            Parent = parent;

            map = parent.map;
        }

        public abstract double Shoot();
    }
}


