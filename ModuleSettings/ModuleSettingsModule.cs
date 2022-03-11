using ModuleSettings.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleSettings
{
    public class ModuleSettingsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleSettingsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["ContentRegion"];
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainSettingsView>();
            containerRegistry.RegisterForNavigation<LanguageSettingsView>();
            containerRegistry.RegisterForNavigation<WallpaperSettingsView>();
            containerRegistry.RegisterForNavigation<CommonSettingsView>();
        }
    }
}
