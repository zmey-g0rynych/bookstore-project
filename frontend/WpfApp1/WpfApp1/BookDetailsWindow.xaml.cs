using System.Windows;

namespace WpfApp1
{
    public partial class BookDetailsWindow : Window
    {
        public bool AddedToCart { get; private set; }

        public BookDetailsWindow(Book book)
        {
            InitializeComponent();
            TitleText.Text = book.Title;
            AuthorText.Text = "Автор: " + book.AuthorFullName;
            PriceText.Text = "Цена: " + book.Price + "₽";
            DescriptionText.Text = book.Description;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            AddedToCart = true;
            DialogResult = true;
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}