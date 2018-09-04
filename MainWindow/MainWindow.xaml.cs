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
using System.Windows.Threading;

namespace Project
{
    public partial class MainWindow : Window
    {       
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            Project.App app = new Project.App();
            app.InitializeComponent();
            app.Run();
        }

        enum Direction { left, right, up, down, none };
        Direction _direction = Direction.none;
        bool _directionIsPressed = false;
        bool _directionIsReleased = false;
        double x = 0;
        double y = 0;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(MovePlayer);
            timer.Start();
        }
        private void MovePlayer(object sender, EventArgs e)
        {

            if(Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.Up))
            {
                ImageBrush img = new ImageBrush();
                img.ImageSource = new BitmapImage(new Uri(@"/Resources/up.png", UriKind.Relative));
                Player.Fill = img;
                y -= .05;
                Canvas.SetTop(Player, y);

            }
            if (Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.Down))
            {
                ImageBrush img = new ImageBrush();
                img.ImageSource = new BitmapImage(new Uri(@"/Resources/down.png", UriKind.Relative));
                Player.Fill = img;
                y += .05;
                Canvas.SetTop(Player, y);
            }
            if (Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.Left))
            {
                ImageBrush img = new ImageBrush();
                img.ImageSource = new BitmapImage(new Uri(@"/Resources/left.png", UriKind.Relative));
                Player.Fill = img;
                x -= .05;
                Canvas.SetLeft(Player, x);
            }
            if (Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.Right))
            {
                ImageBrush img = new ImageBrush();
                img.ImageSource = new BitmapImage(new Uri(@"/Resources/right.png", UriKind.Relative));
                Player.Fill = img;
                x += .05;
                Canvas.SetLeft(Player, x);
            }
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    _direction = Direction.up;
                    _directionIsPressed = true;
                    break;
                case Key.Down:
                    _direction = Direction.down;
                    _directionIsPressed = true;
                    break;
                case Key.Left:
                    _direction = Direction.left;
                    _directionIsPressed = true;
                    break;
                case Key.Right:
                    _direction = Direction.right;
                    _directionIsPressed = true;
                    break;
                default:
                    _direction = Direction.none;
                    break;
            }
        }
        private void OnKeyRelease(Object sender, KeyEventArgs e)
        { 
            if (Keyboard.IsKeyDown(Key.Down))
                _direction = Direction.down;
            else if (Keyboard.IsKeyDown(Key.Up))
                _direction = Direction.up;
            else if (Keyboard.IsKeyDown(Key.Left))
                _direction = Direction.left;
            else if (Keyboard.IsKeyDown(Key.Right))
                _direction = Direction.right;
            else
                _direction = Direction.none;
        }

    }
}
