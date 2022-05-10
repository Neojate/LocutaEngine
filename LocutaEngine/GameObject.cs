using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocutaEngine
{
    public abstract class GameObject
    {
        protected Texture2D texture;
        protected Color color = Color.White;

        protected Vector2 position;
        protected Vector2 size;

        protected float rotation;

        #region MUTATORS
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Size { get { return size; } set { size = value; } }
        public float Rotation { get { return rotation; } set { rotation = value; } }
        #endregion

        public GameObject()
        {
            color = Color.White;

            position = Vector2.Zero;
            size = new Vector2(100, 100);

            rotation = 0f;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Render(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
