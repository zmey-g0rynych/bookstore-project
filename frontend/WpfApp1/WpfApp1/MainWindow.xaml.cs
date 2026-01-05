using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new();
        private const string BaseUrl = "http://localhost:8080/api/books";

        private List<CartItem> cart = new(); // список товаров в корзине
        private decimal balance = 0;

        public MainWindow(decimal initialBalance)
        {
            InitializeComponent();

            balance = initialBalance;
            UpdateBalanceText();

            _httpClient.BaseAddress = new Uri(BaseUrl);
            LoadBooks();
        }

        private void UpdateBalanceText()
        {
            BalanceText.Text = $"Ваш баланс: {balance:C}";
        }

        private async void LoadBooks()
        {
            try
            {
                var books = await _httpClient.GetFromJsonAsync<List<Book>>("");
                BooksListView.ItemsSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки книг: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BooksListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (BooksListView.SelectedItem is Book selectedBook)
            {
                var detailsWindow = new BookDetailsWindow(selectedBook);
                if (detailsWindow.ShowDialog() == true && detailsWindow.AddedToCart)
                {
                    var existingItem = cart.FirstOrDefault(item => item.Book.Title == selectedBook.Title);
                    if (existingItem != null)
                    {
                        existingItem.Quantity++;
                    }
                    else
                    {
                        cart.Add(new CartItem { Book = selectedBook, Quantity = 1 });
                    }

                    MessageBox.Show("Книга добавлена в корзину!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void PayCart_Click(object sender, RoutedEventArgs e)
        {
            decimal total = cart.Sum(item => (decimal)item.Book.Price * item.Quantity);

            if (total > balance)
            {
                MessageBox.Show($"Недостаточно средств. Требуется: {total:C}, доступно: {balance:C}", "Недостаточно средств", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            balance -= total;
            cart.Clear();
            UpdateBalanceText();

            MessageBox.Show("Оплата прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            var cartWindow = new CartWindow(cart, balance);
            cartWindow.BalanceUpdated += newBalance =>
            {
                balance = newBalance;
                UpdateBalanceText();
            };
            cartWindow.ShowDialog();
        }

        private void TopUpBalance_Click(object sender, RoutedEventArgs e)
        {
            var topUpWindow = new TopUpWindow();
            if (topUpWindow.ShowDialog() == true)
            {
                balance += topUpWindow.Amount;
                UpdateBalanceText();
            }
        }
    }
}
