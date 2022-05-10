﻿using LocutaEngine.Assets;
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

        protected bool hasInit = false;

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

        protected virtual void Init()
        {
            hasInit = true;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

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
