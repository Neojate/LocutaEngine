using LocutaEngine.Figures;
using Microsoft.Xna.Framework;
using System;

namespace LocutaEngine.Physics
{
    public class Collision
    {
        public bool Collide(RigitBody bodyA, RigitBody bodyB, out Vector2 normal, out float depth)
        {
            normal = Vector2.Zero;
            depth = 0f;

            if (bodyA is Circle)
            {
                Circle circleA = (Circle)bodyA;
                if (bodyB is Circle)
                {
                    Circle circleB = (Circle)bodyB;
                    return CircleCollision(circleA, circleB, out normal, out depth);
                }
                else if (bodyB is Polygon)
                {
                    Polygon polygonB = (Polygon)bodyB;
                    return CirclePolygonCollision(circleA, polygonB, out normal, out depth);
                }
            }
            else if (bodyA is Polygon)
            {
                Polygon polygonA = (Polygon)bodyA;
                if (bodyB is Circle)
                {
                    Circle circleB = (Circle)bodyB;
                    return CirclePolygonCollision(circleB, polygonA, out normal, out depth);
                }
                else if (bodyB is Polygon)
                {
                    Polygon polygonB = (Polygon)bodyB;
                }
            }
            return false;
        }

        public void ResolveCollision(RigitBody bodyA, RigitBody bodyB, Vector2 normal, float depth)
        {
            Vector2 relativeVelocity = bodyB.LinearVelocity - bodyA.LinearVelocity;

            float e = (float)Math.Min(bodyA.Restitution, bodyB.Restitution);

            float j = -(1f + e) * Vector2.Dot(relativeVelocity, normal);
            j /= bodyA.InvMass + bodyB.InvMass;

            Vector2 impulse = j * normal;

            
            bodyA.LinearVelocity -= impulse * bodyA.InvMass;    
            bodyB.LinearVelocity += impulse * bodyB.InvMass;
        }

        private bool CircleCollision(Circle circleA, Circle circleB, out Vector2 normal, out float depth)
        {
            normal = Vector2.Zero;
            depth = 0f;

            float distance = Vector2.Distance(circleA.Center, circleB.Center);
            float totalRadius = circleA.Radius + circleB.Radius;

            if (distance > totalRadius)
                return false;

            normal = Vector2.Normalize(circleB.Center - circleA.Center);
            depth = totalRadius - distance;
            return true;
        }

        private bool CirclePolygonCollision(Circle circle, Polygon polygon, out Vector2 normal, out float depth)
        {
            normal = Vector2.Zero;
            depth = float.MaxValue;

            Vector2 axis = Vector2.Zero;
            float axisDepth = 0f;

            float minA, maxA, minB, maxB;

            Vector2[] vertices = polygon.TransformedVertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 va = vertices[i];
                Vector2 vb = vertices[(i + 1) % vertices.Length];

                Vector2 edge = vb - va;
                axis = new Vector2(-edge.Y, edge.X);
                axis = Vector2.Normalize(axis);

                ProjectVertices(vertices, axis, out minA, out maxA);
                ProjectCircle(circle.Center, circle.Radius, axis, out minB, out maxB);

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
            ProjectCircle(circle.Center, circle.Radius, axis, out minB, out maxB);

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
