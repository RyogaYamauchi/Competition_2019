using Models;
using Services;
using UnityEngine;
using Views;
using Zenject;
using Framework;

namespace Main
{
    public class GameManager : ViewBase
    {
        [SerializeField] private GameObject _root;

        [SerializeField] private GameObject _UICanvas;
        private CreateDependentObjectService _createDependentObjectService;
        private AppState _appState;

        [Inject]
        public void Construct(CreateDependentObjectService createDependentObjectService, AppState appState)
        {
            _createDependentObjectService = createDependentObjectService;
            _appState = appState;
        }

        private void Awake()
        {
        }

        private async void Start()
        {
            _appState.RootModel.Root = _root;
            _appState.RootModel.UIRoot = _UICanvas;
            var view = await _createDependentObjectService.CreateDependentObjectAsync<PlayerView>();
            var uiItems = await _createDependentObjectService.CreateDependentUIAsync<UIItemsView>();
        }
    }
}