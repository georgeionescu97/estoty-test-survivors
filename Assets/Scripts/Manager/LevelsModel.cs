
namespace MVC
{
    public class LevelsModel : ILevels
    {
        public int Level { get; private set; }

        public void LevelUp()
        {
            Level++;
        }

        public void Restore()
        {
            Level = 0;
        }
    }
}
