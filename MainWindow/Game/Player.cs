﻿using System;
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
        double dyingSize = 40;
        int rollFrames = 0;
        int speedFrames;
        string uri;
        bool invincible = false;

        /*============================= Player >> CTOR ===========================*/
        public Player(Map map) : base(map)
        {
            X = Math.Round(map.Width / 2);
            Y = map.Height - 50;
            Width = 40;
            Height = 45;
            uri = "C:\\Users\\Corey\\Source\\Repos\\Project\\MainWindow\\Assets\\down.png";
            Sprite = new Rec(Width, Height, uri);

            myScore = new ScoreKeep();
            myScore.IsDead += Score_died;
        }

        /*================================== Score_died ==========================*/
        private void Score_died(object sender, ScoreKeep e)
        {
            Console.WriteLine("You Died!");
            if (bulletCreate != null) { bulletCreate.Stop(); }
            DispatcherTimer t = new DispatcherTimer();
            int tcount = 0;
            t.Interval = TimeSpan.FromMilliseconds(15);
            t.Tick += delegate
            {
                tcount++;
                if (tcount > 60)
                {
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
                Move();

                if (Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.Down) || Keyboard.IsKeyDown(Key.Up))
                {
                    Shoot();
                }
                else { if (bulletCreate != null) { bulletCreate.Stop(); } }

                X = Math.Min(Map.Width - Width, Math.Max(0, X));
                Y = Math.Min(Map.Height - Height, Math.Max(0, Y));

                IsHit();

                if (Keyboard.IsKeyDown(Key.E))
                {
                    Roll();

                }
                RollReset();

                SpeedUp(this);

                rollFrames++;
            }
        }
        /// <summary>
        /// Makes it to where you are invincible
        /// </summary>
        private void Roll()
        {
            if (rollFrames < 50 && rollFrames > 0)
            {
                if (rollFrames >= 0 && invincible != true)
                {
                    rollFrames = 0;
                }
                invincible = true;
                Sprite = new Rec(30, 35, uri);
            }
        }
        /// <summary>
        /// Makes you not invincible after a short time
        /// </summary>
        private void RollReset()
        {
            if (rollFrames == 50)
            {
                invincible = false;
                Sprite = new Rec(40, 45, uri);
                rollFrames = -25;
            }
        }
        /// <summary>
        /// Lets you move using WASD
        /// </summary>
        private void Move()
        {
            if (Keyboard.IsKeyDown(Key.A))
            {
                X -= speed;
            }
            else if (Keyboard.IsKeyDown(Key.D))
            {
                X += speed;
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                Y -= speed;
            }
            else if (Keyboard.IsKeyDown(Key.S))
            {
                Y += speed;
            }
        }
        /// <summary>
        /// Lets you shoot using the arrow keys
        /// </summary>
        private void Shoot()
        {
            if (bulletCreate == null)
            {
                bulletCreate = new DispatcherTimer();
                bulletCreate.Interval = TimeSpan.FromMilliseconds(250);
                bulletCreate.Tick += delegate
                {
                    Playerammo a = new Playerammo(Map, X + Width / 2, Y);

                    if (Keyboard.IsKeyDown(Key.Up))
                    {
                        a.direction = "up";
                    }
                    if (Keyboard.IsKeyDown(Key.Down))
                    {
                        a.direction = "down";
                    }
                    if (Keyboard.IsKeyDown(Key.Left))
                    {
                        a.direction = "left";
                    }
                    if (Keyboard.IsKeyDown(Key.Right))
                    {
                        a.direction = "right";
                    }

                    Map.AddObject(a);
                };
            };
            bulletCreate.Start();
        }
        /// <summary>
        /// Detects when you get hit and shakes "camera" when you do
        /// </summary>
        private void IsHit()
        {
            foreach (GameObject obj in Map.Objects)
            {
                if (!obj.ObDied && obj is TempEnemyammo)
                {
                    if (IsHit(this, obj))
                    {
                        if (invincible == false)
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

        private void SpeedUp(GameObject player)
        {
            SpeedPowerUp test = new SpeedPowerUp(Map, X + 100, Y - 100);
            if (Keyboard.IsKeyDown(Key.Space))
            {

                test.Width = 40;
                test.Height = 40;


                Map.AddObject(test);
            }

            foreach (GameObject obj in Map.Objects)
            {
                if (!obj.ObDied && obj is SpeedPowerUp)
                {
                    if (IsHit(this, obj))
                    {
                        speedFrames = 300;

                        obj.ObDied = true;
                        obj.Sprite = null;
                        obj.Width = 0;
                        obj.Height = 0;
                    }
                }
            }

            if (speedFrames > 0)
            {
                speed = 5;
            }
            else
            {
                speed = 3;
            }
            speedFrames--;
        }

        /*================================== OnRender =============================*/
        public override void OnRender(DrawingContext dc)
        {
            if (!myScore.Died) { base.OnRender(dc); }
            else
            {
                Map.DrwTxt(dc, "YOU DIED", (Map.Width / 2), (Map.Height / 2),
                             dyingSize, HorizontalAlignment.Center,
                             System.Windows.VerticalAlignment.Center);
            }
        }
    }

    public class SpeedPowerUp : GameObject
    {
        public string direction;

        public SpeedPowerUp(Map m, double X, double Y) : base(m)
        {
            this.X = X;
            this.Y = Y;
            Width = 20;
            Height = 20;
            string uri = "http://pixelartmaker.com/art/f59eaa826d4e49f.png";
            Sprite = new Rec(Width, Height, uri);
        }
    }
}
