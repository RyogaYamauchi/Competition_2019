namespace Scripts.Models
{
    public class GameModel
    {
        public static GameModel Instance { get; } = new GameModel();
        public EnemiesModel EnemiesModel { get; }
        public IPlayerModel PlayerModel { get; }
        public UIModel UiModel { get; }
        public GameProgressModel GameProgressModel { get; }

        public GameModel()
        {
            EnemiesModel = new EnemiesModel();
            PlayerModel = new PlayerModel();
            UiModel = new UIModel();
            GameProgressModel = new GameProgressModel();
        }
    }
}