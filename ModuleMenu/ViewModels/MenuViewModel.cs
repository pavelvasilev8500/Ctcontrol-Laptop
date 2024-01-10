using ClassesLibrary.Classes;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModuleMenu.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        IEventAggregator _ea;
        private string _newObject;
        private string NewObject
        {
            get { return _newObject; }
            set { _newObject = value; }
        }
        private Visibility backButtonVisible = Visibility.Hidden;
        public Visibility BackButtonVisible
        {
            get { return backButtonVisible; }
            set { SetProperty(ref backButtonVisible, value); }
        }
        private Visibility mainButtonVisible = Visibility.Visible;
        public Visibility MainButtonVisible
        {
            get { return mainButtonVisible; }
            set { SetProperty(ref mainButtonVisible, value); }
        }
        private string commandNavigationParameter;
        public string CommandNavigationParameter
        {
            get { return commandNavigationParameter; }
            set { SetProperty(ref commandNavigationParameter, value); }
        }
        private ObservableCollection<object> _views = new ObservableCollection<object>();
        public ObservableCollection<object> Views
        {
            get { return _views; }
            set { SetProperty(ref _views, value); }
        }
        public DelegateCommand CloseAppCommand { get; private set; }
        public DelegateCommand<string> NavigateCommand { get; set; }

        public MenuViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _ea = ea;
            _regionManager = regionManager;
            CloseAppCommand = new DelegateCommand(CloseApp);
            NavigateCommand = new DelegateCommand<string>(Navigate);
            _regionManager.Regions.CollectionChanged += Regions_CollectionChanged;
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
                MessageBox.Show(NewObject);
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
            if (navigatePath == "MainView")
            {
                MainButtonVisible = Visibility.Visible;
                BackButtonVisible = Visibility.Hidden;
            }
            if (navigatePath == "ControlView")
            {
                MainButtonVisible = Visibility.Visible;
                BackButtonVisible = Visibility.Hidden;
            }
            if (navigatePath == "MobileView")
            {
                MainButtonVisible = Visibility.Visible;
                BackButtonVisible = Visibility.Hidden;
            }
            if (navigatePath == "MainSettingsView")
            {
                MainButtonVisible = Visibility.Hidden;
                BackButtonVisible = Visibility.Visible;
                CommandNavigationParameter = "MainView";
            }
            if (navigatePath == "CommonSettingsView")
            {
                MainButtonVisible = Visibility.Visible;
                BackButtonVisible = Visibility.Visible;
                CommandNavigationParameter = "MainSettingsView";
            }

        }

        private void CloseApp()
        {
            _ea.GetEvent<SendBoolEvent>().Publish(true);
        }
    }
}
