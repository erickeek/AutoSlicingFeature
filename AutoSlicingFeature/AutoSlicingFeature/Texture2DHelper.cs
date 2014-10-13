using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AutoSlicingFeature
{
    public static class Texture2DHelper
    {
        public static Color GetPixel(this Color[] colors, int x, int y, int width)
        {
            return colors[x + (y * width)];
        }

        public static bool IsTransparent(this Color[] colors, int x, int y, int width)
        {
            return colors.GetPixel(x, y, width) == Color.Transparent;
        }

        public static Color[] GetPixels(this Texture2D texture)
        {
            var colors = new Color[texture.Width * texture.Height];
            texture.GetData(colors);
            return colors;
        }
    }
}