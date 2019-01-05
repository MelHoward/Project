using System.IO;

namespace twoDTDS.Engine
{
    /*---------------------------------------------------------------------------------------
                                         ASSET 
    ---------------------------------------------------------------------------------------*/
    public static class Asset
    {
        public static string CD = Directory.GetCurrentDirectory();
        private static string[] Hero = Directory.GetFiles(CD + "/Hero", "*.png");
        private static string[] Bullet = Directory.GetFiles(CD + "/Bullet", "*.png");
        private static string[] Enemy = Directory.GetFiles(CD + "/Enemy", "*.png");
        private static string[] Environment = Directory.GetFiles(CD + "/Environment ", "*.png");

        public static string[] hero
        {
            get => Hero;
            set => Hero = value;
        }

        public static string[] bullet
        {
            get => Bullet;
            set => Bullet = value;
        }

        public static string[] enemy
        {
            get => Enemy;
            set => Enemy = value;
        }

        public static string[] env
        {
            get => Environment;
            set => Environment = value;
        }
    }
}