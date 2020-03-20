namespace Models
{
    public class AppState
    {
        public AppState()
        {
            InitializePlayer();
            InitializeRoot();
        }

        private void InitializeRoot()
        {
            RootModel = new RootModel();
        }

        public RootModel RootModel { get; set; }

        public PlayerModel PlayerModel { get; private set; }

        private void InitializePlayer()
        {
            PlayerModel = new PlayerModel(10, 1);
        }
    }
}