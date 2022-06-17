using LocutaEngine.Ecs;
using Microsoft.Xna.Framework;

namespace LocutaEngine.Physics
{
    public class ColliderSystem : SystemBase<Collider>
    {
        private static Collision collision = new Collision();

        public new static void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
                components[i].Step(gameTime);

            for (int i = 0; i < components.Count - 1; i++)
            {
                Collider colliderA = components[i];
                for (int j = i + 1; j < components.Count; j++)
                {
                    Collider colliderB = components[j];
                    if (collision.Collide(colliderA, colliderB, out Vector2 normal, out float depth)) 
                    {
                        if (colliderA.IsStatic)
                        {
                            colliderB.Move(-normal * depth);
                        }
                        else if (colliderB.IsStatic)
                        {
                            colliderA.Move(-normal * depth);
                        }
                        else
                        {
                            colliderA.Move(-normal * depth * 0.5f);
                            colliderB.Move(normal * depth * 0.5f);
                        }

                        colliderA.HasCollide = true;
                        colliderB.HasCollide = true;

                        collision.ResolveCollision(colliderA, colliderB, normal, depth);

                        OnCollision(colliderA, colliderB);
                    }

                }
            }

        }

        protected static void OnCollision(Collider colliderA, Collider colliderB)
        {

        }
    }
}
