using System;
using System.Collections.Generic;
using System.Windows;

namespace ResourcesLibrary.Resources.Wallpapers.Classes
{
    public static class Wallpapers
    {
        private static string wallpaper;
        private static List<string> m_Wallpapers = new List<string>()
        {
            "GrayTheme",
            "BlueTheme",
            "MonoGrayTheme",
            "MonoBlueTheme",
            "BlackTheme",
            "BlackDrawableTheme"
        };
        public static List<string> All_Wallpapers
        {
            get
            {
                return m_Wallpapers;
            }
        }
        public static string Wallpaper
        {
            get
            {
                return wallpaper;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == wallpaper) return;
                wallpaper = value;
                switch (wallpaper)
                {
                    case "GrayTheme":
                        ResourceDictionary GrayTheme = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Themes/GrayTheme.xaml", UriKind.Relative)) as ResourceDictionary;
                        Application.Current.Resources.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(GrayTheme);
                        break;
                    case "BlueTheme":
                        ResourceDictionary BlueTheme = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Themes/BlueTheme.xaml", UriKind.Relative)) as ResourceDictionary;
                        Application.Current.Resources.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(BlueTheme);
                        break;
                    case "MonoGrayTheme":
                        ResourceDictionary MonoGrayTheme = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Themes/MonoLightGrayTheme.xaml", UriKind.Relative)) as ResourceDictionary;
                        Application.Current.Resources.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(MonoGrayTheme);
                        break;
                    case "MonoBlueTheme":
                        ResourceDictionary MonoBlueTheme = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Themes/MonoBlueTheme.xaml", UriKind.Relative)) as ResourceDictionary;
                        Application.Current.Resources.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(MonoBlueTheme);
                        break;
                    case "BlackTheme":
                        ResourceDictionary BlackTheme = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Themes/BlackTheme.xaml", UriKind.Relative)) as ResourceDictionary;
                        Application.Current.Resources.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(BlackTheme);
                        break;
                    case "BlackDrawableTheme":
                        ResourceDictionary BlackDrawableTheme = Application.LoadComponent(new Uri("/ResourcesLibrary;component/Resources/Themes/BlackDrawableTheme.xaml", UriKind.Relative)) as ResourceDictionary;
                        Application.Current.Resources.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(BlackDrawableTheme);
                        break;
                }
            }
        }
    }
}
