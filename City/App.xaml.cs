using System.Windows;
using City.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using ModuleMain;
using ModuleSettings;
using ModuleMobile;
using ResourcesLibrary.Resources.Wallpapers.Classes;
using ResourcesLibrary.Resources.Languages.Classes;

namespace City
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            Wallpapers.Wallpaper = ModuleSettings.Properties.Settings.Default.DefaultWallpaper;
            Languages.Language = ModuleSettings.Properties.Settings.Default.DefaultLanguage;
            return Container.Resolve<ShellWindow>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleMainModule>();
            moduleCatalog.AddModule<ModuleSettingsModule>();
            moduleCatalog.AddModule<ModuleMobileModule>();
        }
    }
}
