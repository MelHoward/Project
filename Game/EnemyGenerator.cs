using System;
using System.Windows.Threading;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    /*---------------------------------------------------------------------------------------
                                    ENEMYGENERATOR : GAMEOBJECT
    ---------------------------------------------------------------------------------------*/
    public class EnemyGenerator : GameObject
    {
        public int EnemiesSpawned = 0;
        public int EnemyCap = 50;
        private double spawnRate = 3;

        /*==================== EnemyGenerator >> CTOR =======================*/
        public EnemyGenerator(Map m, Player p) : base(m)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(spawnRate);
            timer.Tick += delegate
            {
                SetSpawnRate();
                timer.Interval = TimeSpan.FromSeconds(spawnRate);
                CreateEnemy(m, p);
                EnemiesSpawned++;
                if (EnemiesSpawned == EnemyCap)
                {
                    timer.Stop();
                }
            };
            timer.Start();
        }

        /*==================== SetSpawnRate =======================*/
        private void SetSpawnRate()
        {
            if (EnemiesSpawned < 10)  {  spawnRate = 3;  }
            if (EnemiesSpawned > 10 && EnemiesSpawned < 25) {  spawnRate = 2;  }
            else {  spawnRate = 1.5;  }
        }

        /*==================== CreateEnemy =======================*/
        private void CreateEnemy(Map m, Player p)
        {
            Engine.Random rand = new Engine.Random();
            double generator = rand.NextDouble(0, 100);
            if (generator <= 20)
            {
                Map.AddObject(new EnemyMoveToRandom(m, p));
            }
            if (generator > 20)
            {
                Map.AddObject(new EnemyMoveToPlayer(m, p));
            }
        }
    }
}
