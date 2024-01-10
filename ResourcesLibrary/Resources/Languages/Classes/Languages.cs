using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace ResourcesLibrary.Resources.Languages.Classes
{
    public static class Languages
    {
        public delegate void EventHandler();
        public static event EventHandler LanguageChanged;
        private static List<CultureInfo> m_Languages = new List<CultureInfo>()
        {
            new CultureInfo("en-US"),
            new CultureInfo("ru-RU")
        };
        public static List<CultureInfo> All_Languages
        {
            get
            {
                return m_Languages;
            }
        }

        //public static List<ResourceDictionary> r_Languages = new List<ResourceDictionary>()
        //{
        //    Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Languages/lang.ru-RU.xaml", UriKind.Relative)) as ResourceDictionary,
        //    Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Languages/lang.xaml", UriKind.Relative)) as ResourceDictionary
        //};

        
        private static ResourceDictionary Russian = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Languages/lang.ru-RU.xaml", UriKind.Relative)) as ResourceDictionary;
        private static ResourceDictionary English = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Languages/lang.xaml", UriKind.Relative)) as ResourceDictionary;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;
                var oldlang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;
                switch (value.Name)
                {
                    case "ru-RU":
                        Application.Current.Resources.MergedDictionaries.Add(Russian);
                        Application.Current.Resources.MergedDictionaries.Remove(English);
                        break;
                    default:
                        Application.Current.Resources.MergedDictionaries.Add(English);
                        Application.Current.Resources.MergedDictionaries.Remove(Russian);
                        break;
                }
                LanguageChanged?.Invoke();
            }
        }
    }
}
