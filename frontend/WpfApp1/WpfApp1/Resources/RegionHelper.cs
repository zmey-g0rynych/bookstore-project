using System.Globalization;
using WpfApp1.Settings;

namespace WpfApp1
{
    public static class RegionHelper
    {
        // Применяет наценку в зависимости от региона
        public static decimal ApplyMarkup(decimal price)
        {
            return AppSettings.CurrentRegion switch
            {
                Region.Russia => price,           // без наценки
                Region.Ukraine => price * 1.05m,  // +5%
                Region.UK => price * 1.10m,       // +10%
                _ => price
            };
        }

        public static string FormatCurrency(decimal amount)
        {
            return AppSettings.CurrentRegion switch
            {
                Region.Russia => $"{amount:N2} ₽",
                Region.Ukraine => $"{amount:N2} ₴",
                Region.UK => $"{amount:N2} £",
                _ => amount.ToString("N2")
            };
        }

        public static CultureInfo GetCultureInfo()
        {
            return AppSettings.CurrentRegion switch
            {
                Region.Russia => new CultureInfo("ru-RU"),
                Region.Ukraine => new CultureInfo("uk-UA"),
                Region.UK => new CultureInfo("en-GB"),
                _ => CultureInfo.InvariantCulture
            };
        }
    }
}
