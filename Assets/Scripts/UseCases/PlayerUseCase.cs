using Models;
using ViewModels;

namespace UseCases
{
    public class PlayerUseCase
    {
        private readonly AppState _appState;
        public PlayerUseCase(AppState appState)
        {
            _appState = appState;
        }
        public PlayerViewModel GetViewModel()
        {
            var model = _appState.PlayerModel;
            var viewModel = new PlayerViewModel(model);
            return viewModel;
        }
    }
}