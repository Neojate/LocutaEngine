using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LocutaEngine.Ecs
{
    public class SystemBase<T> where T : Component, IRefreshable
    {
        protected static List<T> components = new List<T>();

        public static void Register(T component)
        {
            components.Add(component);
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
                components[i].Update(gameTime);
        }
    }
}
