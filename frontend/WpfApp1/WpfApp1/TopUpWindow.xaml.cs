using System;
using System.Windows;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    public partial class TopUpWindow : Window
    {
        public decimal Amount { get; private set; }

        public TopUpWindow()
        {
            InitializeComponent();
        }

        private static readonly Regex MoneyRegex =
    new Regex(@"^\d+([.,]\d{1,2})?$");

        private void TopUp_Click(object sender, RoutedEventArgs e)
        {
            string input = TopUpAmountBox.Text;

            input = input.Trim();
            input = input.Replace(',', '.');

            if (!MoneyRegex.IsMatch(input))
            {
                MessageBox.Show(
                    "Введите корректную сумму пополнения",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            Amount = decimal.Parse(
                input,
                System.Globalization.CultureInfo.InvariantCulture);

            DialogResult = true;
            Close();
        }

    }
}
