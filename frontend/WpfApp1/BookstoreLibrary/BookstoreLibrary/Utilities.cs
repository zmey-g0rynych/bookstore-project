namespace BookstoreLibrary
{
    public static class Utilities
    {
        // Метод для расчета наценки
        public static decimal ApplyMarkup(decimal price, decimal markupPercent)
        {
            return price + (price * markupPercent / 100m);
        }

        // Метод для форматирования валюты (можно для WPF)
        public static string FormatCurrency(decimal amount, string currencySymbol = "$")
        {
            return $"{currencySymbol}{amount:F2}";
        }
    }
}
