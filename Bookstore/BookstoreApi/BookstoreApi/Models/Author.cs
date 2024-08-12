namespace BookstoreApi.Models
{
    public class Author
    {
        public int author_id { get; set; }
        public string name { get; set; }
        public string biography { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
