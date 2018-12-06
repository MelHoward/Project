using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twoDTDS.Engine
{
    class SoundAssets
    {
        public static string CD = Directory.GetCurrentDirectory();
        private static string[] Sounds = Directory.GetFiles(CD + "/SoundAssets", "*.mp3");
        public static string[] snd { get => Sounds ; set => Sounds = value; }

    }
}
