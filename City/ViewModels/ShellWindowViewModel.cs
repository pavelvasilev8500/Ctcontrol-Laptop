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
using City.Models;
using Prism.Events;
using ClassesLibrary.Client;
using ClassesLibrary.Classes;
using ClassesLibrary.ServerWork;
using System.Threading;
using System.Windows;
using System.Globalization;
using System.Windows.Forms;
using ClassesLibrary.SystemInfo;
using WarningDialog.Views;

namespace City.ViewModels
{
    class ShellWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        IEventAggregator _ea;
        private ObservableCollection<object> _views = new ObservableCollection<object>();
        private static Mutex InstanceCheckMutex;
        private string _newObject;
        private static string ClientId { get; set; }
        private static bool IsLaptop { get; set; }
        private static bool Notification { get; set; } = false;
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
        CancellationTokenSource cts = new CancellationTokenSource();
        public ShellWindowViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            if (!IsProgramStart())
            {
                ResourceDictionary Russian = System.Windows.Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Languages/lang.ru-RU.xaml", UriKind.Relative)) as ResourceDictionary;
                ResourceDictionary English = System.Windows.Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Languages/lang.xaml", UriKind.Relative)) as ResourceDictionary;
                switch (CultureInfo.InstalledUICulture.Name)
                {
                    case "ru-RU":
                        System.Windows.MessageBox.Show(Russian["m_Anothercopy"].ToString());
                        break;
                    default:
                        System.Windows.MessageBox.Show(English["m_Anothercopy"].ToString());
                        break;
                }
                Process.GetCurrentProcess().Kill();
            }
            _ea = ea;
            Wallpapers.Wallpaper = ModuleSettings.Properties.Settings.Default.DefaultWallpaper;
            Languages.Language = ModuleSettings.Properties.Settings.Default.DefaultLanguage;
            CloseAppCommand = new DelegateCommand(CloseApp);
            NavigateCommand = new DelegateCommand<string>(Navigate);
            _regionManager = regionManager;
            _regionManager.Regions.CollectionChanged += Regions_CollectionChanged;
            RunAsAdministartor();
            BatteryCheck();
        }
        private void BatteryCheck()
        {
            Thread batteryCheck = new Thread(() =>
            {
                do
                {
                    switch (SystemInformation.PowerStatus.BatteryChargeStatus)
                    {
                        case BatteryChargeStatus.Low:
                            if (Notification == false)
                            {
                                if (SystemInfo.GetNotebookBataryFloat() <= 10)
                                {
                                    var mw = new MainWindow();
                                    mw.ShowDialog();
                                    Notification = true;
                                }
                            }
                            break;
                        default:
                            Notification = false;
                            break;
                    }
                    Thread.Sleep(1000);
                }
                while (!cts.IsCancellationRequested);
            });
            batteryCheck.Name = "BatteryCheckThread";
            batteryCheck.SetApartmentState(ApartmentState.STA);
            if (IsLaptop)
                batteryCheck.Start();
        }
        private bool IsProgramStart()
        {
            var currentProc = Process.GetCurrentProcess();
            string processName = currentProc.ProcessName;
            bool isNew;
            InstanceCheckMutex = new Mutex(true, processName, out isNew);
            return isNew;
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
            if(navigatePath == "ControlView")
            {
                _ea.GetEvent<SendIdEvent>().Publish(Id());
                _ea.GetEvent<SendSystemUriEvent>().Publish(Properties.Settings.Default.SystemUri);
                _ea.GetEvent<SendStatusUriEvent>().Publish(Properties.Settings.Default.StatusUri);
                _ea.GetEvent<SendBoolEvent>().Publish(IsPcLaptop());
            }
            if(navigatePath == "MobileView")
            {
                _ea.GetEvent<SendIdEvent>().Publish(Id());
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
        private static string Id()
        {
            if (Properties.Settings.Default.FirstStart)
            {
                Properties.Settings.Default.ClientId = GenerateClientId.Id();
                ClientId = Properties.Settings.Default.ClientId;
                Properties.Settings.Default.FirstStart = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                ClientId = Properties.Settings.Default.ClientId;
            }
            return ClientId;
        }
        private static bool IsPcLaptop()
        {
            if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.NoSystemBattery || SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Unknown)
            {
                IsLaptop = false;
            }
            else
            {
                IsLaptop = true;
            }
            return IsLaptop;
        }
        ShellModel shellModel = new ShellModel(Id(), IsPcLaptop());

        private void CloseApp()
        {
            Put.PutData(Properties.Settings.Default.StatusUri, ClientId, CreateJson.CreateDataJson(new ClassesLibrary.DataModels.StatusDataModel(), ClientId, false));
            Environment.Exit(0);
        }
    }
}
