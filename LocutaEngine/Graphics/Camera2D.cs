using LocutaEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace LocutaEngine.Graphics
{
    public class Camera2D
    {
        public Keys MoveUpKey = Keys.Up;
        public Keys MoveDownKey = Keys.Down;
        public Keys MoveLeftKey = Keys.Left;
        public Keys MoveRightKey = Keys.Right;

        private Input input;

        private Canvas canvas;

        private float z;
        private Matrix transform;
        private Vector2 position;
        private float rotation;

        public Matrix Transform { get { return transform; } }
        public Vector2 Position { get { return position; } }
        public float Rotation { get { return rotation; } }

        public Camera2D(Canvas canvas)
        {
            input = Input.Instance;
            this.canvas = canvas;

            z = 1f;
            rotation = 0f;
            position = Vector2.Zero;

            UpdateMatrix();
        }

        public void Move(Vector2 amount)
        {
            position += amount;
            UpdateMatrix();
        }

        public void Zoom(float amount)
        {
            z += amount;
            if (z < 0.1f)
                z = 0.1f;

            UpdateMatrix();
        }

        public void Rotate(float amount)
        {
            rotation += amount;
            UpdateMatrix();
        }

        public void UpdateMatrix()
        {
            transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(new Vector3(z, z, 1)) *
                Matrix.CreateTranslation(new Vector3(canvas.Width * 0.5f, canvas.Height * 0.5f, 0));
        }

        public void Update(GameTime gameTime)
        {
            if (input.IsKeyDown(MoveUpKey))
                Move(new Vector2(0, -10));
            if (input.IsKeyDown(MoveDownKey))
                Move(new Vector2(0, 10));
            if (input.IsKeyDown(MoveLeftKey))
                Move(new Vector2(-10, 0));
            if (input.IsKeyDown(MoveRightKey))
                Move(new Vector2(10, 0));
            if (input.IsWheelDown())
                Zoom(-0.1f);
            if (input.IsWheelUp())
                Zoom(0.1f);
            if (input.IsKeyDown(Keys.R))
                Rotate(0.1f);

        }

    }
}
