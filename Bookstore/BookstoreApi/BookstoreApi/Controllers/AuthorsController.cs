using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookstoreApi.Data;
using BookstoreApi.Models;
using BookstoreApi.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookstoreContext _context;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(BookstoreContext context, ILogger<AuthorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            try
            {
                var authors = await _context.Authors
                    .Select(a => new AuthorDTO
                    {
                        author_id = a.author_id,
                        name = a.name,
                        biography = a.biography
                    })
                    .ToListAsync();

                _logger.LogInformation("Fetched {Count} authors", authors.Count);
                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching authors");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching authors");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            try
            {
                var author = await _context.Authors
                    .Where(a => a.author_id == id)
                    .Select(a => new AuthorDTO
                    {
                        author_id = a.author_id,
                        name = a.name,
                        biography = a.biography
                    })
                    .FirstOrDefaultAsync();

                if (author == null)
                {
                    _logger.LogWarning("Author not found: {Id}", id);
                    return NotFound();
                }

                _logger.LogInformation("Fetched author: {Id}", id);
                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching author {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching author");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO authorDTO)
        {
            try
            {
                var author = new Author
                {
                    name = authorDTO.name,
                    biography = authorDTO.biography
                };

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                authorDTO.author_id = author.author_id;

                _logger.LogInformation("Created new author with ID: {Id}", authorDTO.author_id);
                return CreatedAtAction(nameof(GetAuthor), new { id = authorDTO.author_id }, authorDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating author");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating author");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDTO>> PutAuthor(int id, AuthorDTO authorDTO)
        {
            if (id != authorDTO.author_id)
            {
                return BadRequest("Author ID mismatch");
            }

            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning("Author not found: {Id}", id);
                    return NotFound("Author not found");
                }

                author.name = authorDTO.name;
                author.biography = authorDTO.biography;

                _context.Entry(author).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var updatedAuthorDTO = new AuthorDTO
                {
                    author_id = author.author_id,
                    name = author.name,
                    biography = author.biography
                };

                _logger.LogInformation("Updated author with ID: {Id}", id);
                return Ok(updatedAuthorDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    _logger.LogWarning("Author not found: {Id}", id);
                    return NotFound("Author not found");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating author with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the author.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning("Author not found: {Id}", id);
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Deleted author with ID: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting author with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting author");
            }
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.author_id == id);
        }
    }
}
