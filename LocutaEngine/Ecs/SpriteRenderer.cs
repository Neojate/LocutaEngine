using LocutaEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocutaEngine.Ecs
{
    public class SpriteRenderer : Component
    {
        //Textura de la imagen
        private Texture2D texture;

        //SourceRectangle de la imagen
        private Rectangle sourceRectangle;

        //Camara para el dibujado
        private Camera2D camera;

        //Instancia del componente transform
        private Transform transform;

        public SpriteRenderer(Texture2D texture, Rectangle sourceRectangle, Camera2D camera)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRectangle;
            this.camera = camera;
            
            transform = entity.GetComponent<Transform>();
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
                destinationRectangle: new Rectangle((int)transform.Position.X, (int)transform.Position.Y, (int)transform.Size.X, (int)transform.Size.Y),
                sourceRectangle: sourceRectangle,
                color: Color.White,
                rotation: transform.Rotation,
                origin: Vector2.Zero,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );

            spriteBatch.End();
        }
    }
}
