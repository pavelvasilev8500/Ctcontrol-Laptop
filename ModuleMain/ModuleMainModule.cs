using ModuleMain.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleMain
{
    public class ModuleMainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleMainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["ContentRegion"];
            var mainView = containerProvider.Resolve<MainView>();
            region.Add(mainView);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();
            containerRegistry.RegisterForNavigation<ControlView>();
        }
    }
}
