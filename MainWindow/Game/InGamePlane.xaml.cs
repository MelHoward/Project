using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
