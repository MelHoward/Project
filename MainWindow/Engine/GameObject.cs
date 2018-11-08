using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace twoDTDS.Engine
{
    public abstract class GameObject : DependencyObject
    {
        public static DependencyProperty XProperty = 
                      DependencyProperty.Register("X", typeof(double), typeof(GameObject));
        public static DependencyProperty YProperty = 
                      DependencyProperty.Register("Y", typeof(double), typeof(GameObject));

        public double X {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public double Y {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public double Width { get; set; }

        public double Height { get; set; }

        public Sprite sprite { get; set; }

        public Map Map { get; set; }

        public bool ObDied { get; set; } = false;

        public GameObject(Map map){ Map = map; }

        public virtual void OnUpdate(){}

        public virtual void OnRender(DrawingContext dc)
        {
            if (sprite != null)
                sprite.Render(this, dc);
        }

        private Storyboard MoveToStoryboard;
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

        public static bool isHit(GameObject me, GameObject other)
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

        public bool isHit(GameObject other){ return isHit(this, other); }

        public void CheckOutOfBounds()
        {
            if ((X < -Width)  || (X > Map.Width + Width) || 
                (Y < -Height) || (Y > Map.Height + Height)) {
                ObDied = true;
            }
        }
    }
}