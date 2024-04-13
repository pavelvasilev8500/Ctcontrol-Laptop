using Prism.Mvvm;
using ClassesLibrary.SystemInfo;
using ClassesLibrary.SystemControls;
using Prism.Regions;
using Prism.Commands;
using System;
using System.Threading;
using Prism.Events;
using ClassesLibrary.Classes;
using System.Windows;

namespace ModuleMain.ViewModels
{
    public class ControlViewModel : BindableBase, IRegionMemberLifetime, IConfirmNavigationRequest
    {
        private string _worktimeday;
        private string _worktimehour;
        private string _worktimeminut;
        private string _worktimesecond;
        private string _batary;
        private string _cputemperature;
        private string _gputemperature;
        private readonly IRegionManager _regionManager;
        IEventAggregator _ea;
        private Visibility _batteryVisibility;
        public string WorkTimeDay
        {
            get { return _worktimeday; }
            set { SetProperty(ref _worktimeday, value); }
        }
        public string WorkTimeHour
        {
            get { return _worktimehour; }
            set { SetProperty(ref _worktimehour, value); }
        }
        public string WorkTimeMinut
        {
            get { return _worktimeminut; }
            set { SetProperty(ref _worktimeminut, value); }
        }
        public string WorkTimeSecond
        {
            get { return _worktimesecond; }
            set { SetProperty(ref _worktimesecond, value); }
        }
        public string Batary
        {
            get { return _batary; }
            set { SetProperty(ref _batary, value); }
        }
        public string CPUtemperature
        {
            get { return _cputemperature; }
            set { SetProperty(ref _cputemperature, value); }
        }
        public string GPUtemperature
        {
            get { return _gputemperature; }
            set { SetProperty(ref _gputemperature, value); }
        }
        public Visibility BatteryVisibility
        {
            get
            {
                return _batteryVisibility;
            }
            set
            {
                SetProperty(ref _batteryVisibility, value);
            }
        }
        private bool IsLaptop { get; set; }
        public bool KeepAlive
        {
            get { return false; }
        }
        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand ShutdonCommand { get; private set; }
        public DelegateCommand RestartCommand { get; private set; }
        public DelegateCommand SleepCommand { get; private set; }
        CancellationTokenSource cts = new CancellationTokenSource();
        public ControlViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            ThreadController();
            _regionManager = regionManager;
            _ea = ea;
            _ea.GetEvent<SendBoolEvent>().Subscribe(BoolMessageRecived);
            NavigateCommand = new DelegateCommand<string>(Navigate);
            ShutdonCommand = new DelegateCommand(Shutdown);
            RestartCommand = new DelegateCommand(Restart);
            SleepCommand = new DelegateCommand(Sleep);
        }

        private void BoolMessageRecived(bool islaptop)
        {
            IsLaptop = islaptop;
            if (IsLaptop)
                _batteryVisibility = Visibility.Visible;
            else
                _batteryVisibility = Visibility.Hidden;
        }
        private void ThreadController()
        {
            Thread data = new Thread(() =>
            {
                UpdateData();
            });
            Thread temperature = new Thread(() =>
            {
                UpdateTemerature();
            });
            data.Name = "UpdateDataThreadFromControl";
            temperature.Name = "UpdateTemperatureThreadFromControl";
            data.Start();
            UpdateSecond();
            temperature.Start();
        }
        private void UpdateData()
        {
            do
            {
                WorkTimeDay = SystemInfo.GetPcWorkTimeDay();
                WorkTimeHour = SystemInfo.GetPcWorkTimeHour();
                WorkTimeMinut = SystemInfo.GetPcWorkTimeMinut();
                Batary = SystemInfo.GetNotebookBatary();
                Thread.Sleep(1000);
            }
            while (!cts.IsCancellationRequested);
        }
        private void UpdateSecond()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Enabled = true;
            timer.Tick += UpdateSecondTimer;
            timer.Interval = 1000;
            timer.Start();
        }
        private void UpdateSecondTimer(object sender, EventArgs e)
        {
            WorkTimeSecond = SystemInfo.GetPcWorkTimeSecond();
        }
        private void UpdateTemerature()
        {
            do
            {
                CPUtemperature = SystemInfo.GetTemperature().Item1;
                GPUtemperature = SystemInfo.GetTemperature().Item2;
            }
            while (!cts.IsCancellationRequested);
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
        private void Shutdown()
        {
            SystemControl.halt(false, false);
        }
        private void Restart()
        {
            SystemControl.halt(true, false);
        }
        private void Sleep()
        {
            SystemControl.Sleep(false, false, false);
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            cts.Cancel();
            continuationCallback(true);
        }

        //Put.PutData(statusuri, id, CreateJson.CreateDataJson(new ClassesLibrary.DataModels.StatusDataModel(), id, status));
        #region VMFunctions
        public void OnNavigatedTo(NavigationContext navigationContext){}
        public bool IsNavigationTarget(NavigationContext navigationContext){ return true;}
        public void OnNavigatedFrom(NavigationContext navigationContext){}
        #endregion
    }
}
