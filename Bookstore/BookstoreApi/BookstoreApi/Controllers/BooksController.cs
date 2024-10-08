﻿using BookstoreApi.Data;
using BookstoreApi.DTOs;
using BookstoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookstoreContext _context;
        private readonly string _uploadsFolderPath;
        private const string DateFormat = "yyyy-MM-dd"; // Define the date format
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookstoreContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            _logger = logger;

            // Ensure the uploads folder exists
            if (!Directory.Exists(_uploadsFolderPath))
            {
                Directory.CreateDirectory(_uploadsFolderPath);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            try
            {
                var books = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .Select(b => new BookDTO
                    {
                        book_id = b.book_id,
                        title = b.title,
                        author_id = b.author_id,
                        genre_id = b.genre_id,
                        price = b.price,
                        publication_date = b.publication_date.ToString(DateFormat), // Format date to string
                        image = b.image // URL to image file
                    })
                    .ToListAsync();

                _logger.LogInformation("Fetched {Count} books", books.Count);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching books");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            try
            {
                var book = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .Where(b => b.book_id == id)
                    .Select(b => new BookDTO
                    {
                        book_id = b.book_id,
                        title = b.title,
                        author_id = b.author_id,
                        genre_id = b.genre_id,
                        price = b.price,
                        publication_date = b.publication_date.ToString(DateFormat), // Format date to string
                        image = b.image // URL to image file
                    })
                    .FirstOrDefaultAsync();

                if (book == null)
                {
                    _logger.LogWarning("Book not found: {Id}", id);
                    return NotFound();
                }

                _logger.LogInformation("Fetched book with ID: {Id}", id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching book with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching book");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> PostBook(BookDTO bookDTO)
        {
            // Validate author and genre existence
            if (!await _context.Authors.AnyAsync(a => a.author_id == bookDTO.author_id))
            {
                _logger.LogWarning("Invalid author ID: {AuthorId}", bookDTO.author_id);
                return BadRequest("Invalid author ID.");
            }

            if (!await _context.Genres.AnyAsync(g => g.genre_id == bookDTO.genre_id))
            {
                _logger.LogWarning("Invalid genre ID: {GenreId}", bookDTO.genre_id);
                return BadRequest("Invalid genre ID.");
            }

            // Parse publication_date from string to DateTime
            if (!DateTime.TryParseExact(bookDTO.publication_date, DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime parsedDate))
            {
                _logger.LogWarning("Invalid publication date format: {PublicationDate}", bookDTO.publication_date);
                return BadRequest("Invalid publication date format.");
            }

            var book = new Book
            {
                title = bookDTO.title,
                author_id = bookDTO.author_id,
                genre_id = bookDTO.genre_id,
                price = bookDTO.price,
                publication_date = parsedDate,
                image = bookDTO.image // URL or Base64 encoded image
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            bookDTO.book_id = book.book_id;

            _logger.LogInformation("Created new book with ID: {Id}", bookDTO.book_id);
            return CreatedAtAction(nameof(GetBook), new { id = bookDTO.book_id }, bookDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDTO bookDTO)
        {
            if (id != bookDTO.book_id)
            {
                _logger.LogWarning("Book ID mismatch: {Id} - {BookId}", id, bookDTO.book_id);
                return BadRequest("Book ID mismatch.");
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Book not found: {Id}", id);
                return NotFound();
            }

            // Validate author and genre
            if (!await _context.Authors.AnyAsync(a => a.author_id == bookDTO.author_id))
            {
                _logger.LogWarning("Invalid author ID: {AuthorId}", bookDTO.author_id);
                return BadRequest("Invalid author ID.");
            }

            if (!await _context.Genres.AnyAsync(g => g.genre_id == bookDTO.genre_id))
            {
                _logger.LogWarning("Invalid genre ID: {GenreId}", bookDTO.genre_id);
                return BadRequest("Invalid genre ID.");
            }

            // Parse publication_date from string to DateTime
            if (!DateTime.TryParseExact(bookDTO.publication_date, DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime parsedDate))
            {
                _logger.LogWarning("Invalid publication date format: {PublicationDate}", bookDTO.publication_date);
                return BadRequest("Invalid publication date format.");
            }

            book.title = bookDTO.title;
            book.author_id = bookDTO.author_id;
            book.genre_id = bookDTO.genre_id;
            book.price = bookDTO.price;
            book.publication_date = parsedDate;
            book.image = bookDTO.image; // URL or Base64 encoded image

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    _logger.LogWarning("Book not found: {Id}", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var updatedBookDTO = new BookDTO
            {
                book_id = book.book_id,
                title = book.title,
                author_id = book.author_id,
                genre_id = book.genre_id,
                price = book.price,
                publication_date = book.publication_date.ToString(DateFormat),
                image = book.image
            };

            _logger.LogInformation("Updated book with ID: {Id}", id);
            return Ok(updatedBookDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Book not found: {Id}", id);
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted book with ID: {Id}", id);
            return NoContent();
        }

        [HttpPost("upload-image")]
        [ApiExplorerSettings(IgnoreApi = true)]  // Exclude this endpoint from Swagger documentation
        public async Task<IActionResult> UploadImage([FromForm] int book_id, [FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                _logger.LogWarning("No file uploaded.");
                return BadRequest("No file uploaded.");
            }

            var book = await _context.Books.FindAsync(book_id);
            if (book == null)
            {
                _logger.LogWarning("Book not found: {Id}", book_id);
                return NotFound("Book not found.");
            }

            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(_uploadsFolderPath, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                book.image = $"/uploads/{fileName}";
                _context.Entry(book).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file for book ID: {BookId}", book_id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while uploading the image.");
            }

            _logger.LogInformation("Uploaded image for book ID: {BookId}", book_id);
            return Ok(new { Status = "Success", ImagePath = book.image });
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.book_id == id);
        }
    }
}
