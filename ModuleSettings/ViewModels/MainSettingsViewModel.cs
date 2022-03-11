using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ModuleSettings.ViewModels
{
    public class MainSettingsViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; set; }
        public bool KeepAlive
        {
            get { return false; }
        }
        public MainSettingsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
    }
}
