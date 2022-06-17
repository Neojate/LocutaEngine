using LocutaEngine.Ecs;
using Microsoft.Xna.Framework;
using System;

namespace LocutaEngine.Physics
{
    public class Collision
    {
        public bool Collide(Collider colliderA, Collider colliderB, out Vector2 normal, out float depth)
        {
            normal = Vector2.Zero;
            depth = 0f;

            if (colliderA.BodyType == BodyType.Circle && colliderB.BodyType == BodyType.Circle)
                return CircleCollision(colliderA, colliderB, out normal, out depth);

            if (colliderA.BodyType == BodyType.Circle && colliderB.BodyType == BodyType.Polygon)
                return CirclePolygonCollision(colliderA, colliderB, out normal, out depth);

            if (colliderA.BodyType == BodyType.Polygon && colliderB.BodyType == BodyType.Circle)
                return CirclePolygonCollision(colliderB, colliderA, out normal, out depth);

            return false;
        }

        private bool CircleCollision(Collider colliderA, Collider colliderB, out Vector2 normal, out float depth)
        {
            normal = Vector2.Zero;
            depth = 0f;

            Renderer rendererA = colliderA.Entity.GetComponent<Renderer>();
            Renderer rendererB = colliderB.Entity.GetComponent<Renderer>();

            float distance = Vector2.Distance(rendererA.Center, rendererB.Center);
            float totalRadius = rendererA.Size.X * 0.5f + rendererB.Size.X * 0.5f;

            if (distance > totalRadius)
                return false;

            normal = Vector2.Normalize(rendererB.Center - rendererA.Center);
            depth = totalRadius - distance;

            return true;
        }

        private bool CirclePolygonCollision(Collider circle, Collider polygon, out Vector2 normal, out float depth)
        {
            normal = Vector2.Zero;
            depth = float.MaxValue;

            Vector2 axis = Vector2.Zero;
            float axisDepth = 0f;

            float minA, maxA, minB, maxB;

            Renderer circleRenderer = circle.Entity.GetComponent<Renderer>();

            Vector2[] vertices = polygon.TransformedVertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 va = vertices[i];
                Vector2 vb = vertices[(i + 1) % vertices.Length];

                Vector2 edge = vb - va;
                axis = new Vector2(-edge.Y, edge.X);
                axis = Vector2.Normalize(axis);

                ProjectVertices(vertices, axis, out minA, out maxA);
                ProjectCircle(circleRenderer.Center, circleRenderer.Size.X * 0.5f, axis, out minB, out maxB);

                if (minA >= maxB || minB >= maxA)
                    return false;

                axisDepth = (float)Math.Min(maxB - minA, maxA - minB);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            int cpIndex = FindClosestPointOnPolygon(circle.Center, vertices);
            Vector2 cp = vertices[cpIndex];

            axis = cp - circle.Center;
            axis = Vector2.Normalize(axis);

            ProjectVertices(vertices, axis, out minA, out maxA);
            ProjectCircle(circleRenderer.Center, circleRenderer.Size.X * 0.5f, axis, out minB, out maxB);

            if (minA >= maxB || minB >= maxA)
                return false;

            axisDepth = (float)Math.Min(maxB - minA, maxA - minB);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }

            Vector2 direction = polygon.Center - circle.Center;

            if (Vector2.Dot(direction, normal) < 0f)
                normal = -normal;

            return true;
        }

        public void ResolveCollision(Collider colliderA, Collider colliderB, Vector2 normal, float depth)
        {
            Vector2 relativeVelocity = colliderB.LinearVelocity - colliderA.LinearVelocity;

            float e = (float)Math.Min(colliderA.Restitution, colliderB.Restitution);

            float j = -(1f + e) * Vector2.Dot(relativeVelocity, normal);
            j /= colliderA.InvMass + colliderB.InvMass;

            Vector2 impulse = j * normal;

            colliderA.LinearVelocity -= impulse * colliderA.InvMass;
            colliderB.LinearVelocity += impulse * colliderB.InvMass;
        }

        private void ProjectVertices(Vector2[] vertices, Vector2 axis, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 v = vertices[i];
                float projection = Vector2.Dot(v, axis);

                if (projection < min)
                    min = projection;
                if (projection > max)
                    max = projection;
            }
        }

        private void ProjectCircle(Vector2 center, float radius, Vector2 axis, out float min, out float max)
        {
            Vector2 direction = Vector2.Normalize(axis);
            Vector2 directionAndRadius = direction * radius;

            Vector2 p1 = center + directionAndRadius;
            Vector2 p2 = center - directionAndRadius;

            min = Vector2.Dot(p1, axis);
            max = Vector2.Dot(p2, axis);

            if (min > max)
            {
                float t = min;
                min = max;
                max = t;
            }
        }

        private int FindClosestPointOnPolygon(Vector2 circleCenter, Vector2[] vertices)
        {
            int result = -1;
            float minDistance = float.MaxValue;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 v = vertices[i];
                float distance = Vector2.Distance(v, circleCenter);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = i;
                }
            }
            return result;
        }
    }
}
