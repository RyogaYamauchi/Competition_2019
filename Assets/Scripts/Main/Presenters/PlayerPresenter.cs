using UseCases;
using Views;

namespace Presenters
{
    public class PlayerPresenter
    {

        private readonly PlayerUseCase _useCase;
        public PlayerPresenter(PlayerUseCase usecase)
        {
            _useCase = usecase;
        }

        public PlayerView View { get; set; }

        public void Start()
        {
            var viewModel = _useCase.GetViewModel();
            View.Show(viewModel);
        }
    }
}