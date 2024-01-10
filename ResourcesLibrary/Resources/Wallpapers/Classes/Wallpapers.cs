using System;
using System.Collections.Generic;
using System.Windows;

namespace ResourcesLibrary.Resources.Wallpapers.Classes
{
    public static class Wallpapers
    {
        private static string _wallpaper;
        private static ResourceDictionary BigSurDay = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Wallpapers/Dictionaries/BigSurDay.xaml", UriKind.Relative)) as ResourceDictionary;
        private static ResourceDictionary BigSurNight = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Wallpapers/Dictionaries/BigSurNight.xaml", UriKind.Relative)) as ResourceDictionary;

        public static List<string> m_Wallpapers = new List<string>()
        {
            "BigSurDay",
            "BigSurNight",
        };

        public static string Wallpaper
        {
            get
            {
                return _wallpaper;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == _wallpaper) return;
                var oldwall = _wallpaper;
                _wallpaper = value;
                switch (_wallpaper)
                {
                    case "BigSurDay":
                        Application.Current.Resources.MergedDictionaries.Add(BigSurDay);
                        break;
                    case "BigSurNight":
                        Application.Current.Resources.MergedDictionaries.Add(BigSurNight);
                        break;
                }
                switch(oldwall)
                {
                    case "BigSurDay":
                        Application.Current.Resources.MergedDictionaries.Remove(BigSurDay);
                        break;
                    case "BigSurNight":
                        Application.Current.Resources.MergedDictionaries.Remove(BigSurNight);
                        break;
                }
            }
        }
    }
}
