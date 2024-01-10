using ClassesLibrary.Classes;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using ResourcesLibrary.Resources.Languages.Classes;
using System.Collections.Generic;
using System.Globalization;

namespace ModuleWelcome.ViewModels
{
    public class SelectLanguageViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        IEventAggregator _ea;
        public List<string> s_Languages { get; set; } = new List<string>();
        public List<CultureInfo> c_Languages { get; set; } = new List<CultureInfo>();
        private string selectedItem;
        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                SetProperty(ref selectedItem, value);
                foreach (var l in c_Languages)
                {
                    if (selectedItem == l.DisplayName)
                    {
                        Languages.Language = l;
                    }
                }
            }
        }
        public bool KeepAlive
        {
            get { return false; }
        }
        public DelegateCommand<string> NavigateCommand { get; set; }
        public SelectLanguageViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            _ea = ea;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            if (s_Languages.Count != 0)
            {
                s_Languages.Clear();
                c_Languages.Clear();
            }
            foreach (var language in Languages.All_Languages)
            {
                s_Languages.Add(language.DisplayName);
                c_Languages.Add(language);
            }
            SelectedItem = ModuleSettings.Properties.Settings.Default.DefaultLanguage.DisplayName;
            Languages.LanguageChanged += Languages_LanguageChanged;
        }
        private void Languages_LanguageChanged()
        {
            ModuleSettings.Properties.Settings.Default.DefaultLanguage = Languages.Language;
            ModuleSettings.Properties.Settings.Default.Save();
            _ea.GetEvent<SendLanguageEvent>().Publish(Languages.Language);
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
    }
}
