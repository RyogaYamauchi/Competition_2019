namespace Models
{
    public class AppState
    {
        public AppState()
        {
            InitializePlayer();
            InitializeRoot();
            InitializeItems();
        }


        public RootModel RootModel { get; set; }

        public PlayerModel PlayerModel { get; private set; }
        public ItemsModel ItemsModel { get; private set; }

        private void InitializePlayer()
        {
            PlayerModel = new PlayerModel(10, 1);
        }
        
        private void InitializeRoot()
        {
            RootModel = new RootModel();
        }

        private void InitializeItems()
        {
            ItemsModel = new ItemsModel();
        }
    }
}