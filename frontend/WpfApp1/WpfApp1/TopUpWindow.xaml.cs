using System;
using System.Windows;

namespace WpfApp1
{
    public partial class TopUpWindow : Window
    {
        public decimal Amount { get; private set; }

        public TopUpWindow()
        {
            InitializeComponent();
        }

        private void TopUp_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(TopUpAmountBox.Text, out decimal value) && value > 0)
            {
                Amount = value;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Введите корректную положительную сумму.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
