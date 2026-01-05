namespace WpfApp1
{
    public class Book
    {
        public string Title { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public string AuthorFullName => $"{AuthorFirstName} {AuthorLastName}";
    }
}
