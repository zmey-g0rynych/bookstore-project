using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Settings;

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
            _ = LoadBooks(); // асинхронная загрузка книг
        }

        private void UpdateBalanceText()
        {
            BalanceText.Text = $"{Application.Current.Resources["BalanceText"]} {RegionHelper.FormatCurrency(balance)}";
        }

        private async Task LoadBooks()
        {
            try
            {
                var books = await _httpClient.GetFromJsonAsync<List<Book>>("");
                foreach (var book in books)
                {
                    book.PriceText = RegionHelper.FormatCurrency(RegionHelper.ApplyMarkup((decimal)book.Price));
                }
                BooksListView.ItemsSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format((string)Application.Current.Resources["Main_LoadBooksError"], ex.Message),
                    (string)Application.Current.Resources["Main_ErrorTitle"],
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
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
                        existingItem.Quantity++;
                    else
                        cart.Add(new CartItem { Book = selectedBook, Quantity = 1 });

                    MessageBox.Show(
                        (string)Application.Current.Resources["Cart_AddSuccess"],
                        (string)Application.Current.Resources["Cart_InfoTitle"],
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
        }

        private void PayCart_Click(object sender, RoutedEventArgs e)
        {
            decimal total = cart.Sum(item => RegionHelper.ApplyMarkup((decimal)item.Book.Price) * item.Quantity);

            if (total > balance)
            {
                MessageBox.Show(
                    string.Format((string)Application.Current.Resources["Cart_InsufficientFundsMsg"],
                                  RegionHelper.FormatCurrency(total),
                                  RegionHelper.FormatCurrency(balance)),
                    (string)Application.Current.Resources["Cart_InsufficientFundsTitle"],
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            balance -= total;
            cart.Clear();
            UpdateBalanceText();

            MessageBox.Show(
                (string)Application.Current.Resources["Cart_PaymentSuccess"],
                (string)Application.Current.Resources["Cart_InfoTitle"],
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            var cartWindow = new CartWindow(cart, balance);
            cartWindow.BalanceUpdated += newBalance =>
            {
                balance = newBalance;
                UpdateBalanceText();
            };
            cartWindow.ShowDialog(); // должно работать
        }


        private void TopUpBalance_Click(object sender, RoutedEventArgs e)
        {
            var topUpWindow = new TopUpWindow();
            if (topUpWindow.ShowDialog() == true)
            {
                balance += topUpWindow.Amount;
                UpdateBalanceText();

                // Используем ресурсы для перевода
                MessageBox.Show(
                    string.Format((string)Application.Current.Resources["TopUp_SuccessMsg"],
                                  RegionHelper.FormatCurrency(topUpWindow.Amount)),
                    (string)Application.Current.Resources["TopUp_SuccessTitle"],
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}
