using ClassesLibrary.Window;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ClassesLibrary.SystemInfo;
using System.Threading;
using System.Threading.Tasks;

namespace City.Views
{
    /// <summary>
    /// Логика взаимодействия для ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        TranslateTransform translate = new TranslateTransform();
        TranslateTransform translate1 = new TranslateTransform();
        TranslateTransform translate2 = new TranslateTransform();

        TranslateTransform wifi1 = new TranslateTransform();
        TranslateTransform wifi2 = new TranslateTransform();
        TranslateTransform wifi3 = new TranslateTransform();
        TranslateTransform wifi4 = new TranslateTransform();
        private bool _wifi { get; set; } = true;
        private bool _switch { get; set; } = true;
        public ShellWindow()
        {
            InitializeComponent();
            Left = WindowsPostition.SetLeft();
            Top = WindowsPostition.SetTop();
            //ShowInTaskbar = false;
        }

        private void Drag_Move(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            Point relativePoint = Marcker.TransformToAncestor(ButtonsGrid).Transform(new Point(0, 0));
            Point h = Home.TransformToAncestor(ButtonsGrid).Transform(new Point(0, 0));
            Marcker.RenderTransform = translate;
            translate.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(relativePoint.X, 0, TimeSpan.FromMilliseconds(300)));
        }
        private void Laptop_Click(object sender, RoutedEventArgs e)
        {
            Point relativePoint = Marcker.TransformToAncestor(ButtonsGrid).Transform(new Point(0, 0));
            Point c = Control.TransformToAncestor(ButtonsGrid).Transform(new Point(0, 0));
            Marcker.RenderTransform = translate;
            translate.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(relativePoint.X, c.X-13, TimeSpan.FromMilliseconds(300)));
        }
        private void Mobile_Click(object sender, RoutedEventArgs e)
        {
            Point relativePoint = Marcker.TransformToAncestor(ButtonsGrid).Transform(new Point(0, 0));
            Point m = Mobile.TransformToAncestor(ButtonsGrid).Transform(new Point(0, 0));
            Marcker.RenderTransform = translate;
            translate.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(relativePoint.X, m.X-24, TimeSpan.FromMilliseconds(300)));
        }

        private void Switcher(object sender, RoutedEventArgs e)
        {
            if (_switch)
            {
                Swither1.RenderTransform = translate1;
                translate1.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(0, 3.5, TimeSpan.FromMilliseconds(300)));
                Swither2.RenderTransform = translate2;
                translate2.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(3, -3.5, TimeSpan.FromMilliseconds(300)));
                _switch = false;
            }
            else
            {
                Swither1.RenderTransform = translate1;
                translate1.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(3, 0, TimeSpan.FromMilliseconds(300)));
                Swither2.RenderTransform = translate2;
                translate2.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(-3.5, 0, TimeSpan.FromMilliseconds(300)));
                _switch = true;
            }
        }


        //private void Wifi(bool IsConnect)
        //{
        //    Wifi1.RenderTransform = wifi1;
        //    Wifi2.RenderTransform = wifi2;
        //    Wifi3.RenderTransform = wifi3;
        //    Wifi4.RenderTransform = wifi4;
        //    //InternetInfo.ConnectionStatatus())
        //    if (IsConnect)
        //    {
        //        wifi1.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(0, 5, TimeSpan.FromMilliseconds(300)));
        //        wifi2.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(0, 7, TimeSpan.FromMilliseconds(300)));
        //        wifi3.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(0, 9, TimeSpan.FromMilliseconds(300)));
        //        wifi4.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(0, 11, TimeSpan.FromMilliseconds(300)));
        //    }
        //    else
        //    {
        //        wifi1.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(5, 0, TimeSpan.FromMilliseconds(300)));
        //        wifi2.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(7, 0, TimeSpan.FromMilliseconds(300)));
        //        wifi3.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(9, 0, TimeSpan.FromMilliseconds(300)));
        //        wifi4.BeginAnimation(TranslateTransform.YProperty, new DoubleAnimation(11, 0, TimeSpan.FromMilliseconds(300)));
        //    }
        //}
    }
}
