using System.Windows;

namespace ClassesLibrary.Window
{
    public static class WindowsPostition
    {
        public static double SetLeft()
        {
            const int OffsetFromScreenBorder = 220;
            return SystemParameters.WorkArea.Right - OffsetFromScreenBorder;
        }
        public static double SetTop()
        {
            const int OffsetFromScreenBorder = 20;
            return SystemParameters.WorkArea.Top + OffsetFromScreenBorder;
        }
    }
}
