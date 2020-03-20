using Models;
using Presenters;
using Services;
using UseCases;
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
            
            //UseCases
            Container.Bind<PlayerUseCase>().AsSingle();
            
            //Services
            Container.Bind<CreateDependentObjectService>().AsSingle();
        }
    }
}