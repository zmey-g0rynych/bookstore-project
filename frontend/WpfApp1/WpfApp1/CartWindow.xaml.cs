using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
                $"{item.Book.Title} — {item.Quantity} шт. — {item.Book.Price:C} за шт.");

            decimal total = cartItems.Sum(item => (decimal)item.Book.Price * item.Quantity);
            TotalText.Text = $"Общая сумма: {total:C}";
            BalanceText.Text = $"Ваш баланс: {balance:C}";
        }

        private void RemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            if (CartList.SelectedIndex >= 0 && CartList.SelectedIndex < cartItems.Count)
            {
                var selectedItem = cartItems[CartList.SelectedIndex];
                selectedItem.Quantity--;

                if (selectedItem.Quantity <= 0)
                {
                    cartItems.RemoveAt(CartList.SelectedIndex);
                }

                UpdateCartDisplay();
            }
            else
            {
                MessageBox.Show("Выберите книгу для удаления.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void PayCart_Click(object sender, RoutedEventArgs e)
        {
            decimal total = cartItems.Sum(item => (decimal)item.Book.Price * item.Quantity);

            if (total > balance)
            {
                MessageBox.Show($"Недостаточно средств. Требуется: {total:C}, доступно: {balance:C}", "Недостаточно средств", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            balance -= total;
            cartItems.Clear();
            UpdateCartDisplay();

            BalanceUpdated?.Invoke(balance); // обновим основной баланс
            MessageBox.Show("Оплата прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
