namespace LocutaEngine.Ecs
{
    public class Component
    {
        protected Entity entity;

        public Entity Entity { get { return entity; } set { entity = value; } }

        public virtual void Init()
        { 

        }
    }
}
