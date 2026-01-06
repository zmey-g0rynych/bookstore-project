using System.Windows;
using System.Text.RegularExpressions;
using WpfApp1.Settings;

namespace WpfApp1
{
    public partial class WelcomeWindow : Window
    {
        public decimal EnteredBalance { get; private set; }
        public bool IsConfirmed { get; private set; } = false;

        private static readonly Regex MoneyRegex =
            new Regex(@"^\d+([.,]\d{1,2})?$");

        public WelcomeWindow()
        {
            InitializeComponent();

            // Значение по умолчанию
            RegionComboBox.SelectedIndex = 0;
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            // ---------- ВЫБОР РЕГИОНА ----------
            if (RegionComboBox.SelectedIndex < 0)
            {
                MessageBox.Show(
                    (string)Application.Current.Resources["Welcome_SelectRegionMsg"],
                    (string)Application.Current.Resources["Welcome_SelectRegionTitle"],
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            switch (RegionComboBox.SelectedIndex)
            {
                case 0:
                    AppSettings.CurrentRegion = Region.Russia;
                    AppSettings.CurrentLanguage = "ru-RU";
                    break;
                case 1:
                    AppSettings.CurrentRegion = Region.Ukraine;
                    AppSettings.CurrentLanguage = "uk-UA";
                    break;
                case 2:
                    AppSettings.CurrentRegion = Region.UK;
                    AppSettings.CurrentLanguage = "en-GB";
                    break;
            }

            // Подгружаем нужный словарь ресурсов
            App.LoadLanguage(AppSettings.CurrentLanguage);

            // ---------- ОБРАБОТКА БАЛАНСА ----------
            string input = BalanceInput.Text;

            input = input.Trim();
            input = input.Replace(',', '.');

            if (!MoneyRegex.IsMatch(input))
            {
                MessageBox.Show(
                    (string)Application.Current.Resources["Welcome_InvalidBalanceMsg"],
                    (string)Application.Current.Resources["Welcome_InvalidBalanceTitle"],
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            decimal value = decimal.Parse(
                input,
                System.Globalization.CultureInfo.InvariantCulture);

            // ---------- ПЕРЕХОД В ГЛАВНОЕ ОКНО ----------
            var mainWindow = new MainWindow(value);
            mainWindow.Show();
            Close();
        }
    }
}
    