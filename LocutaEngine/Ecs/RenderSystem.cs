using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LocutaEngine.Ecs
{
    public class RenderSystem<T> where T : Component, IRenderizable
    {
        private static List<T> components = new List<T>();

        public static void Register(T component)
        {
            components.Add(component);
        }

        public static void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
                components[i].Render(spriteBatch, gameTime);
        }
    }
}
