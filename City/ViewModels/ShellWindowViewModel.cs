using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ResourcesLibrary.Resources.Languages.Classes;
using ResourcesLibrary.Resources.Wallpapers.Classes;
using System.Diagnostics;
using System.Security.Principal;
using System.Reflection;

namespace City.ViewModels
{
    class ShellWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private ObservableCollection<object> _views = new ObservableCollection<object>();
        private string _newObject;
        public DelegateCommand CloseAppCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; set; }
        public ObservableCollection<object> Views
        {
            get { return _views; }
            set { SetProperty(ref _views, value); }
        }
        private string NewObject
        {
            get { return _newObject; }
            set { _newObject = value; }
        }
        public ShellWindowViewModel(IRegionManager regionManager)
        {
            Wallpapers.Wallpaper = ModuleSettings.Properties.Settings.Default.DefaultWallpaper;
            Languages.Language = ModuleSettings.Properties.Settings.Default.DefaultLanguage;
            CloseAppCommand = new DelegateCommand(CloseApp);
            NavigateCommand = new DelegateCommand<string>(Navigate);
            _regionManager = regionManager;
            _regionManager.Regions.CollectionChanged += Regions_CollectionChanged;
            RunAsAdministartor();
        }
        private void Regions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var region = (IRegion)e.NewItems[0];
                region.Views.CollectionChanged += Views_CollectionChanged;
            }
        }
        private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Views.Add(e.NewItems[0].GetType().Name);
                NewObject = e.NewItems[0].GetType().Name;
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Views.Remove(e.OldItems[0].GetType().Name);
            }
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                if (NewObject != navigatePath)
                    _regionManager.RequestNavigate("ContentRegion", navigatePath);
            }
        }
        private bool IsRunAsAdministrator()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }
        private void RunAsAdministartor()
        {
            if (!IsRunAsAdministrator())
            {
                var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);
                processInfo.UseShellExecute = true;
                processInfo.Verb = "runas";
                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception)
                {
                }
                Environment.Exit(0);
            }
        }
        private void CloseApp()
        {
            Environment.Exit(0);
        }
    }
}
