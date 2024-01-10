using ClassesLibrary.Classes;
using Prism.Events;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace ModuleMobile.Views
{
    /// <summary>
    /// Логика взаимодействия для MobileView.xaml
    /// </summary>
    public partial class MobileView : UserControl
    {
        IEventAggregator _ea;
        TranslateTransform translate = new TranslateTransform();
        private bool _switch { get; set; } = true;
        public MobileView(IEventAggregator ea)
        {
            InitializeComponent();
            _ea = ea;
            _ea.GetEvent<SendIdEvent>().Subscribe(Id);
        }

        private void Id(string id)
        {
            CreateQr(id);
        }

        private void CreateQr(string id)
        {
            QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData data = qr.CreateQrCode(id, QRCoder.QRCodeGenerator.ECCLevel.L);
            QRCoder.QRCode code = new QRCoder.QRCode(data);
            System.Drawing.Bitmap bitmap = code.GetGraphic(20, System.Drawing.Color.White, System.Drawing.Color.Transparent, false);
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                QR.Source = bitmapimage;
            }
        }

        private void Switcher(object sender, System.Windows.RoutedEventArgs e)
        {
            if(_switch)
            {
                _switch = false;
                SwitcherButton.RenderTransform = translate;
                var converter = new System.Windows.Media.BrushConverter(); 
                //SwitcherColor.Background = (System.Windows.Media.Brush)converter.ConvertFromString("#FFFFFFFF");
                translate.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(0, 10, TimeSpan.FromMilliseconds(300)));
            }
            else
            {
                _switch = true;
                SwitcherButton.RenderTransform = translate;
                var converter = new System.Windows.Media.BrushConverter();
                //SwitcherColor.Background = (System.Windows.Media.Brush)converter.ConvertFromString("#FFFFFFFF");
                translate.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation(10, 0, TimeSpan.FromMilliseconds(300)));
            }
        }
    }
}
