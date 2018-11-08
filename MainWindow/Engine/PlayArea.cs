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
    public class PlayArea : FrameworkElement
    {
        private VisualCollection canvas;

        public IPlayAreaControl PlaneControl { get; set; }

        private Map map;
        public virtual Map Map
        {
            get
            {
                return map;
            }
            set
            {
                map = value;
            }
        }

        //amount to translate the coordinate 
        public double ViewOffsetX { get; set; }
        public double ViewOffsetY { get; set; }

        //translate -half width and -half height to center the sprite
        public double ViewScaleOriginX { get; set; } = 0.5;
        public double ViewScaleOriginY { get; set; } = 0.5;

        //set axis scale factor
        public double ViewScaleX { get; set; } = 1;
        public double ViewScaleY { get; set; } = 1;

        protected override Visual GetVisualChild(int index){ return canvas[index]; }

        protected override int VisualChildrenCount { get{ return canvas.Count; } }

        public PlayArea()
        {
            canvas = new VisualCollection(this);
            Loaded += PlayArea_Loaded;
        }

        private IPlayAreaControl GetPlayAreaParent(DependencyObject obj)
        {
            if ((obj != null) && (obj is FrameworkElement))
            {
                if (obj is IPlayAreaControl)
                    return (IPlayAreaControl)obj;
                return GetPlayAreaParent(((FrameworkElement)obj).Parent);
            }
            return null;
        }

        private void PlayArea_Loaded(object sender, RoutedEventArgs e)
        {
            PlaneControl = GetPlayAreaParent(Parent);
            CompositionTarget.Rendering += RenderCompT;
        }

        private void RenderCompT(object sender, EventArgs e){ Render(); }

        private void Render()
        {
            if (map != null)
            {
                map.OnUpdate();
                DrawingVisual view = new DrawingVisual();

                using (DrawingContext dc = view.RenderOpen()){ map.OnRender(dc); }

                TransformGroup group = new TransformGroup();
                group.Children.Add(new TranslateTransform(ViewOffsetX, ViewOffsetY));
                group.Children.Add(new ScaleTransform() { CenterX = ViewScaleOriginX,
                                   CenterY = ViewScaleOriginY, ScaleX = ViewScaleX,
                                   ScaleY = ViewScaleY });
                PlaneControl.SetTransfromOrigin(new Point
                                               (ViewScaleOriginX, ViewScaleOriginY));
                PlaneControl.SetTransfrom(group);
 
                PushVisual(view);
            }
        }
        //clear screen push new image
        public void PushVisual(Visual v)
        {
            canvas.Clear();
            canvas.Add(v);
        }
    }
}
