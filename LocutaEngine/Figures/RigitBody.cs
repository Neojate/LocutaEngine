using Microsoft.Xna.Framework;
using System;

namespace LocutaEngine.Figures
{
    public class RigitBody
    {
        protected Vector2 center;

        protected Vector2 linearVelocity;
        protected Vector2 linearForce;

        protected float mass;
        protected float invMass;
        protected float restitution;

        protected bool isStatic;

        protected bool hasCollide;

        protected Action onCollision;

        public Vector2 Center { get { return center; } set { center = value; } }

        public Vector2 LinearVelocity { get { return linearVelocity; } set { linearVelocity = value; } }

        public float InvMass { get { return invMass; } }

        public float Restitution { get { return restitution; } }

        public bool IsStatic { get { return isStatic; } }

        public bool HasCollide { get { return hasCollide; } set { hasCollide = value; } }

        public RigitBody(float mass, float restitution, bool isStatic)
        {
            this.mass = mass;
            this.invMass = isStatic ? 0 : 1 / mass;

            this.restitution = restitution;

            this.isStatic = isStatic;
            this.hasCollide = false;
        }

        public RigitBody(float mass, float restitution, bool isStatic, Action onCollision)
        {
            this.mass = mass;
            this.invMass = isStatic ? 0 : 1 / mass;
            this.restitution = restitution;

            this.isStatic = isStatic;
            this.hasCollide = false;

            this.onCollision = onCollision;
        }

        public void ApplyForce(Vector2 amount)
        {
            linearForce = amount;
        }

        public void Move(Vector2 amount)
        {
            center += amount;
        }

        public void MoveTo(Vector2 position)
        {
            center = position;
        }

        public void Step(GameTime gameTime)
        {
            if (IsStatic)
                return;

            float friction = 0.99f;

            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 acceleration = linearForce * invMass;

            linearVelocity += acceleration * time;

            LinearVelocity *= friction;

            center += linearVelocity * time;

            if (Math.Abs(linearVelocity.X) < 1 && Math.Abs(linearVelocity.Y) < 1)
                LinearVelocity = Vector2.Zero;

            linearForce = Vector2.Zero;
        }
    }
}
