using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LocutaEngine.Assets
{
    public class Fonts : Asset
    {
        private static readonly Lazy<Fonts> Lazy = new Lazy<Fonts>(() => new Fonts());

        public static Fonts Instance { get { return Lazy.Value; } }

        private Dictionary<string, SpriteFont> data = new Dictionary<string, SpriteFont>();

        public void AddFont(string fontName)
        {
            data.Add(fontName, content.Load<SpriteFont>($"fonts/{fontName}"));
        }

        public void AddFont(string fontName, string fileRoute)
        {
            data.Add(fontName, content.Load<SpriteFont>($"{fileRoute}/{fontName}"));
        }

        public void AddFont(string fontName, SpriteFont font)
        {
            data.Add(fontName, font);
        }

        public SpriteFont GetFont(string fontName)
        {
            return data[fontName];
        }

        public void RemoveFont(string fontName)
        {
            data.Remove(fontName);
        }
    }
}
