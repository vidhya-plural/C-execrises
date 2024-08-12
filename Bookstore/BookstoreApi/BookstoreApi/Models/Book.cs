namespace BookstoreApi.Models
{
    public class Book
    {
        public int book_id { get; set; }
        public string title { get; set; }
        public int author_id { get; set; }
        public int genre_id { get; set; }
        public decimal price { get; set; }
        public DateTime publication_date { get; set; }
        public string? image { get; set; } // This will now handle large Base64 strings
        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}
