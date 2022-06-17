using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocutaEngine.Ecs
{
    public interface IRenderizable
    {
        void Render(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
