using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfApp1.Settings;

namespace WpfApp1
{
    public partial class CartWindow : Window
    {
        private List<CartItem> cartItems;
        private decimal balance;

        public event Action<decimal> BalanceUpdated;

        public CartWindow(List<CartItem> cartItems, decimal currentBalance)
        {
            InitializeComponent();

            this.cartItems = cartItems;
            this.balance = currentBalance;

            UpdateCartDisplay();
        }

        private void UpdateCartDisplay()
        {
            CartList.ItemsSource = null;
            CartList.ItemsSource = cartItems.Select(item =>
                $"{item.Book.Title} — {item.Quantity} шт. — {RegionHelper.FormatCurrency(item.Book.PriceWithMarkup)} за шт.");

            decimal total = cartItems.Sum(item => item.Book.PriceWithMarkup * item.Quantity);

            // Берём корректный ключ ресурса для TotalText
            if (Application.Current.Resources.Contains("Cart_TotalText"))
                TotalText.Text = string.Format((string)Application.Current.Resources["Cart_TotalText"], RegionHelper.FormatCurrency(total));
            else
                TotalText.Text = $"Total: {RegionHelper.FormatCurrency(total)}";

            BalanceText.Text = $"{Application.Current.Resources["BalanceText"]} {RegionHelper.FormatCurrency(balance)}";
        }

        private void PayCart_Click(object sender, RoutedEventArgs e)
        {
            decimal total = cartItems.Sum(item => item.Book.PriceWithMarkup * item.Quantity);

            if (total > balance)
            {
                MessageBox.Show(
                    string.Format(
                        (string)Application.Current.Resources["Cart_InsufficientFundsMsg"],
                        RegionHelper.FormatCurrency(total),
                        RegionHelper.FormatCurrency(balance)
                    ),
                    (string)Application.Current.Resources["Cart_InsufficientFundsTitle"],
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            balance -= total;
            cartItems.Clear();
            UpdateCartDisplay();

            BalanceUpdated?.Invoke(balance);

            MessageBox.Show(
                (string)Application.Current.Resources["Cart_PaymentSuccess"],
                (string)Application.Current.Resources["Cart_PaymentSuccessTitle"],
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        private void RemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            if (CartList.SelectedIndex >= 0 && CartList.SelectedIndex < cartItems.Count)
            {
                var selectedItem = cartItems[CartList.SelectedIndex];
                selectedItem.Quantity--;

                if (selectedItem.Quantity <= 0)
                    cartItems.RemoveAt(CartList.SelectedIndex);

                UpdateCartDisplay();
            }
            else
            {
                MessageBox.Show(
                    (string)Application.Current.Resources["Cart_SelectBookToRemove"],
                    (string)Application.Current.Resources["Cart_InfoTitle"],
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
