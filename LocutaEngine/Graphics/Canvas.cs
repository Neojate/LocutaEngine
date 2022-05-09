using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocutaEngine.Graphics
{
    public class Canvas
    {
        private RenderTarget2D renderTarget2D;

        private Game game;

        public int Width { get { return renderTarget2D.Width; } }
        public int Height { get { return renderTarget2D.Height; } }
        public Vector2 Center { get { return new Vector2(Width, Height) * 0.5f; } }

        public Game Game { get { return game; } }

        public Canvas(Game game, int width, int height)
        {
            this.game = game;

            renderTarget2D = new RenderTarget2D(game.GraphicsDevice, width, height);
        }

        public void Set()
        {
            game.GraphicsDevice.SetRenderTarget(renderTarget2D);
        }

        public void Unset()
        {
            game.GraphicsDevice.SetRenderTarget(null);
        }

        public void Present(SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Pink);

            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget2D, CalculateDestinationRectangle(), Color.White);
            spriteBatch.End();
        }

        public Rectangle CalculateDestinationRectangle()
        {
            Rectangle backbufferBounds = game.GraphicsDevice.PresentationParameters.Bounds;
            float backBufferAspectRatio = (float)backbufferBounds.Width / backbufferBounds.Height;
            float screenAspectRatio = (float)Width / Height;

            float rx = 0f;
            float ry = 0f;
            float rw = backbufferBounds.Width;
            float rh = backbufferBounds.Height;

            if (screenAspectRatio > backBufferAspectRatio)
            {
                rh = rw / screenAspectRatio;
                ry = (backbufferBounds.Height - rh) / 2f;
            }
            else if (screenAspectRatio < backBufferAspectRatio)
            {
                rw = rh * screenAspectRatio;
                rx = backbufferBounds.Width / 2f;
            }

            return new Rectangle((int)rx, (int)ry, (int)rw, (int)rh);
        }
    }
}
