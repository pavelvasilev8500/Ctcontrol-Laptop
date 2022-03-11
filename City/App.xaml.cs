using System.Windows;
using City.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using ModuleMain;
using ModuleSettings;

namespace City
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleMainModule>();
            moduleCatalog.AddModule<ModuleSettingsModule>();
        }
    }
}
