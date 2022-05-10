using LocutaEngine.Figures;

namespace LocutaEngine.Physics
{
    public interface ICollidable
    {
        RigitBody RigitBody { get; set; }
    }
}
