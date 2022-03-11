using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ResourcesLibrary.Resources.Wallpapers.Classes;

namespace ModuleSettings.ViewModels
{
    class WallpaperSettingsViewModel : BindableBase, IRegionMemberLifetime
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
            if (SelectedItem < Wallpapers.All_Wallpapers.Count)
            {
                Wallpapers.Wallpaper = Wallpapers.All_Wallpapers[SelectedItem];
                SelectedItem++;
            }
            else if (SelectedItem >= Wallpapers.All_Wallpapers.Count)
            {
                SelectedItem = 0;
                Wallpapers.Wallpaper = Wallpapers.All_Wallpapers[SelectedItem];
                SelectedItem++;
            }
        }
        private void Apply()
        {
            Properties.Settings.Default.DefaultWallpaper = Wallpapers.Wallpaper;
            Properties.Settings.Default.Save();
        }
        private void Previous()
        {
            if (SelectedItem <= 0)
            {
                SelectedItem = Wallpapers.All_Wallpapers.Count;
                SelectedItem--;
                Wallpapers.Wallpaper = Wallpapers.All_Wallpapers[SelectedItem];
            }
            else if (SelectedItem <= Wallpapers.All_Wallpapers.Count)
            {
                SelectedItem--;
                Wallpapers.Wallpaper = Wallpapers.All_Wallpapers[SelectedItem];
            }
        }
    }
}
