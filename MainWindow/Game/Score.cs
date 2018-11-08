using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public class Score
    {
        public static int Norm { get; set; } = 1;
        public static int Guid { get; set; } = 5;

        public int MaxHP { get; set; } = 250;
        public int HP { get; set; } = 250;
        public int Streak { get; set; } = 0;
        public int MaxStreak { get; set; } = 0;
        public int Sc { get; set; } = 0;

        public bool Died { get; set; } = false;

        public event EventHandler<Score> isDead;
        public event EventHandler<Score> Streaks;

        public void playerHit(int Damage)
        {
            Streak = 0;
            HP -= Damage;
            if (HP <= 0)
            {
                HP = 0;
                Died = true;
                isDead?.Invoke(this, this);
            }
        }

        public void shotEnemy(int Score)
        {
            this.Sc += Score + Streak;
            Streak++;
            MaxStreak = Math.Max(MaxStreak, Streak);
            Streaks ?.Invoke(this, this);
        }
    }
}
