using BookstoreApi.Models;

public class Genre
{
    public int genre_id { get; set; } // Primary key

    public string genre_name { get; set; }

    public ICollection<Book> Books { get; set; }
}
