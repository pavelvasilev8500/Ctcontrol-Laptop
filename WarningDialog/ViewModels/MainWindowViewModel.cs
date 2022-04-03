using ClassesLibrary.Classes;
using Prism.Events;
using Prism.Mvvm;
using ResourcesLibrary.Resources.Languages.Classes;
using System.Globalization;

namespace WarningDialog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IEventAggregator _ea;

        public MainWindowViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<SendLanguageEvent>().Subscribe(MessageReceived);
        }

        private void MessageReceived(CultureInfo language)
        {
            Languages.Language = language;
        }
    }
}
