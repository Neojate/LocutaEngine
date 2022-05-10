﻿using LocutaEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocutaEngine.Screens
{
    public class ScreenManager
    {
        private static Lazy<ScreenManager> lazy = new Lazy<ScreenManager>(() => new ScreenManager());

        public static ScreenManager Instance { get { return lazy.Value; } }

        private List<Screen> screens = new List<Screen>();

        private Game game;

        private Canvas canvas;

        internal Game Game { get { return game; } set { game = value; } }

        internal Canvas Canvas { get { return canvas; } set { canvas = value; } }

        public void AddScreen(Screen screen)
        {
            screen.SetGame(game);
            screen.SetCanvas(canvas);
            screens.Add(screen);
        }

        public void RemoveScreen(string screenName)
        {
            screens.Find(screen => screen.Name == screenName).State = ScreenState.Shutdown;
        }

        public T GetScreen<T>(string screenName) where T : Screen
        {
            return (T)screens.Find(screen => screen.Name == screenName);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < screens.Count; i++)
            {
                Screen screen = screens[i];
                if (screen.State == ScreenState.Shutdown)
                    screens.RemoveAt(i);

                if (screen.State == ScreenState.Active)
                    screen.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < screens.Count; i++)
                if (screens[i].State != ScreenState.Shutdown)
                    screens[i].Draw(spriteBatch, gameTime);
        }
    }
}
