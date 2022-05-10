using Microsoft.Xna.Framework;

namespace LocutaEngine.Figures
{
    public class Polygon : RigitBody
    {
        private Vector2[] vertices;
        private Vector2[] transformedVertices;

        private bool transformUpdateRequired;

        public Vector2[] Vertices { get { return Vertices; } }
        public Vector2[] TransformedVertices { get { return transformedVertices; } }

        public Polygon(Vector2[] vertices, float mass, float restitution, bool isStatic) : base(mass, restitution, isStatic)
        {

        }

        public Polygon(Vector2 center, int width, int height, float mass, float restitution, bool isStatic) : base(mass, restitution, isStatic)
        {
            this.center = center;

            float left = -width / 2f;
            float right = left + width;
            float bottom = -height / 2f;
            float top = bottom + height;

            this.vertices = new Vector2[4]
            {
                new Vector2(left, top),
                new Vector2(right, top),
                new Vector2(right, bottom),
                new Vector2(left, bottom)
            };
            transformedVertices = new Vector2[4];

            TransformVertices();
        }

        public void TransformVertices()
        {
            Matrix matrix = Matrix.CreateTranslation(new Vector3(center.X, center.Y, 0));

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 vertex = vertices[i];
                transformedVertices[i] = Vector2.Transform(vertex, matrix);
            }
        }
    }
}
