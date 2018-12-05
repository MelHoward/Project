using System;
using System.Windows;
using System.Windows.Media;

namespace twoDTDS.Engine
{

    /*---------------------------------------------------------------------------------------
                                PLAYAREA : FRAMEWORKELEMENT
    ---------------------------------------------------------------------------------------*/
    public class PlayArea : FrameworkElement
    {
        private VisualCollection canvas;
        private Map map;
        public IPlayAreaControl PlaneControl { get; set; }

        /*============================= Player >> CTOR ===========================*/
        public virtual Map Map
        {
            get { return map; }
            set { map = value; }
        }

        //amount to translate the coordinate 
        public double ViewOffsetX { get; set; }
        public double ViewOffsetY { get; set; }
        //translate -half width and -half height to center the Sprite
        public double ViewScaleOriginX { get; set; } = 0.5;
        public double ViewScaleOriginY { get; set; } = 0.5;
        //set axis scale factor
        public double ViewScaleX { get; set; } = 1;
        public double ViewScaleY { get; set; } = 1;

        /*========================= GetVisualChild ==========================*/
        protected override Visual GetVisualChild(int index)
        {
            return canvas[index];
        }

        /*========================= VisualChildernCount =====================*/
        protected override int VisualChildrenCount
        {
            get{
                return canvas.Count;
            }
        }

        /*========================== PlayArea >> CTOR =======================*/
        public PlayArea()
        {
            canvas = new VisualCollection(this);
            Loaded += PlayArea_Loaded;
        }

        /*========================= GetPlayAreaParent =======================*/
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

        /*============================= PlayArea_Loaded======================*/
        private void PlayArea_Loaded(object sender, RoutedEventArgs e)
        {
            PlaneControl = GetPlayAreaParent(Parent);

            CompositionTarget.Rendering += RenderCompT;
        }

        /*============================= RenderCompT =========================*/
        private void RenderCompT(object sender, EventArgs e){ Render(); }

        /*============================= Render ==============================*/
        private void Render()
        {
            if (map != null)
            {
                map.OnUpdate();
                DrawingVisual view = new DrawingVisual();
                TransformGroup group = new TransformGroup();
                using (DrawingContext dc = view.RenderOpen()) { map.OnRender(dc); }
                group.Children.Add(new TranslateTransform(ViewOffsetX, ViewOffsetY));
                group.Children.Add(new ScaleTransform() { CenterX = ViewScaleOriginX,
                                   CenterY = ViewScaleOriginY, ScaleX = ViewScaleX,
                                   ScaleY = ViewScaleY });
                PlaneControl.SettransformOrigin(
                             new Point(ViewScaleOriginX, ViewScaleOriginY));
                PlaneControl.Settransform(group);
 
                PushVisual(view);
            }
        }

        /*============================= PushVisual ==========================*/
        public void PushVisual(Visual v)
        {
            canvas.Clear();
            canvas.Add(v);
        }
    }
}
