using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace ModuleMobile.ViewModels
{
    public class MobileViewModel : BindableBase, IRegionMemberLifetime, IConfirmNavigationRequest
    {

        private readonly IRegionManager _regionManager;

        public bool KeepAlive
        {
            get { return false; }
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        public MobileViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
