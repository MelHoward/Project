using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace twoDTDS.Engine
{
    public interface PlayAreaControl
    {
        void SetTransfrom(Transform transfrom);
        void SetTransfromOrigin(Point pt);
    }

    public class PlayArea : FrameworkElement
    {
        private VisualCollection canvas;

        public PlayAreaControl PlaneControl { get; set; }

        private Map _map;
        public virtual Map map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
            }
        }

        public double ViewOffsetX { get; set; }
        public double ViewOffsetY { get; set; }

        public double ViewScaleOriginX { get; set; } = 0.5;
        public double ViewScaleOriginY { get; set; } = 0.5;

        public double ViewScaleX { get; set; } = 1;
        public double ViewScaleY { get; set; } = 1;

        protected override Visual GetVisualChild(int index)
        {
            return canvas[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return canvas.Count;
            }
        }

        public PlayArea()
        {
            canvas = new VisualCollection(this);

            Loaded += PlayArea_Loaded;
        }

        private PlayAreaControl GetPlayAreaParent(DependencyObject obj)
        {
            if (obj != null && obj is FrameworkElement)
            {
                if (obj is PlayAreaControl)
                    return (PlayAreaControl)obj;

                return GetPlayAreaParent(((FrameworkElement)obj).Parent);
            }
            return null;
        }

        private void PlayArea_Loaded(object sender, RoutedEventArgs e)
        {
            PlaneControl = GetPlayAreaParent(Parent);

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            Render();
        }

        private void Render()
        {
            if (_map != null)
            {
                _map.OnUpdate();

                DrawingVisual view = new DrawingVisual();

                using (DrawingContext dc = view.RenderOpen())
                {
                    _map.OnRender(dc);
                }

                TransformGroup group = new TransformGroup();
                group.Children.Add(new TranslateTransform(ViewOffsetX, ViewOffsetY));
                group.Children.Add(new ScaleTransform() { CenterX = ViewScaleOriginX, CenterY = ViewScaleOriginY, ScaleX = ViewScaleX, ScaleY = ViewScaleY });
                PlaneControl.SetTransfromOrigin(new Point(ViewScaleOriginX, ViewScaleOriginY));
                PlaneControl.SetTransfrom(group);

                PushVisual(view);
            }
        }

        public void PushVisual(Visual visual)
        {
            canvas.Clear();
            canvas.Add(visual);
        }
    }
}
