
namespace MVC
{
    public interface ILevels
    {
        int Level { get; }
        void LevelUp();
        void Restore();
    }

}
