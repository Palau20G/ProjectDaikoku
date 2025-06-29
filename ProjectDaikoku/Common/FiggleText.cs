using System;
using Figgle;
using Figgle.Fonts;
namespace ProjectDaikoku.Common
{
    public class BannerTextConfig
    {
        public void BannerText(string bannerName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(FiggleFonts.Banner3D.Render(bannerName));
            Console.ResetColor();
        }
    }

}