using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocutaEngine
{
    public struct Portrait
    {
        public Texture2D Texture { get; set; }

        public Rectangle SourceRectangle { get; set; }

        public Color Color { get; set; }

        public Portrait(Texture2D texture, Rectangle sourceRectangle, Color color)
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
            Color = color;
        }
    }
}
