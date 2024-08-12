using Microsoft.EntityFrameworkCore;
using BookstoreApi.Models;

namespace BookstoreApi.Data
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; } // Add this line

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys and relationships
            modelBuilder.Entity<Genre>().HasKey(g => g.genre_id);
            modelBuilder.Entity<Author>().HasKey(a => a.author_id);
            modelBuilder.Entity<Book>().HasKey(b => b.book_id);

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Books)
                .WithOne(b => b.Genre)
                .HasForeignKey(b => b.genre_id);

            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.author_id);

            // Seed data
            modelBuilder.Entity<Author>().HasData(
                new Author { author_id = 1, name = "J.K. Rowling", biography = "British author, best known for the Harry Potter series." },
                new Author { author_id = 2, name = "George R.R. Martin", biography = "American novelist and short story writer, known for A Song of Ice and Fire." }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre { genre_id = 1, genre_name = "Fantasy" },
                new Genre { genre_id = 2, genre_name = "Science Fiction" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    book_id = 1,
                    title = "Harry Potter and the Sorcerer's Stone",
                    author_id = 1,
                    genre_id = 1,
                    price = 19.99m,
                    publication_date = new DateTime(1997, 6, 26),
                    image = "data:image/jpg;base64,<Base64ImageString>" // Replace with actual Base64 image string
                },
                new Book
                {
                    book_id = 2,
                    title = "A Game of Thrones",
                    author_id = 2,
                    genre_id = 1,
                    price = 15.99m,
                    publication_date = new DateTime(1996, 8, 6),
                    image = "data:image/jpg;base64,<Base64ImageString>" // Replace with actual Base64 image string
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
