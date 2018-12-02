using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
                      DependencyProperty.Register("X", typeof(double), typeof(GameObject));
        public static DependencyProperty YProperty = 
                      DependencyProperty.Register("Y", typeof(double), typeof(GameObject));

        /*============================= X >> Acc. ===========================*/
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        /*============================= Y >> Acc. ===========================*/
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        /*============================= Width ===============================*/
        public double Width { get; set; }
        /*============================== Height =============================*/
        public double Height { get; set; }

        /*=========================== Sprite >> CTOR ========================*/
        public Sprite Sprite { get; set; }

        /*=========================== Map >> CTOR ===========================*/
        public Map Map { get; set; }

        /*============================= ObDied ==============================*/
        public bool ObDied { get; set; } = false;

        /*=========================== GameObject ============================*/
        public GameObject(Map map){ Map = map; }

        /*=========================== OnUpdate  =============================*/
        public virtual void OnUpdate() { }

        /*=========================== <<OnRender>> ==========================*/
        public virtual void OnRender(DrawingContext dc)
        {
            if (Sprite != null)
                Sprite.Render(this, dc);
        }

        /*========================== MoveToStoryboard =======================*/
        private Storyboard MoveToStoryboard;

        /*============================= MoveTo ==============================*/
        public Storyboard MoveTo(double x, double y, double durationMs)
        {
            if (MoveToStoryboard != null)
            {
                MoveToStoryboard.Stop();
                MoveToStoryboard.Remove();
            }

            Storyboard sb = new Storyboard();
            Duration duration = new Duration(TimeSpan.FromMilliseconds(durationMs));

            DoubleAnimation xAnimation = new DoubleAnimation(x, duration);
            DoubleAnimation yAnimation = new DoubleAnimation(y, duration);

            Storyboard.SetTargetProperty(xAnimation, new PropertyPath(XProperty));
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath(YProperty));

            Storyboard.SetTarget(xAnimation, this);
            Storyboard.SetTarget(yAnimation, this);

            sb.Children.Add(xAnimation);
            sb.Children.Add(yAnimation);

            sb.Begin();

            MoveToStoryboard = sb;
            return sb; 
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
        public bool IsHit(GameObject other)
        {
            return IsHit(this, other);
        }

        /*========================= CheckOutOfBounds ========================*/
        public void CheckOutOfBounds()
        {
            if ((X < -Width)  || (X > Map.Width + Width) || (Y < -Height) || (Y > Map.Height + Height)) {
                ObDied = true;
            }
        }
    }
}