using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using twoDTDS.Engine;

namespace twoDTDS.Game
{

/*---------------------------------------------------------------------------------------
                                    ENEMY : GAMEOBJECT 
---------------------------------------------------------------------------------------*/
    public class Enemy : GameObject
    {
        protected Engine.Random rand = new Engine.Random();
        protected List<AmmoInGame> bullets = new List<AmmoInGame>();
        protected DispatcherTimer dispense;
        protected Player player;
        double powerUpSpawnRate;
        protected int HitPoints;
        protected int frames;
        protected string uri;

        /*========================  SinglEnemy  ===========================*/
        public Enemy(Map m, Player p) : base(m)
        {
            Width = 80;
            Height = 48;
            HitPoints = 4;
            uri = Asset.enemy[0];
            Sprite = new Rec(Width, Height, uri);
            Spawner();

            this.player = p;
        }
        /// <summary>
        /// Enemies die
        /// </summary>
        /*======================== OnUpdate ================================*/
        public override void OnUpdate()
        {
            foreach (GameObject obj in Map.Objects)
            {   
                IfEnemyDead();

                if (!obj.ObDied && obj is Playerammo)
                {
                    if (IsHit(this, obj))
                    {
                        player.myScore.ShotEnemy(ScoreKeep.Norm);
                        obj.ObDied = true;
                        HitPoints -= 1;
                        EnemyHit(this);
                    }
                }
               
                if (frames == 20)
                {
                    frames = 0;
                    Sprite = new Rec(Width, Height, uri);
                    Width = 80;
                    Height = 48;
                }
                frames++;
            }
        }

        protected void Spawner()
        {
            System.Random spawn = new System.Random();
            int spawnNum = spawn.Next(0, 100);

            if (spawnNum <= 25)
            {
                //Top
                X = 360;
                Y = 20;
            }
            if (spawnNum <= 50 && spawnNum > 25)
            {
                //Left
                Y = 250;
            }
            if (spawnNum <= 75 && spawnNum > 50)
            {
                //Right
                Y = 250;
                X = 750;
            }
            if (spawnNum <= 100 && spawnNum > 75)
            {
                //Bot
                Y = 500;
                X = 385;
            }
        }

        private void EnemyHit(GameObject enemy)
        {
            if (frames < 30)
            {
                Sprite = null;
                enemy.Width = 0;
                enemy.Height = 0;
            }

        }

        private void SpawnPowerUp(Map m, Player p)
        {
            if(powerUpSpawnRate >= 10 && powerUpSpawnRate <= 20)
            {
                SpeedPowerUp speed;
                if (this is EnemyMoveToRandom)
                {
                    speed = new SpeedPowerUp(m, p, X + 30, Y + 30);
                }
                else
                {
                    speed = new SpeedPowerUp(m, p, X, Y);
                }
                m.AddObject(speed);
            }
            if(powerUpSpawnRate <= 5 && powerUpSpawnRate >= 0)
            {
                InvincibilityPowerUp inv;
                if (this is EnemyMoveToRandom)
                {
                   inv = new InvincibilityPowerUp(m, p, X + 30, Y + 30);
                }
                else
                {
                    inv = new InvincibilityPowerUp(m, p, X, Y);
                }
                
                m.AddObject(inv);
            }
        }

        private void IfEnemyDead()
        {
            if (HitPoints == 0)
            {
                rand = new Engine.Random();
                powerUpSpawnRate = rand.NextDouble(0, 200);
                SpawnPowerUp(Map, player);
                this.ObDied = true;
                this.Width = 0;
                this.Height = 0;
                if (this is EnemyMoveToRandom)
                {
                    dispense.Stop();
                }
            }
        }
    }
}
    

   
