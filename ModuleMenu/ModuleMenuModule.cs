using ModuleMenu.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleMenu
{
    public class ModuleMenuModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleMenuModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MenuView>();
        }
    }
}
