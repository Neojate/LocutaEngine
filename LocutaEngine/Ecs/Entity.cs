using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LocutaEngine.Ecs
{
    public class Entity
    {
        //Listado de los componentes
        private List<Component> components;

        public List<Component> Components { get { return components; } set { components = value; } }

        public Entity()
        {
            components = new List<Component>();
        }

        public void AddComponent(Component component)
        {
            component.Entity = this;
            components.Add(component);
            component.Init();
        }

        public void DestroyComponent(Component component)
        {
            components.Remove(component);
        }

        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
                if (components[i].GetType().Equals(typeof(T)))
                    return (T)components[i];

            return null;
        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
