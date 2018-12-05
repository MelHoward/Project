using twoDTDS.Engine;

namespace twoDTDS.Game
{

    /*---------------------------------------------------------------------------------------
                                   << ENEMYAMMO >> : GAMEOBJECT
    ---------------------------------------------------------------------------------------*/
    public abstract class EnemyAmmo : GameObject
    {
        public EnemyAmmo(Map map) : base(map) { }
        public int Damage { get; set; } = 1;
    }



}
