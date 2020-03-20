using System.Reflection;
using Framework;
using Models;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace Services
{
    public class CreateDependentObjectService
    {
        private readonly DiContainer _container;
        private readonly AppState _appState;

        public CreateDependentObjectService(DiContainer container, AppState appState)
        {
            _container = container;
            _appState = appState;
        }

        public async UniTask<T> CreateDependentObjectAsync<T>(Transform transform = null) where T : ViewBase
        {
            if (transform == null)
            {
                transform = _appState.RootModel.Root.transform;
            }

            var path = typeof(T).GetCustomAttribute<PrefabPathAttribute>().Path;
            var obj = await Resources.LoadAsync<T>(path);
            return _container.InstantiatePrefabForComponent<T>(obj, transform);
        }
    }
}