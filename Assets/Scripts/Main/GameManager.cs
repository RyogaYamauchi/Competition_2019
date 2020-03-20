using Models;
using Services;
using UnityEngine;
using Views;
using Zenject;

namespace Main
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _root;
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
            var view = _createDependentObjectService.CreateDependentObjectAsync<PlayerView>();
        }
    }
}