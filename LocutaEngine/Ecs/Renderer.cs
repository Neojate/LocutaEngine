using LocutaEngine.Ecs;
using LocutaEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocutaEngine.Ecs
{
    public class Renderer : Component, IRenderizable
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 size;
        private Rectangle sourceRectangle;
        private Color color;
        private Camera2D camera;
        private float rotation;

        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Size { get { return size; } set { size = value; } }
        public Rectangle SourceRectangle { get { return sourceRectangle; } set { sourceRectangle = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public Camera2D Camera { get { return camera; } set { camera = value; } }
        public float Rotation { get { return rotation; } set { rotation = value; } }

        public Vector2 Center { get { return position + size * 0.5f; } }

        public Renderer() { }

        public Renderer(Texture2D texture, Vector2 position, Vector2 size, Rectangle sourceRectangle, Camera2D camera)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.sourceRectangle = sourceRectangle;
            this.color = Color.White;
            this.camera = camera;
            this.rotation = 0f;
        }

        public Renderer(Texture2D texture, Vector2 position, Vector2 size, Rectangle sourceRectangle, Color color, Camera2D camera)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.camera = camera;
            this.rotation = 0f;
        }

        public Renderer(Texture2D texture, Vector2 position, Vector2 size, Rectangle sourceRectangle, Color color, Camera2D camera, float rotation)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.camera = camera;
            this.rotation = rotation;
        }

        public void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(
                sortMode: SpriteSortMode.Immediate,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp,
                rasterizerState: RasterizerState.CullNone,
                transformMatrix: camera == null ? Matrix.Identity : camera.Transform
            );

            spriteBatch.Draw(
                texture: texture,
                destinationRectangle: new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y),
                sourceRectangle: sourceRectangle,
                color: color,
                rotation: rotation,
                origin: Vector2.Zero,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );

            spriteBatch.End();
        }
    }
}
