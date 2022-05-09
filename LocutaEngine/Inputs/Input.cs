using LocutaEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace LocutaEngine.Inputs
{
    public enum ButtonType
    {
        LeftButton, RightButton
    }

    public sealed class Input
    {
        private static readonly Lazy<Input> lazy = new Lazy<Input>(() => new Input());

        public static Input Instance { get { return lazy.Value; } }

        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;

        private MouseState previousMouseState;
        private MouseState currentMouseState;

        private Canvas canvas;

        private Camera2D camera2d;

        public Canvas Canvas { set { canvas = value; } }

        public Camera2D Camera2d { set { camera2d = value; } }

        public Vector2 MousePosition { get { return new Vector2(currentMouseState.X, currentMouseState.Y); } }

        public Vector2 CanvasPosition
        {
            get
            {
                Rectangle screenDestinationRectangle = canvas.CalculateDestinationRectangle();

                float sx = (MousePosition.X - screenDestinationRectangle.X) / (float)screenDestinationRectangle.Width;
                float sy = (MousePosition.Y - screenDestinationRectangle.Y) / (float)screenDestinationRectangle.Height;

                sx *= (float)canvas.Width;
                sy *= (float)canvas.Height;

                return new Vector2((int)sx, (int)sy);
            }
        }

        public Vector2 MouseCameraPosition
        {
            get
            {
                Matrix invert = Matrix.Invert(camera2d.Transform);
                return Vector2.Transform(CanvasPosition, invert);
            }
        }

        public Point WindowPosition { get { return currentMouseState.Position; } }

        public Input()
        {
            previousKeyboardState = Keyboard.GetState();
            currentKeyboardState = previousKeyboardState;

            previousMouseState = Mouse.GetState();
            currentMouseState = previousMouseState;
        }

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyClicked(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }

        public bool IsButtonDown()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsButtonDown(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.LeftButton:
                    return currentMouseState.LeftButton == ButtonState.Pressed;
                case ButtonType.RightButton:
                    return currentMouseState.RightButton == ButtonState.Pressed;
                default:
                    return false;
            }
        }

        public bool IsButtonClicked()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }

        public bool IsButtonClicked(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.LeftButton:
                    return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
                case ButtonType.RightButton:
                    return currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;
                default:
                    return false;
            }
        }

        public bool IsWheelUp()
        {
            return previousMouseState.ScrollWheelValue < currentMouseState.ScrollWheelValue;
        }

        public bool IsWheelDown()
        {
            return previousMouseState.ScrollWheelValue > currentMouseState.ScrollWheelValue;
        }
    }
}
