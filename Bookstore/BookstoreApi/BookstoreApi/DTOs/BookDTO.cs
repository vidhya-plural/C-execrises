namespace BookstoreApi.DTOs
{
    public class BookDTO
    {
        public int book_id { get; set; }
        public string title { get; set; }
        public int author_id { get; set; }
        public int genre_id { get; set; }
        public decimal price { get; set; }
        public string publication_date { get; set; } // Keep as string
        public string? image { get; set; } // Base64 encoded image data
    }
}
