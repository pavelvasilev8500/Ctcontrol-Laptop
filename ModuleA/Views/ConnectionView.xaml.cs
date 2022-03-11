using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ControlLibrary.Classes;

namespace ModuleA.Views
{
    /// <summary>
    /// Логика взаимодействия для ConnectionView.xaml
    /// </summary>
    public partial class ConnectionView : System.Windows.Controls.UserControl
    {
        //написать алгоритм для перерисовывания qr прі смене ip можно использовать для перезагрузки сервера
        Timer timer = new Timer();
        private string FirstIp { get; set; } = GetPCIP.getIP();
        private string SecondIp { get; set; }
        private string IP { get; set; } = GetPCIP.getIP() + "/api/pcdata/update/" + ControlLibrary.Properties.Settings.Default.PCID.ToString();
        private void CreateQR(string ip)
        {
            QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData data = qr.CreateQrCode(ip, QRCoder.QRCodeGenerator.ECCLevel.L);
            QRCoder.QRCode code = new QRCoder.QRCode(data);
            Bitmap bitmap = code.GetGraphic(20, System.Drawing.Color.White, System.Drawing.Color.Transparent, false);
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
        private void StartClock()
        {
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
            timer.Interval = 1000;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            SecondIp = GetPCIP.getIP();
            //System.Windows.MessageBox.Show($"{FirstIp} - {SecondIp} - {FirstIp == SecondIp}");
            if (FirstIp != SecondIp)
            {
                CreateQR(SecondIp + "/api/pcdata/update/" + ControlLibrary.Properties.Settings.Default.PCID.ToString());
                FirstIp = SecondIp;
            }
        }
        public ConnectionView()
        {
            InitializeComponent();
            StartClock();
            CreateQR(IP);
        }
    }
}
