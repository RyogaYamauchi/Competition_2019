using Main.MasterDatas;
using Models;
using Services;
using ViewModels;

namespace UseCases
{
    public class PlayerUseCase
    {
        private readonly AppState _appState;

        private readonly InputService _inputService;

        public PlayerUseCase(AppState appState, InputService inputService)
        {
            _appState = appState;
            _inputService = inputService;
        }

        public PlayerViewModel GetViewModel()
        {
            var model = _appState.PlayerModel;
            var viewModel = new PlayerViewModel(model);
            return viewModel;
        }

        public InputType GetInputType()
        {
            return _inputService.InputType;
        }
    }
}