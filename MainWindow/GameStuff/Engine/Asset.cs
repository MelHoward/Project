using System.IO;

namespace twoDTDS.Engine
{
    public static class Asset
    {
        private static string Cd = Directory.GetCurrentDirectory();
        private static string[] paths = Directory.GetFiles(Cd, "*.png", SearchOption.AllDirectories);

        public static string[] Paths { get => paths; set => paths = value; }
    }
}
