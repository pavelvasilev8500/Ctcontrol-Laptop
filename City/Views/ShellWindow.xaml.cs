using ClassesLibrary.Window;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace City.Views
{
    public partial class ShellWindow : Window
    {
        TranslateTransform translate = new TranslateTransform();
        TranslateTransform translate1 = new TranslateTransform();
        TranslateTransform translate2 = new TranslateTransform();

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
    }
}
