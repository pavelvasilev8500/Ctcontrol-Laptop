using ClassesLibrary.Window;
using System.Windows;

namespace City.Views
{
    /// <summary>
    /// Логика взаимодействия для ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
            Left = WindowsPostition.SetLeft();
            Top = WindowsPostition.SetTop();
            ShowInTaskbar = false;
        }

        private void Button_Move(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
