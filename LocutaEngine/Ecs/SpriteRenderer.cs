using LocutaEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocutaEngine.Ecs
{
    public class SpriteRenderer : Component, IRenderizable
    {
        //Textura de la imagen
        private Texture2D texture;

        //SourceRectangle de la imagen
        private Rectangle sourceRectangle;

        //Color del dibujado
        private Color color;

        //Camara para el dibujado
        private Camera2D camera;

        //Instancia del componente transform
        private Transform transform;

        public Rectangle SourceRectangle { get { return sourceRectangle; } set { sourceRectangle = value; } }

        public Color Color { get { return color; } set { color = value; } }

        public Camera2D Camera { get { return camera; } }

        public SpriteRenderer(Texture2D texture, Rectangle sourceRectangle, Color color, Camera2D camera)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.camera = camera;
        }

        public override void Init()
        {
            transform = entity.GetComponent<Transform>();
        }

        public void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(
                sortMode: SpriteSortMode.Immediate,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.LinearClamp,
                rasterizerState: RasterizerState.CullNone,
                transformMatrix: camera == null ? Matrix.Identity : camera.Transform
            );

            spriteBatch.Draw(
                texture: texture,
                destinationRectangle: new Rectangle((int)transform.Position.X, (int)transform.Position.Y, (int)transform.Size.X, (int)transform.Size.Y),
                sourceRectangle: sourceRectangle,
                color: color,
                rotation: transform.Rotation,
                origin: Vector2.Zero,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );

            spriteBatch.End();
        }
    }
}
