using Models;
using UseCases;
using ViewModels;
using Views;

namespace Presenters
{
    public class UIItemsPresenter
    {
        private readonly UIItemsUseCase _useCase;
        private readonly AppState _appState;

        public UIItemsPresenter(UIItemsUseCase useCase, AppState appState)
        {
            _useCase = useCase;
            _appState = appState;
        }

        public UIItemsView View { get; set; }

        public UIItemsViewModel GetUIItemsViewModel()
        {
            var model = _appState.ItemsModel;
            var viewModel = new UIItemsViewModel(model.Items);

            return viewModel;
        }
    }
}