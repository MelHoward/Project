using twoDTDS.Engine;

namespace twoDTDS.Game
{
    /*---------------------------------------------------------------------------------------
                            << AmmoInGame >>
    ---------------------------------------------------------------------------------------*/
    public abstract class AmmoInGame
    {
        public Map Map { get; set; }
        public GameObject Parent { get; set; }

        /*--------------------  AmmoInGame >> CTOR ----------------------------*/
        public AmmoInGame(GameObject parent)
        {
            Parent = parent;
            Map = parent.Map;
        }

        public abstract double Shoot();
    }
}