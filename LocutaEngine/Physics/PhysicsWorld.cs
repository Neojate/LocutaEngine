using LocutaEngine.Figures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LocutaEngine.Physics
{
    public class PhysicsWorld
    {
        protected List<GameObject> gameObjects;

        public PhysicsWorld(List<GameObject> gameObjects)
        {
            this.gameObjects = gameObjects;
        }

        public void Update(GameTime gameTime)
        {
            //int iterations = 24;
            //for (int iteration = 0; iteration < iterations; iteration++)
            //{
                //Añadir los colliders
                List<ICollidable> colidables = new List<ICollidable>();
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i] is ICollidable)
                    {
                        ICollidable colideObject = (ICollidable)gameObjects[i];
                        colidables.Add(colideObject);

                        colideObject.RigitBody.Step(gameTime);
                    }
                }

                Collision collision = new Collision();

                for (int i = 0; i < colidables.Count - 1; i++)
                {
                    RigitBody bodyA = colidables[i].RigitBody;
                    for (int j = i + 1; j < colidables.Count; j++)
                    {
                        RigitBody bodyB = colidables[j].RigitBody;

                        if (collision.Collide(bodyA, bodyB, out Vector2 normal, out float depth))
                        {
                            if (bodyA.IsStatic)
                            {
                                bodyB.Move(-normal * depth);
                            }
                            else if (bodyB.IsStatic)
                            {
                                bodyA.Move(-normal * depth);
                            }
                            else
                            {
                                bodyA.Move(-normal * depth / 2f);
                                bodyB.Move(normal * depth / 2f);
                            }

                            bodyA.HasCollide = true;
                            bodyB.HasCollide = true;

                            collision.ResolveCollision(bodyA, bodyB, normal, depth);

                            OnCollision(bodyA, bodyB);
                        }
                    }
                }
            //}

        }

        protected virtual void OnCollision(RigitBody bodyA, RigitBody bodyB)
        {

        }
    }
}
