using LocutaEngine.Physics;
using Microsoft.Xna.Framework;
using System;

namespace LocutaEngine.Figures
{
    public class Circle : RigitBody
    {
        private float radius;

        public float Radius { get { return radius; } }

        public Circle(Vector2 center, float radius, float mass, float restitution, bool isStatic) : base(mass, restitution, isStatic)
        {
            this.center = center;
            this.radius = radius;
        }
    }
}
