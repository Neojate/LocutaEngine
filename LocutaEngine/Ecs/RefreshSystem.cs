using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LocutaEngine.Ecs
{
    public static class RefreshSystem 
    {
        private static List<IRefreshable> refreshables = new List<IRefreshable>();

        public static void Register(IRefreshable refreshable)
        {
            refreshables.Add(refreshable);
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < refreshables.Count; i++)
                refreshables[i].Update(gameTime);
        }
    }
}
