using BookstoreLibrary;
using System.Globalization;
using WpfApp1.Settings;

namespace WpfApp1
{
    public static class RegionHelper
    {
        public static decimal ApplyMarkup(decimal price)
        {
            decimal markupPercent = AppSettings.CurrentRegion switch
            {
                Region.Russia => 0m,
                Region.Ukraine => 5m,
                Region.UK => 10m,
                _ => 0m
            };

            return Utilities.ApplyMarkup(price, markupPercent);
        }

        public static string FormatCurrency(decimal amount)
        {
            string symbol = AppSettings.CurrentRegion switch
            {
                Region.Russia => "₽",
                Region.Ukraine => "₴",
                Region.UK => "£",
                _ => "$"
            };

            return Utilities.FormatCurrency(amount, symbol);
        }
    }
}
