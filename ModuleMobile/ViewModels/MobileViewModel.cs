using ClassesLibrary.Classes;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Globalization;
using System.Windows;

namespace ModuleMobile.ViewModels
{
    public class MobileViewModel : BindableBase, IRegionMemberLifetime, IConfirmNavigationRequest
    {

        private readonly IRegionManager _regionManager;
        IEventAggregator _ea;
        private string _id;
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
        private Visibility _qrvisibility = Visibility.Visible;
        public Visibility QRVisibility
        {
            get { return _qrvisibility; }
            set { SetProperty(ref _qrvisibility, value); }
        }
        private Visibility _accessecodevisibility = Visibility.Hidden;
        public Visibility AccesseCodeVisibility
        {
            get { return _accessecodevisibility; }
            set { SetProperty(ref _accessecodevisibility, value); }
        }
        public bool KeepAlive
        {
            get { return false; }
        }

        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand SwitchQrCommand { get; set; }
        ResourceDictionary Russian = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Languages/lang.ru-RU.xaml", UriKind.Relative)) as ResourceDictionary;
        ResourceDictionary English = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Languages/lang.xaml", UriKind.Relative)) as ResourceDictionary;

        public MobileViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            _ea = ea;
            _ea.GetEvent<SendIdEvent>().Subscribe(GetId);
            NavigateCommand = new DelegateCommand<string>(Navigate);
            SwitchQrCommand = new DelegateCommand(SwitchQr);
            switch (CultureInfo.CurrentUICulture.Name)
            {
                case "ru-RU":
                    Text = Russian["m_AccessCode"].ToString();
                    break;
                default:
                    Text = English["m_AccessCode"].ToString();
                    break;
            }
        }

        private void SwitchQr()
        {

            if(QRVisibility == Visibility.Visible)
            {
                QRVisibility = Visibility.Hidden;
                AccesseCodeVisibility = Visibility.Visible;
                switch (CultureInfo.CurrentUICulture.Name)
                {
                    case "ru-RU":
                        Text = Russian["m_QRCode"].ToString();
                        break;
                    default:
                        Text = English["m_QRCode"].ToString();
                        break;
                }
            }
            else
            {
                QRVisibility = Visibility.Visible;
                AccesseCodeVisibility = Visibility.Hidden;
                switch (CultureInfo.CurrentUICulture.Name)
                {
                    case "ru-RU":
                        Text = Russian["m_AccessCode"].ToString();
                        break;
                    default:
                        Text = English["m_AccessCode"].ToString();
                        break;
                }
            }
        }

        private void GetId(string id)
        {
            Id = id;
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
