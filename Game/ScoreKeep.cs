using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
/*---------------------------------------------------------------------------------------
                                     SCOREKEEP 
---------------------------------------------------------------------------------------*/
    public class ScoreKeep
    {
        public static int Norm { get; set; } = 1;
        public static int Guid { get; set; } = 5;

        public int MaxHP { get; set; } = 250;
        public int HP { get; set; } = 250;
        public int Streak { get; set; } = 0;
        public int MaxStreak { get; set; } = 0;
        public int Sc { get; set; } = 0;

        public bool Died { get; set; } = false;

        public event EventHandler<ScoreKeep> IsDead;
        public event EventHandler<ScoreKeep> Streaks;

        public void PlayerHit(int Damage)
        {
            Streak = 0;
            HP -= Damage;
            if (HP <= 0)
            {
                HP = 0;
                Died = true;
                IsDead?.Invoke(this, this);
            }
        }

        public void ShotEnemy(int Score)
        {
            this.Sc += Score + Streak;
            Streak++;
            MaxStreak = Math.Max(MaxStreak, Streak);
            Streaks ?.Invoke(this, this);
        }
    }
}
