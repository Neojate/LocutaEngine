using Microsoft.Xna.Framework;

namespace LocutaEngine.Ecs
{
    public class Transform : Component
    {
        //Posición de la entidad en esquina superior izquierda
        private Vector2 position;

        //Tamaño de la entidad
        private Vector2 size;

        //Rotación de la entidad
        private float rotation;

        public Vector2 Position { get { return position; } set { position = value; } }

        public Vector2 Size { get { return size; } set { size = value; } }

        public float Rotation { get { return rotation; } set { rotation = value; } }

        public Vector2 Center { get { return position + size * 0.5f; } }
    }
}
