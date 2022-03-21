using ClassesLibrary.Classes;
using Prism.Events;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ModuleMobile.Views
{
    /// <summary>
    /// Логика взаимодействия для MobileView.xaml
    /// </summary>
    public partial class MobileView : UserControl
    {
        IEventAggregator _ea;
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
    }
}
