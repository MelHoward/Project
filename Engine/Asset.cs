using System;
using System.IO;


namespace twoDTDS.Engine
{

    /*---------------------------------------------------------------------------------------
                                         ASSET 
    ---------------------------------------------------------------------------------------*/
    public static class Asset
    {
        private static string[] Hero = Directory.GetFiles(@".\Assets\Hero", "*.png", SearchOption.AllDirectories);
        private static string[] Bullet = Directory.GetFiles(@".\Assets\Bullet", "*.png", SearchOption.AllDirectories);
        private static string[] Enemy = Directory.GetFiles(@".\Assets\Enemy", "*.png", SearchOption.AllDirectories);

        public static string[] hero  { get => Hero; set => Hero = value; }
        public static string[] bullet { get => Bullet; set => Bullet = value; }
        public static string[] enemy { get => Enemy; set => Enemy = value; }
    }
}
