﻿using LocutaEngine.Physics;
using Microsoft.Xna.Framework;
using System;

namespace LocutaEngine.Ecs
{
    public class Collider : Component, IRefreshable
    {
        //Tipo de cuerpo
        private BodyType bodyType;

        //Velocidad en 2D
        private Vector2 linearVelocity;
        //Fuerza en 2D
        private Vector2 linearForce;

        //Masa
        private float mass;
        //Masa invertida
        private float invMass;
        //Capacidad de Rebote [0-1]
        private float restitution;

        //Si el elemento es estático
        private bool isStatic;

        //Acción al colisionar
        public Action<Collider> OnCollision { get; set; }

        //Vertices
        private Vector2[] vertices;
        //Vertices transformados
        private Vector2[] transformedVertices;

        public BodyType BodyType { get { return bodyType; } }

        public Vector2 LinearVelocity { get { return linearVelocity; } set { linearVelocity = value; } }

        public float Mass { get { return mass; } }

        public float Restitution { get { return restitution; } }

        public float InvMass { get { return invMass; } }

        public bool IsStatic { get { return isStatic; } }

        internal Vector2[] Vertices { get { return vertices; } }
        internal Vector2[] TransformedVertices { get { return transformedVertices; } }

        public Vector2 Center { get { return entity.GetComponent<Transform>().Center; } }
        
        public Collider(BodyType bodyType, float mass, float restitution, bool isStatic)
        {
            this.bodyType = bodyType;
            this.mass = mass;
            this.restitution = restitution;
            this.isStatic = isStatic;

            invMass = isStatic ? 0 : 1 / mass;

            ColliderSystem.Register(this);
        }

        public Collider(BodyType bodyType, float mass, float restitution, bool isStatic, Action<Collider> onCollision)
        {
            this.bodyType = bodyType;
            this.mass = mass;
            this.restitution = restitution;
            this.isStatic = isStatic;
            this.OnCollision = onCollision;

            invMass = isStatic ? 0 : 1 / mass;

            ColliderSystem.Register(this);
        }

        public override void Init()
        {
            if (bodyType == BodyType.Polygon)
                generatePolygon();
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void ApplyForce(Vector2 amount)
        {
            linearForce = amount;
        }

        public void Move(Vector2 amount)
        {
            entity.GetComponent<Transform>().Position += amount;
        }

        public void MoveTo(Vector2 position)
        {
            entity.GetComponent<Transform>().Position = position;
        }

        internal void Step(GameTime gameTime)
        {
            if (isStatic)
                return;

            float friction = 0.99f;
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 acceleration = linearForce * invMass;
            linearVelocity += acceleration * time;
            linearVelocity *= friction;

            Move(linearVelocity * time);

            if (Math.Abs(linearVelocity.X) < 1 && Math.Abs(linearVelocity.Y) < 1)
                linearVelocity = Vector2.Zero;

            linearForce = Vector2.Zero;
        }

        internal void generatePolygon()
        {
            Transform transform = entity.GetComponent<Transform>();
            float left = -transform.Size.X * 0.5f;
            float right = left + transform.Size.X;
            float bottom = -transform.Size.Y * 0.5f;
            float top = bottom + transform.Size.Y;

            vertices = new Vector2[4]
            {
                new Vector2(left, top),
                new Vector2(right, top),
                new Vector2(right, bottom),
                new Vector2(left, bottom)
            };
            transformedVertices = new Vector2[4];

            TransformVertices();
        }

        internal void TransformVertices()
        {
            Vector2 center = entity.GetComponent<Transform>().Center;
            Matrix matrix = Matrix.CreateTranslation(new Vector3(center.X, center.Y, 0));

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 vertex = vertices[i];
                transformedVertices[i] = Vector2.Transform(vertex, matrix);
            }
        }
    }
}
