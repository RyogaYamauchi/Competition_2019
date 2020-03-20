using Framework;

namespace Repository
{
    public class GameRepository : RepositoryBase
    {
        public static readonly GameRepository Instance = new GameRepository();

        public ITalkRepository TalkRepository { get; private set; }
        public IAnimationRepository AnimationRepository { get; private set; }

        public GameRepository()
        {
            TalkRepository = new TalkRepository();
            AnimationRepository = new AnimationRepository();
        }

    }
}
