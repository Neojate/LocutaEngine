using LocutaEngine.Assets;
using LocutaEngine.Graphics;
using LocutaEngine.Inputs;
using LocutaEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocutaEngine.Main
{
    public class Locuta
    {
        private ScreenManager screenManager = ScreenManager.Instance;

        private Textures textures = Textures.Instance;
        private Fonts fonts = Fonts.Instance;
        private Effects effects = Effects.Instance;

        private Input input = Input.Instance;

        private Game game;

        private Canvas canvas;

        public Locuta(Game game, int width, int height)
        {
            this.game = game;

            canvas = new Canvas(game, width, height);

            screenManager.Game = game;
            screenManager.Canvas = canvas;

            input.Canvas = canvas;

            textures.SetContent(game.Content);
            fonts.SetContent(game.Content);
            effects.SetContent(game.Content);
        }

        public void Update(GameTime gameTime)
        {
            input.Update();

            screenManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            canvas.Set();

            screenManager.Draw(spriteBatch, gameTime);

            canvas.Unset();
            canvas.Print(spriteBatch);
        }
    }
}
