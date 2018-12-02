using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Threading;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
/*---------------------------------------------------------------------------------------
                              PLAYER : GAMEOBJECT
---------------------------------------------------------------------------------------*/
    public class Player : GameObject
    {
        public ScoreKeep myScore { get; set; }
        Engine.Random r = new Engine.Random();

        DispatcherTimer bulletCreate;
        DispatcherTimer camShake;
        int cameraShakeCount = 0;
        double speed = 3;
        double dyingSize = 12;
        string direction = "";

        /*============================= Player >> CTOR ===========================*/
        public Player(Map map) : base(map)
        {
            X = Math.Round(map.Width / 2);
            Y = map.Height - 50;
            Width = 14;
            Height = 14;
            Sprite = new Rec(Width, Height);

            myScore = new ScoreKeep();
            myScore.IsDead += Score_died;
        }

        /*================================== Score_died ==========================*/
        private void Score_died(object sender, ScoreKeep e) { 
            Console.WriteLine("You Died!");
            if (bulletCreate != null) { bulletCreate.Stop(); }
            DispatcherTimer t = new DispatcherTimer();
            int tcount = 0;
            t.Interval = TimeSpan.FromMilliseconds(15);
            t.Tick += delegate
            {
                tcount++;
                if (tcount > 60){
                    t.Stop(); return;
                }
                dyingSize = dyingSize + (24 - dyingSize) / 10;
            };
        t.Start();
        }

        /*================================== OnUpdate =============================*/

        public override void OnUpdate()
        {
            if (!myScore.Died)
            {
                if (Keyboard.IsKeyDown(Key.A))
                {
                    X -= speed;
                    direction = "left";
                }
                else if (Keyboard.IsKeyDown(Key.D))
                {
                    X += speed;
                    direction = "right";
                }
                if (Keyboard.IsKeyDown(Key.W))
                {
                    Y -= speed;
                    direction = "up";
                }
                else if (Keyboard.IsKeyDown(Key.S))
                {
                    Y += speed;
                    direction = "down";
                }

                if (Keyboard.IsKeyDown(Key.Space))
                {
                    if (bulletCreate == null)
                    {
                        bulletCreate = new DispatcherTimer();
                        bulletCreate.Interval = TimeSpan.FromMilliseconds(250);
                        bulletCreate.Tick += delegate
                        {
                            Playerammo a = new Playerammo(Map, X + Width / 2, Y);
                            if (direction == "up")
                            {
                                a.upVel = 15;
                                a.downVel = 0;
                                a.leftVel = 0;
                                a.rightVel = 0;
                            }
                            if (direction == "down")
                            {
                                a.upVel = 0;
                                a.downVel = 15;
                                a.leftVel = 0;
                                a.rightVel = 0;
                            }
                            if (direction == "left")
                            {
                                a.upVel = 0;
                                a.downVel = 0;
                                a.leftVel = 15;
                                a.rightVel = 0;
                            }
                            if (direction == "right")
                            {
                                a.upVel = 0;
                                a.downVel = 0;
                                a.leftVel = 0;
                                a.rightVel = 15;
                            }

                            Map.AddObject(a);
                        };
                    }
                    bulletCreate.Start();
                }
                else { if (bulletCreate != null) { bulletCreate.Stop(); }}

                X = Math.Min(Map.Width - Width, Math.Max(0, X));
                Y = Math.Min(Map.Height - Height, Math.Max(0, Y));

                foreach (GameObject obj in Map.Objects)
                {
                    if (!obj.ObDied && obj is TempEnemyammo)
                    {
                        if (IsHit(this, obj))
                        {
                            myScore.PlayerHit(((TempEnemyammo)obj).Damage);

                            if (camShake == null)
                            {
                                camShake = new DispatcherTimer();
                                camShake.Interval = TimeSpan.FromMilliseconds(25);
                                camShake.Tick += delegate
                                {
                                    cameraShakeCount++;
                                    if (cameraShakeCount > 7)
                                    {
                                        camShake.Stop();
                                        Map.Plane.ViewOffsetX = 0;
                                        Map.Plane.ViewOffsetY = 0;
                                        return;
                                    }
                                    Map.Plane.ViewOffsetX = r.NextDouble(-5, 5);
                                    Map.Plane.ViewOffsetY = r.NextDouble(-5, 5);
                                };
                            }
                            cameraShakeCount = 0;
                            camShake.Start();
                        }
                    }
                }
            }
        }

        /*================================== OnRender =============================*/
        public override void OnRender(DrawingContext dc)
        {
            if (!myScore.Died) { base.OnRender(dc); }
            else
            {
                Map.DrwTxt(dc, "YOU DIED", (Map.Width / 2), (Map.Height / 2),
                             dyingSize, HorizontalAlignment.Center,
                             System.Windows.VerticalAlignment.Center );
            }
        }
    }
}
