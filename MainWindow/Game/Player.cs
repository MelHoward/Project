using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using twoDTDS.Engine;

namespace twoDTDS.Game
{
    public class Random
    {
        System.Random rand = new System.Random((int)DateTime.UtcNow.TimeOfDay.TotalMilliseconds);

        public double NextDouble()
        {
            return rand.NextDouble();
        }
        public double NextDouble(double min, double max)
        {
            return (double)rand.Next((int)(min * 10000), (int)(max * 10000)) / 10000;
        }
        public int Next(int min, int max)
        {
            return rand.Next(min, max);
        }
    }

    public class ObjPlayer : GameObject
    {
        Random rand = new Random();
        DispatcherTimer bulletCreate;
        DispatcherTimer camaraShake;
        int cameraShakeCount = 0;
        double speed = 3;
        double dyingSize = 12;

        public ObjPlayer(Map Map) : base(Map)
        {
            X = Math.Round(Map.Width / 2);
            Y = Map.Height - 50;

            Width = 14;
            Height = 14;
            Score Score = new Score();
            Sprite = new Rect(new SolidColorBrush(Color.FromRgb(255, 50, 50)), Width, Height);
 
        }

        private void ScoreManager_Dieded(object sender, Score e)
        {
            Console.WriteLine("YOU DIED!");

            if (bulletCreate != null)
                bulletCreate.Stop();

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

    

        public override void OnUpdate()
        {
            if (!Score.IsDied)
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
                if (Keyboard.IsKeyDown(Key.Space))
                {
                    if (bulletCreate == null)
                    {
                        bulletCreate = new DispatcherTimer();
                        bulletCreate.Interval = TimeSpan.FromMilliseconds(75);
                        bulletCreate.Tick += delegate
                        {
                            Map.AddObject(new ObjOwnBullet(Map, X + Width / 2, Y));
                        };
                    }
                    bulletCreate.Start();
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

                foreach (GameObject obj in Map.Objects)
                {
                    if (!obj.IsDied && obj is EnemyBullet)
                    {
                        if (IsHitted(this, obj))
                        {
                            Score.HeroHitted(((EnemyBullet)obj).Damage);

                            if (camaraShake == null)
                            {
                                camaraShake = new DispatcherTimer();
                                camaraShake.Interval = TimeSpan.FromMilliseconds(25);
                                camaraShake.Tick += delegate
                                {
                                    cameraShakeCount++;
                                    if (cameraShakeCount > 7)
                                    {
                                        camaraShake.Stop();
                                        Map.Plane.ViewOffsetX = 0;
                                        Map.Plane.ViewOffsetY = 0;
                                        return;
                                    }
                                    Map.Plane.ViewOffsetX = rand.NextDouble(-5, 5);
                                    Map.Plane.ViewOffsetY = rand.NextDouble(-5, 5);
                                };
                            }

                            cameraShakeCount = 0;
                            camaraShake.Start();
                        }
                    }
                }
            }
        }

        public override void OnRender(DrawingContext dc)
        {
            if (!Score.IsDied)
            {
                base.OnRender(dc);
            }
            else
            {
                Map.DrawText(dc, "YOU DIED", Map.Width / 2, Map.Height / 2, dyingSize, System.Windows.HorizontalAlignment.Center, System.Windows.VerticalAlignment.Center);
            }
        }
    }
}
