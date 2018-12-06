using System;
using System.Windows;
using System.Numerics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using twoDTDS.Engine;
using static System.Windows.Media.Imaging.WriteableBitmapExtensions;
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
        public static ScoreKeep myScore { get; set; }
        Engine.Random r = new Engine.Random();
        DispatcherTimer bulletCreate, camShake;
        public int cameraShakeCount = 0, rollFrames = 0, invincibilityFrames;
        public float speed = 3, dyingSize = 40;
        public string uri;
        public bool invincible = false;
        public bool roll = false;
        GameObject iteration;
        Obstacle Obst;

        enum Direction
        {
            left,
            right,
            idle,
            back,
            forward
        }

        Direction Currentdirection;
        Direction PreviousDirection;


        /*============================= Player >> CTOR ===========================*/
        public Player(Map map) : base(map)
        {
            imgFrames = Asset.heroIdle;
            iflIndex = 0;
            X = (float) Math.Round(map.Width / 2);
            Y = (float) (map.Height - 50);
            Position = new System.Numerics.Vector2((float) X, Y);
            Width = 40;
            Height = 45;
        
            myScore = new ScoreKeep();
            myScore.IsDead += Score_died;
        }

        public override void Render(WriteableBitmap img)
        {
            base.Render(img);
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
                    Keyboard.IsKeyDown(Key.Down) || Keyboard.IsKeyDown(Key.Up))
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

                X = (float) Math.Min(Map.Width - Width, Math.Max(0, X));
                Y = (float) Math.Min(Map.Height - Height, Math.Max(0, Y));

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
        /*================================== RollReset =============================*/
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
        /*================================== ShowIframes =============================*/
        private void ShowIframes()
        {
            if (invincibilityFrames > 0)
            {
                invincible = true;
                invincibilityFrames--;
            }

            if (invincibilityFrames == 0)  {  invincible = false; } 

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

        /*================================== Mover =============================*/
        private void Move()
        {
            PreviousDirection = Currentdirection;
            for (int i = 0; i < Map.Objects.Count; i++)
            {
                iteration = Map.Objects[i];
                if (!iteration.ObDied && iteration is Obstacle)
                {
                    Obst = (Obstacle) iteration;
                    if (IsHit(this, Obst))
                    {
                        Obst.CollisionSetBack(this);
                        Obst.checkTopBot(this);
                    }
                }
            }

            if (Keyboard.IsKeyDown(Key.A))
            {
                Currentdirection = Direction.left;
            }
            else if (Keyboard.IsKeyDown(Key.D))
            {
                Currentdirection = Direction.right;
            }
            else if (Keyboard.IsKeyDown(Key.W))
            {
                Currentdirection = Direction.forward;
            }
            else if (Keyboard.IsKeyDown(Key.S))
            {
                Currentdirection = Direction.back;
            }
            else {   Currentdirection = Direction.idle;  }
        }

        /*================================== SetRate =============================*/
        public override void SetRate()
        {
            Move();
            if (PreviousDirection != Currentdirection) iflIndex = 0;
            switch (Currentdirection)
            {
                case Direction.idle:
                    Rate = new Vector2(Rate.X, Rate.Y);
                    break;
                case Direction.forward:
                    Rate = new Vector2(Rate.X, speed);
                    imgFrames = Asset.heroBack;
                    break;
                case Direction.back:
                    Rate = new Vector2(Rate.X, -speed);
                    imgFrames = Asset.heroFront;
                    break;
                case Direction.left:
                    Rate = new Vector2(-speed, Rate.Y);
                    imgFrames = Asset.heroLeft;
                    break;
                case Direction.right:
                    Rate = new Vector2(speed, Rate.Y);
                    imgFrames = Asset.heroRight;
                    break;
            }
        }

        /*================================== Duration =============================*/
        public override void Duration(double durationMS)
        {
            base.Duration(durationMS);
        }
        /*================================== Shoot =============================*/
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
            }

            ;
            bulletCreate.Start();
        }
        /*================================== IsPlayerHit =============================*/
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
        /*================================== CheckPowerUp()=================*/
        public void CheckPowerUp()
        {
            InvincibilityPowerUp sp= new InvincibilityPowerUp(Map, this, X + 100, Y - 100);
            if (Keyboard.IsKeyDown(Key.Space))
            {
                Map.AddObject(sp);
            }
            if (Keyboard.IsKeyDown(Key.D3))
            {
                Rock rock = new Rock(Map, 300, 300);
                Map.AddObject(rock);
            }

        }

        /*================================== OnRender =============================*/
        //    public override void OnRender(DrawingContext dc)
        //    {
        //        if (!myScore.Died) { base.OnRender(dc); }
        //        else
        //        {
        //            Map.DrwTxt(dc, "YOU DIED", (Map.Width / 2), (Map.Height / 2),
        //                         dyingSize, HorizontalAlignment.Center,
        //                         System.Windows.VerticalAlignment.Center);
        //        }
        //    }
        //}
    }
}
