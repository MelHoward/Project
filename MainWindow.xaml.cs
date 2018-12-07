using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using twoDTDS.Engine;
using twoDTDS.Game;

namespace twoDTDS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            grid.Children.Add(new InGamePlane());
            
        }
    }
}