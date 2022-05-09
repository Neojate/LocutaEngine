
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LocutaEngine.Assets
{
    public sealed class Textures : Asset
    {
        private static readonly Lazy<Textures> Lazy = new Lazy<Textures>(() => new Textures());

        public static Textures Instance { get { return Lazy.Value; } }

        private Dictionary<string, Texture2D> data = new Dictionary<string, Texture2D>();

        public void AddTexture(string textureName)
        {
            data.Add(textureName, content.Load<Texture2D>($"images/{textureName}"));
        }

        public void AddTexture(string textureName, string fileRoute)
        {
            data.Add(textureName, content.Load<Texture2D>($"{fileRoute}/{textureName}"));
        }

        public void AddTexture(string textureName, Texture2D texture)
        {
            data.Add(textureName, texture);
        }

        public Texture2D GetTexture(string textureName)
        {
            return data[textureName];
        }

        public void RemoveTexture(string textureName)
        {
            data.Remove(textureName);
        }
    }
}
