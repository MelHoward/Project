using System.IO;

namespace twoDTDS.Engine
{
    /*---------------------------------------------------------------------------------------
                                         ASSET 
    ---------------------------------------------------------------------------------------*/
    public  class Asset
    {
        public  static string CD = Directory.GetCurrentDirectory();
        private static string[] HeroBack = Directory.GetFiles(CD + "/HeroBack", "*.png");
        private static string[] HeroFront = Directory.GetFiles(CD + "/HeroFront", "*.png");
        private static string[] HeroLeft = Directory.GetFiles(CD + "/HeroLeft", "*.png");
        private static string[] HeroRight = Directory.GetFiles(CD + "/HeroRight", "*.png");
        private static string[] HeroIdle = Directory.GetFiles(CD + "/HeroIdle", "*.png");
        private  static string[] Bullet = Directory.GetFiles(CD + "/Bullet", "*.png");
        private  static string[] Enemy = Directory.GetFiles(CD + "/Enemy", "*.png");
        private  static string[] Environment = Directory.GetFiles(CD + "/Environment ", "*.png");

        public static  string[] heroBack { get =>HeroBack; set => HeroBack = value; }
        public static  string[] heroFront { get =>HeroFront; set => HeroFront = value; }
        public static  string[] heroLeft { get =>HeroLeft; set => HeroLeft = value; }
        public static  string[] heroIdle{ get =>HeroIdle; set => HeroIdle = value; }
        public static  string[] heroRight { get =>HeroRight; set => HeroRight = value; }
        public static string[] bullet { get => Bullet; set => Bullet = value; }
        public static string[] enemy { get => Enemy; set => Enemy = value; }
        public static string[] env { get => Environment; set => Environment = value; }
    }
}
