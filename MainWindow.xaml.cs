using System.Windows;
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