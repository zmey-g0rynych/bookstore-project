using System.Windows;

namespace WpfApp1
{
    public partial class BookDetailsWindow : Window
    {
        private readonly Book book;
        public bool AddedToCart { get; private set; } = false;

        public BookDetailsWindow(Book book)
        {
            InitializeComponent();
            this.book = book;

            // Заполняем поля
            TitleText.Text = book.Title;
            AuthorText.Text = $"{Application.Current.Resources["BookDetails_AuthorLabel"]} {book.AuthorFullName}";
            PriceText.Text = $"{Application.Current.Resources["BookDetails_PriceLabel"]} {RegionHelper.FormatCurrency(book.PriceWithMarkup)}";
            DescriptionText.Text = book.Description;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            AddedToCart = true;
            DialogResult = true; // <- вот это важно для ShowDialog()
            Close();
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
