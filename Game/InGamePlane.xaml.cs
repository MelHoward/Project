using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using twoDTDS.Engine;


namespace twoDTDS.Game
{
    public partial class InGamePlane : UserControl, IPlayAreaControl
    {
        
        public InGamePlane()
        {
           InitializeComponent();
            Loaded += InGamePlane_Loaded;
        }
        //CALLS LEVEL
        private void InGamePlane_Loaded(object sender, RoutedEventArgs e)
        {
            plane.Map = new Level(plane);

            
        }

        private void Bt_Restart_Click(object sender, RoutedEventArgs e)
        {
            plane.Map = new Level(plane);
        }

        public void Settransform(Transform transform)
        {
            RenderTransform = transform;
        }

        public void SettransformOrigin(Point pt)
        {
            RenderTransformOrigin = pt;
        }

    }
}
    

