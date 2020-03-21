using Models;
using Presenters;
using Services;
using UseCases;
using Views;
using Zenject;

namespace Installers
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            
            // Model
            Container.Bind<AppState>().AsSingle();
            
            
            //Presenters
            Container.Bind<PlayerPresenter>().AsSingle();
            Container.Bind<UIItemsPresenter>().AsSingle();
            
            //UseCases
            Container.Bind<PlayerUseCase>().AsSingle();
            Container.Bind<UIItemsUseCase>().AsSingle();
            
            //Services
            Container.Bind<CreateDependentObjectService>().AsSingle();
            Container.Bind<InputService>().AsSingle();
        }
    }
}