using ModuleWelcome.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleSettings
{
    public class ModuleWelcomeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleWelcomeModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["ContentRegion"];
            //var mainView = containerProvider.Resolve<SelectLanguageView>();
            //region.Add(mainView);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SelectLanguageView>();
        }
    }
}
