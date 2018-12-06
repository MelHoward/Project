using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using static System.Windows.Media.Imaging.WriteableBitmapExtensions;

namespace twoDTDS.Engine
{

    /*---------------------------------------------------------------------------------------
                                    DEFAULT -> STATIC
    ---------------------------------------------------------------------------------------*/
    public static class Default
    {
        public static Typeface Typeface = new Typeface("ComicSans");
        public static Random Random = new Random();
    }
/*---------------------------------------------------------------------------------------
                         << GAMEOBJECT >> : DEPENDENCYPROPERTY 
---------------------------------------------------------------------------------------*/
    public abstract class GameObject : DependencyObject
    {
        public static DependencyProperty XProperty = 
                      DependencyProperty.Register("X", typeof(float), typeof(GameObject));
        public static DependencyProperty YProperty = 
                      DependencyProperty.Register("Y", typeof(float), typeof(GameObject));
        public int frames = 0,
                          frameRule = 10,
                          iflIndex;
        public Size size = new Size();                  
        public string[] imgFrames = null;
        /*============================= X >> Acc. ===========================*/
        public float X
        {
            get { return (float) GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        /*============================= Y >> Acc. ===========================*/
        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        /*============================= Width ===============================*/
        public float Width { get; set; }
        /*============================== Height =============================*/
        public float Height { get; set; }
        /*============================== Position =============================*/
        public Vector2 Position { get; set; }
        /*============================== Rate =============================*/
         public Vector2 Rate  { get; set;  }
        /*========================== Sprite >> CTOR ========================*/
        public Sprite Sprite { get; set; }
        /*=========================== Map >> CTOR ===========================*/
        public Map Map { get; set; }
        /*============================= ObDied ==============================*/
        public bool ObDied { get; set; } = false;
        /*=========================== GameObject ============================*/
        public GameObject(Map map){ Map = map; }
        /*============================== SetRate =============================*/
        public virtual void SetRate () { }
        /*=========================== Duration ===========================*/
        public virtual void Duration(double durationMS)
        {
            SetRate();
            Position += Rate * (float) (durationMS / 1000f);
        }


        /*=========================== OnRender ===========================*/
        public virtual void OnRender (DrawingContext dc)
        {
            if( Sprite != null )
                Sprite.Render(this, dc);
        }
        /*=========================== Render ===========================*/
        public virtual void Render(WriteableBitmap img)
        {
            BitmapImage src = new BitmapImage(new Uri(imgFrames[iflIndex]));
            WriteableBitmap bitmap = new WriteableBitmap(src);
            size = new Size(src.PixelWidth, src.PixelHeight);

            img.Blit(new Point(Position.X, Position.Y), bitmap,
                           new Rect(size), Colors.White, BlendMode.Alpha);

            if ( iflIndex >= imgFrames.Length - 1 ) {  iflIndex = 0;  }
            else if ((frames += 1) > frameRule) { iflIndex += 1; frames = 0;  }
        }

        /*============================= IsHit ===============================*/
        public static bool IsHit(GameObject me, GameObject other)
        {
            // returns true if other is within player bounds
            double leftX = me.X - other.Width,
                   rightX = me.X + me.Width,
                   bottomY = me.Y - other.Height,
                   topY = me.Y + me.Height;

            if ((other.X >= leftX) && (other.X <= rightX) && 
                (other.Y >= bottomY) && (other.Y <= topY)){
                return true;
            }
            return false;
        }

        /*============================= IsHit ===============================*/
        public virtual bool IsHit(GameObject other)
        {
            return IsHit(this, other);
        }

        /*========================= CheckOutOfBounds ========================*/
        public void CheckOutOfBounds()
        {
            if ((X < -Width)  || (X > Map.Width + Width) || 
                (Y < -Height) || (Y > Map.Height + Height)) {
                ObDied = true;
            }
        }

        public virtual void OnUpdate() { }

        private Storyboard MoveToStoryboard;

        /*============================= MoveTo ==============================*/
        public Storyboard MoveTo (double x, double y, double durationMs)
        {
            if( MoveToStoryboard != null )
            {
                MoveToStoryboard.Stop( );
                MoveToStoryboard.Remove( );
            }

            Storyboard sb = new Storyboard( );
            Duration duration = new Duration(TimeSpan.FromMilliseconds(durationMs));

            DoubleAnimation xAnimation = new DoubleAnimation(x, duration);
            DoubleAnimation yAnimation = new DoubleAnimation(y, duration);

            Storyboard.SetTargetProperty(xAnimation, new PropertyPath(XProperty));
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath(YProperty));

            Storyboard.SetTarget(xAnimation, this);
            Storyboard.SetTarget(yAnimation, this);

            sb.Children.Add(xAnimation);
            sb.Children.Add(yAnimation);

            sb.Begin( );

            MoveToStoryboard = sb;
            return sb;
        }


    }
}