// SoundAssets.cs

using System.ComponentModel;
using System.IO;
using System.Media;
using static System.Media.SoundPlayer;

namespace twoDTDS.Engine
{
    public class SoundAssets
    {
        public static string CD = Directory.GetCurrentDirectory();
        private static string[] Sounds = Directory.GetFiles(CD + "/Sounds", "*.mp3");

        public static string[] sound
        {
            get => Sounds;
            set => Sounds = value;
        }

    }
}
