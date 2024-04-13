namespace City.MainWindowClasses
{
    public class GetSettings
    {
        private bool _firstStart { get; set; }
        private string _id { get; set; }
        private string _wallpaper { get; set; }
        private System.Globalization.CultureInfo _language { get; set; }
        public (bool, string, string, System.Globalization.CultureInfo) GetAllSettings()
        {
            _firstStart = Properties.Settings.Default.FirstStart;
            _id = Properties.Settings.Default.ClientId;
            _wallpaper = ModuleSettings.Properties.Settings.Default.DefaultWallpaper;
            _language = ModuleSettings.Properties.Settings.Default.DefaultLanguage;
            return (_firstStart, _id, _wallpaper, _language);
        }
    }
}
