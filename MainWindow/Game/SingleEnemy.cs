using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public class SingleEnemy : GameObject
    {
        Engine.Random rand = new Engine.Random();
        List<AmmoInGame> bullets = new List<AmmoInGame>();
        DispatcherTimer dispense;
        Player player;

        /*========================  SingleEnemy  ===========================*/
        public SingleEnemy(Map m, Player p) : base(m)
        {
            MoveToRandom();
            Width = 40;
            Height = 40;

            Sprite = new Rec("D:\\Documents\\Programs\\Project352\\Project\\MainWindow\\img\\up.png", Width, Height);
            this.player = p;

            if (dispense == null)
            {
                dispense = new DispatcherTimer();
                dispense.Interval = TimeSpan.FromMilliseconds(75);
                dispense.Tick += delegate
                {
                    Map.AddObject(new Ammo(Map, X + Width / 2, Y));
                };
            }
         dispense.Start();
        }

        /*======================== OnUpdate ================================*/
        public override void OnUpdate()
        {
            foreach (GameObject obj in Map.Objects)
            {
                if (!obj.ObDied && obj is Ammo)
                {
                    if(IsHit(this, obj))
                    {
                        player.myScore.ShotEnemy(ScoreKeep.Norm);
                    }
                }
            }
        }

        /*=============================== MoveToRandom ======================*/
        private void MoveToRandom()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(700);
            timer.Tick += delegate
            {
                double x = rand.NextDouble(10, Map.Width - 10 - 80);
                double duration = Math.Abs(x - X) * 8;
                timer.Interval = TimeSpan.FromMilliseconds(duration);
                MoveTo(x, rand.NextDouble(10, 30), duration);
            };
           timer.Start();
        }
    }
}
