using Scripts.Presenters;

namespace Scripts.Models
{
    public class GameModel
    {
        public static GameModel Instance { get; } = new GameModel();
        public EnemiesModel EnemiesModel { get; }

        public GameModel()
        {
            EnemiesModel = new EnemiesModel();
        }
    }
}