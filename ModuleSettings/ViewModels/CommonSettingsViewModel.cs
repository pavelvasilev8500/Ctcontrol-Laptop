using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using Prism.Events;
using ClassesLibrary.Classes;
using ClassesLibrary.SystemControls;

namespace ModuleSettings.ViewModels
{
    public class CommonSettingsViewModel : BindableBase, IRegionMemberLifetime
    {
        IEventAggregator _ea;
        private readonly IRegionManager _regionManager;
        public bool KeepAlive
        {
            get { return false; }
        }
        private bool _autostartSwitcher;
        private bool _secondSwitcher;
        private Visibility SecondVisibale { get; set; }
        public bool AutostartSwitcher
        {
            get
            {
                return _autostartSwitcher;
            }
            set
            {
                SetProperty(ref _autostartSwitcher, value);
                if (value == true)
                {
                    Autorun.SetAutorunValue(value);
                    Properties.Settings.Default.DefaultAutorun = AutostartSwitcher;
                    Properties.Settings.Default.Save();
                }
                else if (value == false)
                {
                    Autorun.SetAutorunValue(value);
                    Properties.Settings.Default.DefaultAutorun = AutostartSwitcher;
                    Properties.Settings.Default.Save();
                }
            }
        }
        public bool SecondSwitcher
        {
            get
            {
                return _secondSwitcher;
            }
            set
            {
                SetProperty(ref _secondSwitcher, value);
                if (value == true)
                {
                    SecondVisibale = Visibility.Visible;
                    _ea.GetEvent<SendEvent>().Publish(SecondVisibale);
                    Properties.Settings.Default.DefaultSecondVisible = SecondSwitcher;
                    Properties.Settings.Default.Save();
                }
                else if (value == false)
                {
                    SecondVisibale = Visibility.Hidden;
                    _ea.GetEvent<SendEvent>().Publish(SecondVisibale);
                    Properties.Settings.Default.DefaultSecondVisible = SecondSwitcher;
                    Properties.Settings.Default.Save();
                }
            }
        }
        public DelegateCommand<string> NavigateCommand { get; set; }
        public CommonSettingsViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _ea = ea;
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            AutostartSwitcher = Properties.Settings.Default.DefaultAutorun;
            SecondSwitcher = Properties.Settings.Default.DefaultSecondVisible;
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
    }
}
