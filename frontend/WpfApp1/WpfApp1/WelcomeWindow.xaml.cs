using System.Windows;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    public partial class WelcomeWindow : Window
    {
        public decimal EnteredBalance { get; private set; }
        public bool IsConfirmed { get; private set; } = false;

        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private static readonly Regex MoneyRegex =
    new Regex(@"^\d+([.,]\d{1,2})?$");

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            string input = BalanceInput.Text;

            // 1️⃣ обработка текста
            input = input.Trim();
            input = input.Replace(',', '.');

            // 2️⃣ регулярное выражение
            if (!MoneyRegex.IsMatch(input))
            {
                MessageBox.Show(
                    "Введите корректную сумму (например: 100 или 100.50)",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // 3️⃣ финальное преобразование
            decimal value = decimal.Parse(
                input,
                System.Globalization.CultureInfo.InvariantCulture);

            var mainWindow = new MainWindow(value);
            mainWindow.Show();
            Close();
        }

    }

}
