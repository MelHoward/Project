using System;
using System.IO;


namespace twoDTDS.Engine
{
    public static class Asset
    {
        private static string Cd = Directory.GetCurrentDirectory();
        private static string[] Paths = Directory.GetFiles(Cd, "*.png", SearchOption.AllDirectories);

        public static string[] paths { get => Paths; set => Paths = value; }
    }
}
