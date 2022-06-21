using LocutaEngine.Assets;
using LocutaEngine.Graphics;
using LocutaEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocutaEngine.Screens
{
    public enum ScreenState
    {
        Active,
        Hidden,
        Shutdown
    }

    public abstract class Screen
    {
        public string Name { get { return GetType().Name; } }

        public ScreenState State;

        public Game game;

        protected Canvas canvas;

        protected Input input;
        protected Textures textures;

        #region MUTATORS
        public Canvas Canvas { get { return canvas; } }
        #endregion

        public Screen()
        {
            input = Input.Instance;
            textures = Textures.Instance;
        }

        public virtual void Init()
        {
            
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public void SetGame(Game game)
        {
            this.game = game;
        }

        public void SetCanvas(Canvas canvas)
        {
            this.canvas = canvas;
        }
    }
}
