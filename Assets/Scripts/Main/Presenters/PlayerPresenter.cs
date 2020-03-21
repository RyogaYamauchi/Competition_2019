using Main.MasterDatas;
using UniRx.Async;
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
            ApplyInput();
        }

        private async void ApplyInput()
        {
            while (true)
            {
                await UniTask.Yield();
                var inputType = _useCase.GetInputType();

                if (inputType != InputType.none)
                {
                    Input(inputType);
                }
            }
        }

        private void Input(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.a:
                    View.Move(-1.0f);
                    break;
                case InputType.d:
                    View.Move(1.0f);
                    break;
                case InputType.w:
                    View.Jump();
                    break;
            }
        }

        public void AddItem(int itemId)
        {
            _useCase.AddItem(itemId);
        }
    }
}