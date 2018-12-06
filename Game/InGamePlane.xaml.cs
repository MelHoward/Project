using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using twoDTDS.Engine;


namespace twoDTDS.Game
{
    public partial class InGamePlane : UserControl, IPlayAreaControl
    {
        MediaPlayer player = new MediaPlayer( );
        public InGamePlane()
        {
            InitializeComponent();
            PlaybackMusic();
          
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
        public void PlaybackMusic ()
        {
            if( player != null )
            {
                player.Open(new Uri(SoundAssets.sound[0]));
                player.MediaEnded += new EventHandler(Media_Ended);
                player.Play( );

                return;
            }
        }

        private void Media_Ended (object sender, EventArgs e)
        {
            player.Position = TimeSpan.Zero;
            player.Play( );
        }
    }
}
