using ModuleMobile.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleMobile
{
    public class ModuleMobileModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleMobileModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["ContentRegion"];
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MobileView>();
        }
    }
}