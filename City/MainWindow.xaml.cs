using City.Classes;
using System;
using System.Windows;

namespace City
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Left = Position.GetLeft();
            Top = Position.GetTop();
            ShowInTaskbar = false;
        }

        public void Move(object sender, EventArgs e)
        {
            DragMove();
        }
    }
}
