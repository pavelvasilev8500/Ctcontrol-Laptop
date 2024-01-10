using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ResourcesLibrary.Resources.Wallpapers.Classes;
using System;
using System.Windows;

namespace ModuleSettings.ViewModels
{
    class WallpaperSettingsViewModel : BindableBase, IRegionMemberLifetime, IConfirmNavigationRequest
    {
        private readonly IRegionManager _regionManager;
        private int SelectedItem { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand NextCommand { get; set; }
        public DelegateCommand ApplyCommand { get; set; }
        public DelegateCommand PreviousCommand { get; set; }
        public bool KeepAlive
        {
            get { return false; }
        }
        public WallpaperSettingsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            for (int i = 0; i < Wallpapers.m_Wallpapers.Count; i++)
                if (Wallpapers.m_Wallpapers[i] == Properties.Settings.Default.DefaultWallpaper)
                    SelectedItem = i;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            NextCommand = new DelegateCommand(Next);
            ApplyCommand = new DelegateCommand(Apply);
            PreviousCommand = new DelegateCommand(Previous);
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
        private void Next()
        {
            SelectedItem++;
            if (SelectedItem >= Wallpapers.m_Wallpapers.Count)
                SelectedItem = 0;
            Wallpapers.Wallpaper = Wallpapers.m_Wallpapers[SelectedItem];
        }
        private void Apply()
        {
            Properties.Settings.Default.DefaultWallpaper = Wallpapers.Wallpaper;
            Properties.Settings.Default.Save();
        }
        private void Previous()
        {
            if (SelectedItem <= 0)
                SelectedItem = Wallpapers.m_Wallpapers.Count;
            SelectedItem--;
            Wallpapers.Wallpaper = Wallpapers.m_Wallpapers[SelectedItem];
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            Apply();
            continuationCallback(true);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
