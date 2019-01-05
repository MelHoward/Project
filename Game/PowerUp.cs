using twoDTDS.Engine;

namespace twoDTDS.Game
{
/*---------------------------------------------------------------------------------------
                             << POWERUP : GAMEOBJECT >>
---------------------------------------------------------------------------------------*/
    public abstract class PowerUp : GameObject
    {
        protected Player p;
        protected bool pickedUp;

        public PowerUp(Map map) : base(map) { }
    }
}