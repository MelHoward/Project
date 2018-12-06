﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using twoDTDS.Engine;
/** Player 
       + Player(Map map)
       + Score_died(object sender, ScoreKeep e):void
       + OnUpdate():void
       + Roll():void
       + RollReset():void
       + Move():void
       + Shoot():void
       + IsPlayerHit():void
       + CheckPowerUp():void
       + OnRender(DrawingContext dc):void   
*/
namespace twoDTDS.Game
{
    /*---------------------------------------------------------------------------------------
                                  PLAYER : GAMEOBJECT
    ---------------------------------------------------------------------------------------*/
    public class Player : GameObject
    {
        public ScoreKeep myScore { get; set; }

        Engine.Random r = new Engine.Random();

        DispatcherTimer bulletCreate,
                        camShake;
        public int cameraShakeCount = 0,
                   rollFrames = 0,
                   invincibilityFrames;

        public double speed = 3, 
                      dyingSize = 40;
        public string uri;
        public bool invincible = false;
        public bool roll = false;
     

        /*============================= Player >> CTOR ===========================*/
        public Player(Map map) : base(map)
        {
            X = Math.Round(map.Width / 2);
            Y = map.Height - 50;
            Width = 40;
            Height = 45;
            uri = Asset.hero[0];
            Sprite = new Rec(Width, Height, uri);

            myScore = new ScoreKeep();
            myScore.IsDead += Score_died;
        }

        /*================================== Score_died ==========================*/
        private void Score_died(object sender, ScoreKeep e)
        {
            Console.WriteLine("You Died!");
            if (bulletCreate != null)
            {
                bulletCreate.Stop();
            }

            DispatcherTimer t = new DispatcherTimer();
            int tcount = 0;
            t.Interval = TimeSpan.FromMilliseconds(15);
            t.Tick += delegate
            {
                tcount++;
                if (tcount > 60)
                {
                    t.Stop();
                    return;
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

                if (Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.Left) ||
                    Keyboard.IsKeyDown(Key.Down)  || Keyboard.IsKeyDown(Key.Up))
                {
                    Shoot();
                }
                else
                {
                    if (bulletCreate != null)
                    {
                        bulletCreate.Stop();
                    }
                }

                X = Math.Min(Map.Width - Width, Math.Max(0, X));
                Y = Math.Min(Map.Height - Height, Math.Max(0, Y));

                if (Keyboard.IsKeyDown(Key.E))
                {
                    Roll();
                }
                RollReset();

                IsPlayerHit();
                ShowIframes();


                CheckPowerUp();
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
                    roll = true;
                }
                
                if (roll == true)
                {
                    invincible = true;
                    Sprite = new Rec(30, 35, uri);
                }
                else
                {
                    invincible = false;
                }
                
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
                Sprite = new Rec(Width, Height, uri);
                rollFrames = -25;
            }

            rollFrames++;
        }

        private void ShowIframes()
        {
            if (invincibilityFrames > 0)
            {
                invincible = true;
                invincibilityFrames--;
            }
            if (invincibilityFrames == 0)
            {
                invincible = false;
            }
            if (invincible == true)
            {
                if (invincibilityFrames % 2 == 0)
                {
                    Sprite = null;
                }
                else
                {
                    Sprite = new Rec(40, 45, uri);
                }
            }
        }
        /// <summary>
        /// Lets you move using WASD
        /// </summary>
        private void Move()
        {
            for (int i = 0; i < Map.Objects.Count; i++)
            {
                iteration = Map.Objects[i];
                if (!iteration.ObDied && iteration is Obstacle)
                {
                    Obst = (Obstacle)iteration;
                    if (IsHit(this, Obst))
                    {
                        Obst.CollisionSetBack(this);
                        Obst.checkTopBot(this);
                    }
                }
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                uri = Asset.hero[3];
                Sprite = new Rec(Width, Height, uri);
                X -= speed;
            }
            else if (Keyboard.IsKeyDown(Key.D))
            {
                uri = Asset.hero[4];
                Sprite = new Rec(Width, Height, uri);
                X += speed;
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                uri = Asset.hero[0];
                Sprite = new Rec(Width, Height, uri);
                Y -= speed;
            }
            else if (Keyboard.IsKeyDown(Key.S))
            {
                uri = Asset.hero[2];
                Sprite = new Rec(Width, Height, uri);
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
                    if (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Up))
                    {
                        a.direction = "upLeft";
                    }
                    if (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Down))
                    {
                        a.direction = "downLeft";
                    }
                    if (Keyboard.IsKeyDown(Key.Right) && Keyboard.IsKeyDown(Key.Up))
                    {
                        a.direction = "upRight";
                    }
                    if (Keyboard.IsKeyDown(Key.Right) && Keyboard.IsKeyDown(Key.Down))
                    {
                        a.direction = "downRight";
                    }

                    Map.AddObject(a);
                };
            };
            bulletCreate.Start();
        }
        /// <summary>
        /// Detects when you get hit and shakes "camera" when you do
        /// </summary>
        private void IsPlayerHit()
        {
            foreach (GameObject obj in Map.Objects)
            {
                if (!obj.ObDied && obj is TempEnemyammo || obj is Enemy)
                {
                    if (IsHit(this, obj))
                    {
                        if (invincible == false)
                        {
                            invincibilityFrames = 50;

                            myScore.PlayerHit(10);

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

        public void CheckPowerUp()
        {
            InvincibilityPowerUp speed = new InvincibilityPowerUp(Map, this, X + 100, Y - 100);
            if (Keyboard.IsKeyDown(Key.Space))
            {


                Map.AddObject(speed);
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
                             System.Windows.VerticalAlignment.Center);
            }
        }
    }
}
