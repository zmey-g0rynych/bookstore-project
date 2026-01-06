using WpfApp1.Settings;

namespace WpfApp1
{
    public static class AppSettings
    {
        public static Region CurrentRegion { get; set; } = Region.Russia;
        public static string CurrentLanguage { get; set; } = "ru-RU";
    }
}
