using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace City.MainWindowClasses
{
    public class GetSettings
    {
        private bool _firstStart { get; set; }
        private string _id { get; set; }
        private string _systemUri { get; set; }
        private string _clientUri { get; set; }
        private string _statusUri { get; set; }
        private string _wallpaper { get; set; }
        private System.Globalization.CultureInfo _language { get; set; }
        public (bool, string, string, string, string, string, System.Globalization.CultureInfo) GetAllSettings()
        {
            _firstStart = Properties.Settings.Default.FirstStart;
            _id = Properties.Settings.Default.ClientId;
            _systemUri = Properties.Settings.Default.SystemUri;
            _clientUri = Properties.Settings.Default.ClientUri;
            _statusUri = Properties.Settings.Default.StatusUri;
            _wallpaper = ModuleSettings.Properties.Settings.Default.DefaultWallpaper;
            _language = ModuleSettings.Properties.Settings.Default.DefaultLanguage;
            return (_firstStart, _id, _systemUri, _clientUri, _statusUri, _wallpaper, _language);
        }
    }
}
